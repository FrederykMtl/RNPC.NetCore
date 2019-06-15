using System;
using System.Collections.Generic;
using RNPC.Core.Action;
using RNPC.Core.Enums;
using RNPC.Core.Exceptions;
using RNPC.Core.Interfaces;
using RNPC.Core.Memory;
using RNPC.Core.Resources;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.DecisionTrees
{
    /// <summary>
    /// TemplateMethod pattern - set the functional template for all tree nodes
    /// </summary>
    public abstract class AbstractDecisionNode : IDecisionNode
    {
        public IDecisionNode RightNode;
        public IDecisionNode LeftNode;
        public int ConfiguredPassFailValue;
        public List<NodeTestInfo> DataToMemorize;

        /// <summary>
        /// Defaulted reaction for a tree. Only stored in the root.
        /// </summary>
        public string DefaultTreeReaction = string.Empty;

        private IDecisionNode _parentNode;


        /// <summary>
        /// Default ctor
        /// </summary>
        protected AbstractDecisionNode()
        {
            DataToMemorize = new List<NodeTestInfo>();
        }

        /// <summary>
        /// Indicates the node type
        /// </summary>
        /// <returns>Node type</returns>
        public string GetNodeType()
        {
            return _parentNode == null ? "Root" : "Node";
        }

        //Sets the node that called this one
        public void SetParentNode(IDecisionNode parent)
        {
            _parentNode = parent;
        }

        public IDecisionNode GetParentNode()
        {
            return _parentNode;
        }

        public List<NodeTestInfo> GetNodeTestsData()
        {
            return DataToMemorize;
        }

        #region Node evaluation

        /// <summary>
        /// Main method - template for the FileController evaluation
        /// </summary>
        /// <param name="traits">Character Traits</param>
        /// <param name="memory">Character's Memory and knowledge</param>
        /// <param name="perceivedEvent">Event to evaluate</param>
        /// <returns>Character's reaction</returns>
        public List<Reaction> Evaluate(CharacterTraits traits, Memory.Memory memory, PerceivedEvent perceivedEvent)
        {
            List<Reaction> reactions;

            if (EvaluateNode(perceivedEvent, memory, traits))
            {
                if (RightNode == null)
                    throw new NullNodeException($"Right node for node {this} is missing.");

                reactions = RightNode.Evaluate(traits, memory, perceivedEvent);

                if (RightNode.GetNodeType() == "Node")
                    DataToMemorize.AddRange(((AbstractDecisionNode) RightNode).DataToMemorize);
            }
            else
            {
                if (LeftNode == null)
                    throw new NullNodeException($"Left node for node {this} is missing.");

                reactions = LeftNode.Evaluate(traits, memory, perceivedEvent);

                if (LeftNode.GetNodeType() == "Node")
                    DataToMemorize.AddRange(((AbstractDecisionNode)LeftNode).DataToMemorize);
            }

            return reactions;
        }

        /// <summary>
        /// Main method for node evaluation purposes
        /// </summary>
        /// <param name="perceivedEvent">The event tht is reacted to</param>
        /// <param name="memory">Character's memory and knowledge</param>
        /// <param name="traits">Chracter's traits</param>
        /// <returns>Test passed</returns>
        protected virtual bool EvaluateNode(PerceivedEvent perceivedEvent, Memory.Memory memory, CharacterTraits traits)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Attribute testing tools and logging

        /// <summary>
        /// Takes the value of an value and compares it against a random value
        /// </summary>
        /// <param name="attributeValue">Value of the value to be tested</param>
        /// <param name="description">description of the test params</param>
        /// <param name="characteristicName"></param>
        /// <param name="modifier">value, if any, that modified the value of the value tested</param>
        /// <returns></returns>
        protected bool TestAttributeAgainstRandomValue(int attributeValue, string description, string characteristicName, int modifier = 0)
        {
            int randomValue = RandomValueGenerator.GeneratePercentileIntegerValue();

            var myTestResult = new NodeTestInfo
            {
                TestedCharacteristic = CharacteristicType.Quality,
                CharacteristicName = characteristicName,
                AttributeValue = attributeValue,
                ProfileScore = EvaluateProfileScore(characteristicName, attributeValue),
                Result = attributeValue + modifier >= randomValue, //this is the test
                PassingValue = randomValue,
                Description = description,
                Modifier = modifier
            };

            DataToMemorize.Add(myTestResult);

            return myTestResult.Result;
        }

        /// <summary>
        /// Takes the value of an value and check if it is smaller or equal than a specified value
        /// </summary>
        /// <param name="attributeValue">Value of the value to be tested</param>
        /// <param name="setValue">value to test against</param>
        /// <param name="description">description of the test params</param>
        /// <param name="characteristicName"></param>
        /// <param name="modifier">value, if any, that modified the value of the value tested</param>
        /// <returns></returns>
        protected bool TestAttributeSmallerOrEqualThanSetValue(int attributeValue, int setValue, string description, string characteristicName, int modifier = 0)
        {
            var myTestResult = new NodeTestInfo
            {
                TestedCharacteristic = CharacteristicType.Quality,
                CharacteristicName = characteristicName,
                AttributeValue = attributeValue,
                ProfileScore = EvaluateProfileScore(characteristicName, attributeValue),
                Result = attributeValue + modifier <= setValue, //this is the test
                PassingValue = setValue,
                Description = description,
                Modifier = modifier
            };

            DataToMemorize.Add(myTestResult);

            return myTestResult.Result;
        }

        /// <summary>
        /// Takes the value of an value and check if it is greater or equal than a specified value
        /// </summary>
        /// <param name="attributeValue">Value of the value to be tested</param>
        /// <param name="setValue">value to test against</param>
        /// <param name="description">description of the test params</param>
        /// <param name="characteristicName"></param>
        /// <param name="characteristic"></param>
        /// <param name="modifier">value, if any, that modified the value of the value tested</param>
        /// <returns></returns>
        protected bool TestAttributeGreaterOrEqualThanSetValue(int attributeValue, int setValue, string description, string characteristicName, CharacteristicType characteristic, int modifier = 0)
        {
            var myTestResult = new NodeTestInfo
            {
                TestedCharacteristic = characteristic,
                CharacteristicName = characteristicName,
                AttributeValue = attributeValue,
                ProfileScore = characteristic == CharacteristicType.Quality? EvaluateProfileScore(characteristicName, attributeValue) : 0,
                Result = attributeValue + modifier >= setValue, //this is the test
                PassingValue = setValue,
                Description = description,
                Modifier = modifier
            };

            DataToMemorize.Add(myTestResult);

            return myTestResult.Result;
        }

        protected bool CharacterHasPersonalValue(PersonalValues value, CharacterTraits traits, string description = "")
        {
            var myTestResult = new NodeTestInfo
            {
                TestedCharacteristic = CharacteristicType.Values,
                CharacteristicName = value.ToString(),
                AttributeValue = 0,
                ProfileScore = IsValueArchetypal(value)? 0.1 : 0,
                Result = traits.DoIPossessThisPersonalValue(value), //this is the test
                PassingValue = 1,
                Description = description,
                Modifier = 0
            };

            DataToMemorize.Add(myTestResult);

            return myTestResult.Result;
        }

        /// <summary>
        /// Executes a random test against an value value
        /// </summary>
        /// <param name="value">value value</param>
        /// <returns>Attribute is greater; test passed</returns>
        protected bool SetValueIsGreaterThanRandomValue(int value)
        {
            return value > RandomValueGenerator.GeneratePercentileIntegerValue();
        }

        private static double EvaluateProfileScore(string characteristicName, int attributeValue)
        {
            if (attributeValue >= Constants.MinStrongPoint)
                if(CharacteristicIsStrongPoint(characteristicName))
                    return 1.0;

            if (attributeValue <= Constants.MaxWeakPoint)
                if (CharacteristicIsWeakPoint(characteristicName))
                    return - 0.25;

            return 0;
        }

        private static bool IsValueArchetypal(PersonalValues value)
        {
            return Enum.IsDefined(typeof(ArchetypalValues), value.ToString());
        }

        private static bool CharacteristicIsWeakPoint(string characteristicName)
        {
            return Enum.IsDefined(typeof(CompiledWeakPoints), characteristicName);
        }

        private static bool CharacteristicIsStrongPoint(string characteristicName)
        {
            return Enum.IsDefined(typeof(CompiledStrongPoints), characteristicName);
        }

        #endregion
    }
}