using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.API;
using RNPC.Core.Action;
using RNPC.Core.Enums;
using RNPC.Core.GameTime;
using RNPC.Core.Memory;
using RNPC.Core.Resources;
using RNPC.FileManager;
using Action = RNPC.Core.Action.Action;

namespace RNPC.Tests.Functional.Character
{
    [TestClass]
    public class CharacterEventTypesTest : AbstractFunctionalTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void New_PersonIsNull_ArgumentNullExceptionRaised()
        {
            // ReSharper disable once UnusedVariable
            var character = new global::RNPC.Core.Character(null, Archetype.None)
                {
                    FileController = new DecisionTreeFileController(),
                    DecisionTreeBuilder = new DecisionTreeBuilder()
                };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void New_InstantiateWithNoName_ArgumentNullExceptionRaised()
        {
            // ReSharper disable once UnusedVariable
            var character = new global::RNPC.Core.Character(new Person("", Guid.NewGuid()), Archetype.None)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
        }

        [TestMethod]
        public void InteractWithMe_InsultRandomArchetype_NormalReactionReturned()
        {
            var testCharacter = GetMeSterlingArcher();
            VerifyTestFilesAndDirectory(testCharacter.MyName);

            Action insult = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Hostile,
                Message = "Hey Swirling! You suck!",
                Target = "Sterling Archer",
                EventName = "Insult",
                Source = "Unknown"
            };

            var reaction = testCharacter.InteractWithMe(insult);
            Assert.IsNotNull(reaction);
            Assert.AreEqual(reaction[0].Target, "Unknown");
            Assert.AreEqual(reaction[0].InitialEvent, insult);
        }

        [TestMethod]
        public void InteractWithMe_NoReaderOrBuilder_ForgettingSomethingReaction()
        {
            // ReSharper disable once InconsistentNaming
            var Sterling = new Person("Sterling Archer", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid())
            {
                DateOfBirth = new StandardDateTime(DateTime.Now.AddYears(-33))
            };

            var testCharacter = new global::RNPC.Core.Character(Sterling, Archetype.None);

            VerifyTestFilesAndDirectory(testCharacter.MyName);

            var insult = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Hostile,
                Message = "Hey Swirling! You suck!",
                Target = "Sterling Archer",
                EventName = "Insult",
                Source = "Unknown"
            };

            var reaction = testCharacter.InteractWithMe(insult);
            Assert.IsNotNull(reaction);
            Assert.IsTrue(reaction[0].Message.Contains("It feels like I'm forgetting something"));
            Assert.AreEqual(reaction[0].InitialEvent, insult);
        }

        [TestMethod]
        public void InteractWithMe_EnvironmentalEvent_WrongEventReactionReturned()
        {
            var testCharacter = GetMeSterlingArcher();

            Action tornado = new Action
            {
                EventType = EventType.Environmental,
                Target = "None",
                EventName = "Tornado",
                Source = "Unknown"
            };

            var reaction = testCharacter.InteractWithMe(tornado);
            Assert.IsNotNull(reaction);
            Assert.AreEqual(reaction[0].Target, null);
            Assert.AreEqual(reaction[0].Message, ErrorMessages.EnvironmentEventPassedInWrongWay);
        }

        [TestMethod]
        public void InteractWithMe_TemperatureEvent_WrongEventReactionReturned()
        {
            var testCharacter = GetMeSterlingArcher();

            Action temperatureIncrease = new Action
            {
                EventType = EventType.Temperature,
                Target = "None",
                EventName = "TempIncrease",
                Source = "None"
            };

            var reaction = testCharacter.InteractWithMe(temperatureIncrease);
            Assert.IsNotNull(reaction);
            Assert.AreEqual(reaction[0].Target, null);
            Assert.AreEqual(reaction[0].Message, ErrorMessages.TemperatureEventPassedInWrongWay);
        }

        [TestMethod]
        public void InteractWithMe_WeatherEvent_WrongEventReactionReturned()
        {
            var testCharacter = GetMeSterlingArcher();

            Action tornado = new Action
            {
                EventType = EventType.Weather,
                Target = "None",
                EventName = "Tornado",
                Source = "None"
            };

            var reaction = testCharacter.InteractWithMe(tornado);
            Assert.IsNotNull(reaction);
            Assert.AreEqual(reaction[0].Target, null);
            Assert.AreEqual(reaction[0].Message, ErrorMessages.EnvironmentEventPassedInWrongWay);
        }

        [TestMethod]
        public void InteractWithMe_TimeEvent_WrongEventReactionReturned()
        {
            var testCharacter = GetMeSterlingArcher();

            Action hourPassed = new Action
            {
                EventType = EventType.Time,
                Target = "None",
                EventName = "HourPassed",
                Source = "None"
            };

            var reaction = testCharacter.InteractWithMe(hourPassed);
            Assert.IsNotNull(reaction);
            Assert.AreEqual(reaction[0].Target, null);
            Assert.AreEqual(reaction[0].Message, ErrorMessages.TimeEventPassedInWrongWay);
        }

        [TestMethod]
        public void InteractWithMe_BiologicalEvent_WrongEventReactionReturned()
        {
            var testCharacter = GetMeSterlingArcher();

            Action headache = new Action
            {
                EventType = EventType.Biological,
                Target = "Sterling Archer",
                EventName = "Headache",
                Source = "Unknown"
            };

            var reaction = testCharacter.InteractWithMe(headache);
            Assert.IsNotNull(reaction);
            Assert.AreEqual(reaction[0].Target, null);
            Assert.AreEqual(reaction[0].Message, ErrorMessages.PhysicalEventPassedInWrongWay);
        }

        [TestMethod]
        public void InteractWithMe_NoTreeDocument_IDontKnowReactionReturned()
        {
            var testCharacter = GetMeSterlingArcher();
            VerifyTestFilesAndDirectory(testCharacter.MyName);

            Action insult = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Hostile,
                Message = "Hey Swirling! You suck!",
                Target = "Sterling Archer",
                EventName = "Insult",
                Source = "Unknown"
            };

            var reaction = testCharacter.InteractWithMe(insult);
            Assert.IsNotNull(reaction);
            Assert.AreEqual(reaction[0].Target, "Unknown");
            Assert.AreEqual(reaction[0].InitialEvent, insult);
        }

        [TestMethod]
        public void InteractWithMe_NullEvent_ArgumentOutOfRangeExceptionRaised()
        {
            var testCharacter = GetMeSterlingArcher();

            var reaction = testCharacter.InteractWithMe(null);
            Assert.IsNotNull(reaction);
            Assert.IsTrue(reaction.Count == 1);
            Assert.IsTrue(reaction[0].Message == ErrorMessages.WhatIsGoingOn);
        }

        [TestMethod]
        public void NotifyEnvironmentalEvent_InvalidCaller_MethodCallerInvalidReactionReturned()
        {
            var testCharacter = GetMeSterlingArcher();

            Action tornado = new Action
            {
                EventType = EventType.Environmental,
                Target = "None",
                EventName = "Tornado",
                Source = "Unknown"
            };

            testCharacter.CharacterReacted += TestCharacterOnCharacterReacted;

            testCharacter.NotifyEnvironmentalEvent(tornado, null);
        }

        /// <summary>
        /// Assertion methods for previous test
        /// </summary>
        /// <param name="sender">The character</param>
        /// <param name="e">The reaction</param>
        private static void TestCharacterOnCharacterReacted(object sender, List<Reaction> e)
        {
            Assert.IsNotNull(sender);
            Assert.IsTrue(sender is global::RNPC.Core.Character);
            Assert.IsNotNull(e);
            Assert.AreEqual(e[0].Message, ErrorMessages.MethodCallerInvalid); 
        }

        [TestMethod]
        public void NotifyEnvironmentalEvent_EnvironmentalEvent_NormalReactionReturned()
        {
            var testCharacter = GetMeSterlingArcher();

            Action fight = new Action
            {
                EventType = EventType.Environmental,
                Target = "None",
                EventName = "NearbyFight",
                Source = "Unknown"
            };

            testCharacter.CharacterReacted += OnCharacterReactedEnvironmental;

            testCharacter.NotifyEnvironmentalEvent(fight, Cronos.Instance);
        }

        /// <summary>
        /// Assertion methods for previous test
        /// </summary>
        /// <param name="sender">The character</param>
        /// <param name="e">The reaction</param>
        private static void OnCharacterReactedEnvironmental(object sender, List<Reaction> e)
        {
            Assert.IsNotNull(sender);
            Assert.IsTrue(sender is global::RNPC.Core.Character);
            Assert.IsNotNull(e);
            Assert.AreEqual(ErrorMessages.IDontKnow, e[0].Message);
        }

        [TestMethod]
        public void NotifyEnvironmentalEvent_WeatherEvent_NormalReactionReturned()
        {
            var testCharacter = GetMeSterlingArcher();

            Action tornado = new Action
            {
                EventType = EventType.Weather,
                Target = "None",
                EventName = "Tornado",
                Source = "Unknown"
            };

            testCharacter.CharacterReacted += OnCharacterReactedWeather;

            testCharacter.NotifyEnvironmentalEvent(tornado, Cronos.Instance);
        }

        /// <summary>
        /// Assertion methods for previous test
        /// </summary>
        /// <param name="sender">The character</param>
        /// <param name="e">The reaction</param>
        private static void OnCharacterReactedWeather(object sender, List<Reaction> e)
        {
            Assert.IsNotNull(sender);
            Assert.IsTrue(sender is global::RNPC.Core.Character);
            Assert.IsNotNull(e);
            Assert.AreEqual(ErrorMessages.IDontKnow, e[0].Message);
        }

        [TestMethod]
        public void NotifyEnvironmentalEvent_TemperatureEvent_NormalReactionReturned()
        {
            var testCharacter = GetMeSterlingArcher();

            Action turnTheHeatUp = new Action
            {
                EventType = EventType.Temperature,
                Target = "None",
                EventName = "ItsGettingHotInHere",
                Source = "Unknown"
            };

            testCharacter.CharacterReacted += OnCharacterReactedTemperature;

            testCharacter.NotifyEnvironmentalEvent(turnTheHeatUp, Cronos.Instance);
        }

        /// <summary>
        /// Assertion methods for previous test
        /// </summary>
        /// <param name="sender">The character</param>
        /// <param name="e">The reaction</param>
        private static void OnCharacterReactedTemperature(object sender, List<Reaction> e)
        {
            Assert.IsNotNull(sender);
            Assert.IsTrue(sender is global::RNPC.Core.Character);
            Assert.IsNotNull(e);
            Assert.AreEqual(e[0].Message, ErrorMessages.IDontKnow);
        }

        [TestMethod]
        public void NotifyEnvironmentalEvent_TimeEvent_NormalReactionReturned()
        {
            var testCharacter = GetMeSterlingArcher();

            Action timePasses = new Action
            {
                EventType = EventType.Time,
                Target = "None",
                EventName = "TimeKeepsOnTicking",
                Source = "Unknown"
            };

            testCharacter.CharacterReacted += OnCharacterReactedTime;

            testCharacter.NotifyEnvironmentalEvent(timePasses, Cronos.Instance);
        }

        /// <summary>
        /// Assertion methods for previous test
        /// </summary>
        /// <param name="sender">The character</param>
        /// <param name="e">The reaction</param>
        private static void OnCharacterReactedTime(object sender, List<Reaction> e)
        {
            Assert.IsNotNull(sender);
            Assert.IsTrue(sender is global::RNPC.Core.Character);
            Assert.IsNotNull(e);
            Assert.AreEqual(e[0].Message, ErrorMessages.IDontKnow);
        }

        [TestMethod]
        public void New_WithInitializedMemory_MemoryContentSet()
        {
            var knowledge = MemoryContentInitializer.CreateItemsAndLinkThem(new ItemLinkFactory());

            var testCharacter = GetMeSterlingArcher();
            testCharacter.AddContentToLongTermMemory(knowledge);

            testCharacter.MyMemory.GetMyInformation().Description = "Sterling is a secret agent for ISIS";

            Assert.IsTrue(testCharacter.MyMemory.HowManyThingsDoIknow() > 0);
            Assert.IsTrue(testCharacter.MyMemory.HowManyThingsDoIknow() < 77);
            Assert.IsTrue(testCharacter.MyMemory.GetMyInformation().ItemType == MemoryItemType.Person);
            //TODO
            Assert.AreNotEqual(string.Empty, testCharacter.MyMemory.GetMyInformation().Description);
            //Assert.IsTrue(testCharacter.MyMemory.Persons.DoIKnowThisPerson(new Person("Geralt of Rivia")));
        }
    }
}