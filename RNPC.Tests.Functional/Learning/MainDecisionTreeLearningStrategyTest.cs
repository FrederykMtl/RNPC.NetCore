using System;
using RNPC.FileManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.API;
using RNPC.Core.Action;
using RNPC.Core.Enums;
using RNPC.Core.Learning.DecisionTrees;
using RNPC.Core.Learning.Substitutions;
using RNPC.Core.Memory;
using Action = RNPC.Core.Action.Action;
// ReSharper disable InconsistentNaming

namespace RNPC.Tests.Functional.Learning
{
    [TestClass]
    public class MainDecisionTreeLearningStrategyTest : AbstractRnpcTest
    {
        [TestMethod]
        public void AnalyzeAndLearn_TestDecisionTreeEvolutions_DecisionTreeChanged()
        {
            //ARRANGE
                //"C:\\Sysdev\\RNPC\\RNPC\\DTO\\Learning\\Resources\\DecisionTreeSubstitutions.xml";
            var controller = new SubstitutionController(new SubstitutionDocumentConverter(), new DecisionTreeFileController(), new SubstitutionMapper());
            MainDecisionTreeLearningStrategy strategy = new MainDecisionTreeLearningStrategy(controller, new DecisionTreeBuilder());

            var Morty = new Person("Morty", Gender.Male, Sex.Male, Orientation.Undefined, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Morty, Archetype.TheInnocent)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            //Initial Action
            //Friendly Greeting
            Action greeting = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Friendly,
                Message = "Hi!",
                Target = character.MyName,
                EventName = "Greeting",
                Source = "The Friend"
            };

            var reaction = new Reaction
            {
                Tone = Tone.Neutral,
                Target = greeting.Source,
                Intent = Intent.Neutral,
                ActionType = ActionType.Verbal,
                InitialEvent = greeting,
                EventType = EventType.Interaction,
                ReactionScore = 0,
                EventName = "Greeting",
                Message = "Hello.", //TODO: Randomize
                Source = greeting.Target
            };

            character.MyMemory.AddActionToLongTermMemory(reaction);
            character.MyMemory.AddActionToLongTermMemory(reaction);
            character.MyMemory.AddActionToLongTermMemory(reaction);

            //ACT
            strategy.AnalyzeAndLearn(character);
            //ASSERT

        }

        [TestMethod]
        public void AnalyzeAndLearn_NoLearningDone_NoDecisionTreeChanged()
        {
            //ARRANGE
            //"C:\\Sysdev\\RNPC\\RNPC\\DTO\\Learning\\Resources\\DecisionTreeSubstitutions.xml";
            var controller = new SubstitutionController(new SubstitutionDocumentConverter(), new DecisionTreeFileController(), new SubstitutionMapper());
            MainDecisionTreeLearningStrategy strategy = new MainDecisionTreeLearningStrategy(controller, new DecisionTreeBuilder());

            var Morty = new Person("Morty", Gender.Male, Sex.Male, Orientation.Undefined, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Morty, Archetype.TheInnocent)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            //ACT
            strategy.AnalyzeAndLearn(character);
            //ASSERT
            Assert.IsFalse(strategy.DecisionTreeEvolved);
        }
    }
}
