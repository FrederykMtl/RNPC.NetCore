using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.API;
using RNPC.API.DecisionNodes;
using RNPC.Core.Enums;
using RNPC.Core.Memory;
using RNPC.FileManager;
using RNPC.Tests.Unit.Stubs;
using Action = RNPC.Core.Action.Action;

namespace RNPC.Tests.Unit.DecisionNodes
{
    [TestClass]
    public class AmIAfraidToBeInjuredTest
    {
        [TestMethod]
        public void EvaluateNode_WithSecurityAsValue_EvaluationCompleted()
        {
            //ARRANGE
            // ReSharper disable once InconsistentNaming
            var Jess = new Person("Jessica Day", Gender.Female, Sex.Female, Orientation.Straight, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Jess, Archetype.TheCaregiver)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            character.MyTraits.PersonalValues.Add(PersonalValues.Security);

            var nodeToTest = new AmIAfraidToBeInjured
            {
                RightNode = new DecisionNodeStub(),
                LeftNode = new DecisionNodeStub()
            };

            Action threat = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Hostile,
                Message = "I'll make sure you have to beg for the rest of your life!",
                Target = character.MyName,
                EventName = "Threat",
                Source = "The Enemy"
            };

            //ACT
            var reaction = nodeToTest.Evaluate(character.MyTraits, character.MyMemory, threat);

            //ASSERT
            Assert.IsNotNull(reaction);
            Assert.AreEqual(threat, reaction[0].InitialEvent);
            Assert.IsNotNull(nodeToTest.DataToMemorize);
            Assert.AreEqual(4, nodeToTest.DataToMemorize.Count);
            Assert.AreEqual(-20, nodeToTest.DataToMemorize[3].Modifier);
        }

        [TestMethod]
        public void EvaluateNode_WithoutSecurityAsValue_EvaluationCompleted()
        {
            //ARRANGE
            // ReSharper disable once InconsistentNaming
            var Jess = new Person("Jessica Day", Gender.Female, Sex.Female, Orientation.Straight, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Jess, Archetype.TheCaregiver)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            var nodeToTest = new AmIAfraidToBeInjured
            {
                RightNode = new DecisionNodeStub(),
                LeftNode = new DecisionNodeStub()
            };

            Action threat = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Hostile,
                Message = "I'll make sure you have to beg for the rest of your life!",
                Target = character.MyName,
                EventName = "Threat",
                Source = "The Enemy"
            };

            //ACT
            var reaction = nodeToTest.Evaluate(character.MyTraits, character.MyMemory, threat);

            //ASSERT
            Assert.IsNotNull(reaction);
            Assert.AreEqual(threat, reaction[0].InitialEvent);
            Assert.IsNotNull(nodeToTest.DataToMemorize);
            Assert.AreEqual(4, nodeToTest.DataToMemorize.Count);
            Assert.AreEqual(0, nodeToTest.DataToMemorize[0].Modifier);
        }
    }
}
