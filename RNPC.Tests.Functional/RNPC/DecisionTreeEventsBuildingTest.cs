using System;
using RNPC.FileManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.API;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;
using Action = RNPC.Core.Action.Action;

namespace RNPC.Tests.Functional.RNPC
{
    /// <summary>
    /// Every added tree should be tested here
    /// </summary>
    [TestClass]
    public class DecisionTreeEventsBuildingTest
    {
        [TestMethod]
        public void BuildTreeFromDocument_FriendlyGreeting_TreeIsProperlyBuilt()
        {
            //ARRANGE
            DecisionTreeBuilder builder = new DecisionTreeBuilder();

            // ReSharper disable once InconsistentNaming
            var Morty = new Person("Morty", Gender.Male, Sex.Male, Orientation.Undefined, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Morty, Archetype.TheInnocent)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

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

            //ACT
            var rootNode = builder.BuildTreeFromDocument(new DecisionTreeFileController(), greeting, "Morty") as AbstractDecisionNode;

            //ASSERT
            Assert.IsNotNull(rootNode);
            //ajusted per event
            Assert.IsFalse(string.IsNullOrEmpty(rootNode.DefaultTreeReaction));
            Assert.AreEqual(0, rootNode.ConfiguredPassFailValue);
            Assert.IsTrue(rootNode.LeftNode != null);
            Assert.IsTrue(rootNode.RightNode != null);
        }

        [TestMethod]
        public void BuildTreeFromDocument_HostileThreat_TreeIsProperlyBuilt()
        {
            //ARRANGE
            DecisionTreeBuilder builder = new DecisionTreeBuilder();

            // ReSharper disable once InconsistentNaming
            var Morty = new Person("Morty", Gender.Male, Sex.Male, Orientation.Undefined, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Morty, Archetype.TheInnocent)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            Action threat = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Hostile,
                Tone = Tone.Threatening,
                Message = "I'm going to kill you!",
                Target = character.MyName,
                EventName = "Threat",
                Source = "The Enemy"
            };

            //ACT
            var rootNode = builder.BuildTreeFromDocument(new DecisionTreeFileController(), threat, "Morty") as AbstractDecisionNode;
            //ASSERT
            Assert.IsNotNull(rootNode);
            //ajusted per event
            Assert.IsFalse(string.IsNullOrEmpty(rootNode.DefaultTreeReaction));
            Assert.AreEqual(0, rootNode.ConfiguredPassFailValue);
            Assert.IsTrue(rootNode.LeftNode != null);
            Assert.IsTrue(rootNode.RightNode != null);
        }

        [TestMethod]
        public void BuildTreeFromDocument_NeutralGreeting_TreeIsProperlyBuilt()
        {
            //ARRANGE
            DecisionTreeBuilder builder = new DecisionTreeBuilder();

            // ReSharper disable once InconsistentNaming
            var Morty = new Person("Morty", Gender.Male, Sex.Male, Orientation.Undefined, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Morty, Archetype.TheInnocent)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            //Neutral Greeting
            Action neutralGreeting = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Neutral,
                Message = "Hi",
                Target = character.MyName,
                EventName = "Greeting",
                Source = "The Ambassador"
            };

            //ACT
            var rootNode = builder.BuildTreeFromDocument(new DecisionTreeFileController(), neutralGreeting, "Morty") as AbstractDecisionNode;
            //ASSERT
            //ASSERT
            Assert.IsNotNull(rootNode);
            //ajusted per event
            Assert.IsFalse(string.IsNullOrEmpty(rootNode.DefaultTreeReaction));
            Assert.AreEqual(0, rootNode.ConfiguredPassFailValue);
            Assert.IsTrue(rootNode.LeftNode != null);
            Assert.IsTrue(rootNode.RightNode != null);
        }

        [TestMethod]
        public void BuildTreeFromDocument_FriendlyHowAreYou_TreeIsProperlyBuilt()
        {
            //ARRANGE
            DecisionTreeBuilder builder = new DecisionTreeBuilder();

            // ReSharper disable once InconsistentNaming
            var Morty = new Person("Morty", Gender.Male, Sex.Male, Orientation.Undefined, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Morty, Archetype.TheInnocent)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            //Friendly How Are You?
            Action enquire = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Friendly,
                Message = "How are you?",
                Target = character.MyName,
                EventName = "HowAreYou",
                Source = "The Friend"
            };

            //ACT
            var rootNode = builder.BuildTreeFromDocument(new DecisionTreeFileController(), enquire, @"Morty") as AbstractDecisionNode;
            //ASSERT
            //ASSERT
            Assert.IsNotNull(rootNode);
            //adjusted per event
            Assert.IsFalse(string.IsNullOrEmpty(rootNode.DefaultTreeReaction));
            Assert.AreEqual(40, rootNode.ConfiguredPassFailValue);
            Assert.IsTrue(rootNode.LeftNode != null);
            Assert.IsTrue(rootNode.RightNode != null);
        }

        [TestMethod]
        public void BuildTreeFromDocument_HostileInsult_TreeIsProperlyBuilt()
        {
            //ARRANGE
            DecisionTreeBuilder builder = new DecisionTreeBuilder();

            // ReSharper disable once InconsistentNaming
            var Morty = new Person("Morty", Gender.Male, Sex.Male, Orientation.Undefined, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Morty, Archetype.TheInnocent)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            //Hostile-Insult
            Action insult = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Hostile,
                Message = "You're really dumb.",
                Target = character.MyName,
                EventName = "Insult",
                Source = "The Frenemy"
            };

            //ACT
            var rootNode = builder.BuildTreeFromDocument(new DecisionTreeFileController(), insult, "Morty") as AbstractDecisionNode;
            //ASSERT
            //ASSERT
            Assert.IsNotNull(rootNode);
            //ajusted per event
            Assert.IsFalse(string.IsNullOrEmpty(rootNode.DefaultTreeReaction));
            Assert.AreEqual(0, rootNode.ConfiguredPassFailValue);
            Assert.IsTrue(rootNode.LeftNode != null);
            Assert.IsTrue(rootNode.RightNode != null);
        }

        [TestMethod]
        public void BuildTreeFromDocument_NeutralInquiry_TreeIsProperlyBuilt()
        {
            //ARRANGE
            DecisionTreeBuilder builder = new DecisionTreeBuilder();

            // ReSharper disable once InconsistentNaming
            var Morty = new Person("Morty", Gender.Male, Sex.Male, Orientation.Undefined, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Morty, Archetype.TheInnocent)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            //Neutral How Are You?
            Action neutralEnquire = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Neutral,
                Message = "How are you?",
                Target = character.MyName,
                EventName = "HowAreYou",
                Source = "The Ambassador"
            };

            //ACT
            var rootNode = builder.BuildTreeFromDocument(new DecisionTreeFileController(), neutralEnquire, "Morty") as AbstractDecisionNode;
            //ASSERT
            //ASSERT
            Assert.IsNotNull(rootNode);
            //ajusted per event
            Assert.IsFalse(string.IsNullOrEmpty(rootNode.DefaultTreeReaction));
            Assert.AreEqual(40, rootNode.ConfiguredPassFailValue);
            Assert.IsTrue(rootNode.LeftNode != null);
            Assert.IsTrue(rootNode.RightNode != null);
        }

        [TestMethod]
        public void BuildTreeFromDocument_FriendlyTeasing_TreeIsProperlyBuilt()
        {
            //ARRANGE
            DecisionTreeBuilder builder = new DecisionTreeBuilder();

            // ReSharper disable once InconsistentNaming
            var Morty = new Person("Morty", Gender.Male, Sex.Male, Orientation.Undefined, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Morty, Archetype.TheInnocent)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            //Friendly Teasing
            Action teasing = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Friendly,
                Message = "Did you have a fight with your comb this morning?",
                Target = character.MyName,
                EventName = "Teasing",
                Source = "The Friend"
            };

            //ACT
            var rootNode = builder.BuildTreeFromDocument(new DecisionTreeFileController(), teasing, "Morty") as AbstractDecisionNode;
            //ASSERT
            //ASSERT
            Assert.IsNotNull(rootNode);
            //ajusted per event
            Assert.IsFalse(string.IsNullOrEmpty(rootNode.DefaultTreeReaction));
            Assert.AreEqual(0, rootNode.ConfiguredPassFailValue);
            Assert.IsTrue(rootNode.LeftNode != null);
            Assert.IsTrue(rootNode.RightNode != null);
        }

        [TestMethod]
        public void BuildTreeFromDocument_HostileMocking_TreeIsProperlyBuilt()
        {
            //ARRANGE
            DecisionTreeBuilder builder = new DecisionTreeBuilder();

            // ReSharper disable once InconsistentNaming
            var Morty = new Person("Morty", Gender.Male, Sex.Male, Orientation.Undefined, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Morty, Archetype.TheInnocent)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            //Hostile Mockery
            Action mockery = new Action
            {
                Tone = Tone.Mocking,
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Hostile,
                Message = "Did your mother dress you up this morning?",
                Target = character.MyName,
                EventName = "Mocking",
                Source = "The Bully"
            };


            //ACT
            var rootNode = builder.BuildTreeFromDocument(new DecisionTreeFileController(), mockery, "Morty") as AbstractDecisionNode;
            //ASSERT
            //ASSERT
            Assert.IsNotNull(rootNode);
            //ajusted per event
            Assert.IsFalse(string.IsNullOrEmpty(rootNode.DefaultTreeReaction));
            Assert.AreEqual(60, rootNode.ConfiguredPassFailValue);
            Assert.IsTrue(rootNode.LeftNode != null);
            Assert.IsTrue(rootNode.RightNode != null);
        }

        [TestMethod]
        public void BuildTreeFromDocument_NeutralApology_TreeIsProperlyBuilt()
        {
            //ARRANGE
            DecisionTreeBuilder builder = new DecisionTreeBuilder();

            // ReSharper disable once InconsistentNaming
            var Morty = new Person("Morty", Gender.Male, Sex.Male, Orientation.Undefined, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Morty, Archetype.TheInnocent)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            //Neutral Apology
            Action apology = new Action
            {
                Tone = Tone.Apologetic,
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Neutral,
                Message = "My apologies",
                Target = character.MyName,
                EventName = "Apology",
                Source = "The Ambassador"
            };

            //ACT
            var rootNode = builder.BuildTreeFromDocument(new DecisionTreeFileController(), apology, "Morty") as AbstractDecisionNode;
            //ASSERT
            //ASSERT
            Assert.IsNotNull(rootNode);
            //ajusted per event
            Assert.IsFalse(string.IsNullOrEmpty(rootNode.DefaultTreeReaction));
            Assert.AreEqual(0, rootNode.ConfiguredPassFailValue);
            Assert.IsTrue(rootNode.LeftNode != null);
            Assert.IsTrue(rootNode.RightNode != null);
        }

        [TestMethod]
        public void BuildTreeFromDocument_FriendlySmile_TreeIsProperlyBuilt()
        {
            //ARRANGE
            DecisionTreeBuilder builder = new DecisionTreeBuilder();

            // ReSharper disable once InconsistentNaming
            var Morty = new Person("Morty", Gender.Male, Sex.Male, Orientation.Undefined, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Morty, Archetype.TheInnocent)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            //Friendly Smile
            Action smile = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.NonVerbal,
                Intent = Intent.Friendly,
                Message = "",
                Target = character.MyName,
                EventName = "Smile",
                Source = "The Friend"
            };

            //ACT
            var rootNode = builder.BuildTreeFromDocument(new DecisionTreeFileController(), smile, "Morty") as AbstractDecisionNode;
            //ASSERT
            //ASSERT
            Assert.IsNotNull(rootNode);
            //ajusted per event
            Assert.IsFalse(string.IsNullOrEmpty(rootNode.DefaultTreeReaction));
            Assert.AreEqual(40, rootNode.ConfiguredPassFailValue);
            Assert.IsTrue(rootNode.LeftNode != null);
            Assert.IsTrue(rootNode.RightNode != null);
        }

        [TestMethod]
        public void BuildTreeFromDocument_HostileGlare_TreeIsProperlyBuilt()
        {
            //ARRANGE
            DecisionTreeBuilder builder = new DecisionTreeBuilder();

            // ReSharper disable once InconsistentNaming
            var Morty = new Person("Morty", Gender.Male, Sex.Male, Orientation.Undefined, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Morty, Archetype.TheInnocent)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            //Hostile Glare
            Action glare = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.NonVerbal,
                Intent = Intent.Hostile,
                Message = "",
                Target = character.MyName,
                EventName = "Glare",
                Source = "The Bully"
            };

            //ACT
            var rootNode = builder.BuildTreeFromDocument(new DecisionTreeFileController(), glare, "Morty") as AbstractDecisionNode;
            //ASSERT
            //ASSERT
            Assert.IsNotNull(rootNode);
            //ajusted per event
            Assert.IsFalse(string.IsNullOrEmpty(rootNode.DefaultTreeReaction));
            Assert.AreEqual(0, rootNode.ConfiguredPassFailValue);
            Assert.IsTrue(rootNode.LeftNode != null);
            Assert.IsTrue(rootNode.RightNode != null);
        }

        [TestMethod]
        public void BuildTreeFromDocument_NeutralSalute_TreeIsProperlyBuilt()
        {
            //ARRANGE
            DecisionTreeBuilder builder = new DecisionTreeBuilder();

            // ReSharper disable once InconsistentNaming
            var Morty = new Person("Morty", Gender.Male, Sex.Male, Orientation.Undefined, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Morty, Archetype.TheInnocent)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            //Neutral Salute
            Action salute = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.NonVerbal,
                Intent = Intent.Neutral,
                Message = "",
                Target = character.MyName,
                EventName = "Salute",
                Source = "The Ambassador"
            };


            //ACT
            var rootNode = builder.BuildTreeFromDocument(new DecisionTreeFileController(), salute, "Morty") as AbstractDecisionNode;
            //ASSERT
            //ASSERT
            Assert.IsNotNull(rootNode);
            //ajusted per event
            Assert.IsFalse(string.IsNullOrEmpty(rootNode.DefaultTreeReaction));
            Assert.AreEqual(0, rootNode.ConfiguredPassFailValue);
            Assert.IsTrue(rootNode.LeftNode != null);
            Assert.IsTrue(rootNode.RightNode != null);
        }

        [TestMethod]
        public void BuildTreeFromDocument_NeutralHowAreYou_TreeIsProperlyBuilt()
        {
            //ARRANGE
            DecisionTreeBuilder builder = new DecisionTreeBuilder();

            // ReSharper disable once InconsistentNaming
            var Morty = new Person("Morty", Gender.Male, Sex.Male, Orientation.Undefined, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Morty, Archetype.TheInnocent)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            //Neutral How Are You?
            Action neutralEnquire = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Neutral,
                Message = "How are you?",
                Target = character.MyName,
                EventName = "HowAreYou",
                Source = "The Ambassador"
            };

            //ACT
            var rootNode = builder.BuildTreeFromDocument(new DecisionTreeFileController(), neutralEnquire, "Morty") as AbstractDecisionNode;
            //ASSERT
            //ASSERT
            Assert.IsNotNull(rootNode);
            //ajusted per event
            Assert.IsFalse(string.IsNullOrEmpty(rootNode.DefaultTreeReaction));
            Assert.AreEqual(40, rootNode.ConfiguredPassFailValue);
            Assert.IsTrue(rootNode.LeftNode != null);
            Assert.IsTrue(rootNode.RightNode != null);
        }
    }
}
