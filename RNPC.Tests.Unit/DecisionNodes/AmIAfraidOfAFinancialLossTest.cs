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
    public class AmIAfraidOfAFinancialLossTest
    {
        [TestMethod]
        public void EvaluateNode_WithWealthAsPersonalValue_EvaluationCompleted()
        {
            //ARRANGE
            // ReSharper disable once InconsistentNaming
            var Jess = new Person("Jessica Day", Gender.Female, Sex.Female, Orientation.Straight, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Jess, Archetype.TheCaregiver)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            character.MyTraits.PersonalValues.Add(PersonalValues.Wealth);

            AmIAfraidOfAFinancialLoss nodeToTest = new AmIAfraidOfAFinancialLoss()
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
            var reaction= nodeToTest.Evaluate(character.MyTraits, character.MyMemory, threat);

            //ASSERT
            Assert.IsNotNull(reaction);
            Assert.AreEqual(threat, reaction[0].InitialEvent);
        }

        [TestMethod]
        public void EvaluateNode_WithNoWealthAsPersonalValue_EvaluationCompleted()
        {
            //ARRANGE
            // ReSharper disable once InconsistentNaming
            var Jess = new Person("Jessica Day", Gender.Female, Sex.Female, Orientation.Straight, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Jess, Archetype.TheCaregiver)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            if (character.MyTraits.DoIPossessThisPersonalValue(PersonalValues.Wealth))
                character.MyTraits.PersonalValues.Remove(PersonalValues.Wealth);

            AmIAfraidOfAFinancialLoss nodeToTest = new AmIAfraidOfAFinancialLoss
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
        }
    }
}
