using System;
using System.Linq;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Exceptions;
using RNPC.Core.Interfaces;
using RNPC.Core.Learning.Interfaces;
using RNPC.Core.Resources;

// ReSharper disable JoinNullCheckWithUsage
//doesn't work
namespace RNPC.Core.Learning.Substitutions
{
    /// <summary>
    /// This class manages all nodes to be replaced
    /// </summary>
    public class SubstitutionController : ISubstitutionController
    {
        //Dependencies
        private readonly ISubstitutionDocumentConverter _converter;
        private readonly IXmlFileController _fileController;
        private readonly ISubstitutionMapper _substitutionMapper;

        //work variables
        private readonly string _substitionsFilePath;

        /// <summary>
        /// defaukt ctor
        /// </summary>
        /// <param name="converter">Class that converst an xml document to a list of substitutions</param>
        /// <param name="fileController">File controller</param>
        /// <param name="substitutionMapper">Mappping class of substitutios</param>
        public SubstitutionController(ISubstitutionDocumentConverter converter, IXmlFileController fileController, ISubstitutionMapper substitutionMapper)
        {
            if (substitutionMapper == null)
                throw new RnpcParameterException("A mapper is required for decision tree learning.");

            if (fileController == null)
                throw new RnpcParameterException("A file controller is required for decision tree learning.");

            if (converter == null)
                throw new RnpcParameterException("A converter is required for decision tree learning.");

            if (string.IsNullOrWhiteSpace(ConfigurationDirectory.Instance.NodeSubstitutionsFile))
                throw new RnpcParameterException("A valid file path is required for decision tree learning.");

            _converter = converter;
            _fileController = fileController;
            _substitionsFilePath = ConfigurationDirectory.Instance.NodeSubstitutionsFile;
            _substitutionMapper = substitutionMapper;

            if(!GetSubstitutionsList())
                throw new Exception("No substitutions have been loaded. Decision tree learning can not be done.");
        }

        /// <summary>
        /// A tree has found to be a candidate for substitution. Apply logic.
        /// </summary>
        /// <param name="builder">Tree builder class</param>
        /// <param name="characterName">name of the character evolding</param>
        /// <param name="action">action that triggered evolution</param>
        /// <param name="decisionTreeToChange">Decision Tree To evolve</param>
        /// <returns></returns>
        public bool SubstituteNode(ITreeBuilder builder, string characterName, RNPC.Core.Action.Action action, string decisionTreeToChange)
        {
            var initialAction = ((Reaction) action).InitialEvent as RNPC.Core.Action.Action;

            if (initialAction == null)
                return false;

            var substitution = _substitutionMapper.GetSubstitutableSubTreeForLeaf(action.EventName);

            if (substitution == null)
                return false;

            var firstNode = builder.BuildTreeFromDocument(_fileController, initialAction, characterName);

            if (firstNode == null)
                return false;

            var replacementNode = builder.BuildSubTreeFromRepository(_fileController, substitution.SubTreeName, ConfigurationDirectory.Instance.SubTreeRepository);

            if (replacementNode == null)
                return false;

            if (!FindAndReplaceMatchingNode(firstNode, substitution, replacementNode))
                return false;

            string fileName = initialAction.ActionType + "-" + initialAction.Intent + "-" + initialAction.EventName;
            
            builder.BuildAndSaveXmlDocumentFromTree(_fileController, firstNode, fileName, characterName);

            return true;
        }

        public bool PruneDecisionTree(ITreeBuilder builder, string characterName, RNPC.Core.Action.Action action, string decisionTreeToChange)
        {
            return true;
        }

        /// <summary>
        /// Method that calls the recursive method that finds nodes that are subject to substitution
        /// </summary>
        /// <param name="firstNode">node to start with</param>
        /// <param name="substitution">substitution to do</param>
        /// <param name="replacementNode"></param>
        private static bool FindAndReplaceMatchingNode(IDecisionNode firstNode, Substition substitution, IDecisionNode replacementNode)
        {
            //A decision tree could conceivably be only a leaf
            //That would be an extremely simple decision process
            //an subject to evolving
            var abstractNode = firstNode as AbstractDecisionNode;

            if (abstractNode == null)
            {
                if (!EvaluateSubstitution(firstNode, substitution))
                    return false;

                firstNode = replacementNode;
                return true;
            }

            return FindMatchingNodes(firstNode, substitution, replacementNode);
        }

        /// <summary>
        /// Recursive method that iterates through the tree and finds nodes that are subject to substitution
        /// </summary>
        /// <param name="currentNode">node to start with</param>
        /// <param name="substitution">substitution to do</param>
        /// <param name="nodeToReplace">The node will be substituted</param>
        private static bool FindMatchingNodes(IDecisionNode currentNode, Substition substitution, IDecisionNode nodeToReplace)
        {
            var abstractNode = (AbstractDecisionNode) currentNode;

            if (abstractNode.LeftNode != null)
            {
                // ReSharper disable once UsePatternMatching
                var leftNode = abstractNode.LeftNode as AbstractDecisionNode;

                if (leftNode != null)
                {
                    if (FindMatchingNodes(leftNode, substitution, nodeToReplace))
                        return true;
                }
                else
                {
                    if (EvaluateSubstitution(abstractNode.LeftNode, substitution))
                    {
                        abstractNode.LeftNode = nodeToReplace;
                        return true;
                    }
                }
            }

            if (abstractNode.RightNode == null)
                return false;

            // ReSharper disable once UsePatternMatching
            var rightNode = abstractNode.RightNode as AbstractDecisionNode;

            if (rightNode != null)
            {
                if (FindMatchingNodes(rightNode, substitution, nodeToReplace))
                    return true;
            }

            if (!EvaluateSubstitution(abstractNode.RightNode, substitution))
                return false;

            abstractNode.RightNode = nodeToReplace;
            return true;

        }

        /// <summary>
        /// Verifies that the substitution configured is applicable here
        /// </summary>
        /// <param name="nodeToEvaluate">the node to evaluate</param>
        /// <param name="substitution">the substitution case</param>
        private static bool EvaluateSubstitution(IDecisionNode nodeToEvaluate, Substition substitution)
        {
            if (nodeToEvaluate.ToString().Replace("RNPC.DecisionLeaves.", "").ToLower() != substitution.LeafName.ToLower())
                return false;

            switch (substitution.Condition)
            {
                case SubstitionCondition.Default:
                    return true;
                case SubstitionCondition.ParentNot:
                    string parentName = nodeToEvaluate.GetParentNode()?.ToString().Replace("RNPC.DecisionNodes.", "").ToLower();
                    if (!substitution.ConditionName.ToLower().Split(',').Contains(parentName))
                    {
                        return true;
                    }

                    return false;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the list of possible substitutions
        /// </summary>
        /// <returns></returns>
        private bool GetSubstitutionsList()
        {
            var substitutionsDocument = _fileController.LoadNodeSubstitutionsFile(_substitionsFilePath);

            if (substitutionsDocument == null)
                return false;

            var substitutionsList = _converter.ConvertDocumentToList(substitutionsDocument);

            _substitutionMapper.AddSubstitution(substitutionsList);

            return true;
        }
    }
}
