using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.API;
using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.Enums;
using RNPC.Core.Memory;
using RNPC.Core.Resources;
using RNPC.Core.TraitGeneration;
using RNPC.FileManager;
using Action = RNPC.Core.Action.Action;

namespace RNPC.Tests.Functional.Character
{
    [TestClass]
    public class CronosTest : AbstractFunctionalTest
    {
        [TestMethod]
        public void NotifyAllCharactersInRegion_TornadoEvent_CharacterNotified()
        {
            // ReSharper disable once InconsistentNaming
            var Sterling = new Person("Sterling Archer", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            var testCharacter = new global::RNPC.Core.Character(Sterling, Archetype.None)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder(),
                MyMemory =  new Memory(Sterling)
                //MyCurrentLocation = new Place("New York", Guid.NewGuid(), "The city at the center of the world")
            };

            testCharacter.CharacterReacted += TestCharacter_CharacterReacted;

            Cronos.Instance.AddFollower(testCharacter);

            var tornado = new Action
            {
                EventType = EventType.Weather,
                Target = "None",
                EventName = "Tornado",
                Source = "Unknown"
            };

            Cronos.Instance.NotifyAllCharactersInRegion(tornado,new Coordinates(), 500);
        }

        [TestMethod]
        public void NotifyAllCharactersInRegion_()
        {
            // ReSharper disable once InconsistentNaming
            var Sterling = new Person("Sterling Archer", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            var testCharacter = new global::RNPC.Core.Character(Sterling, Archetype.None)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder(),
                MyMemory = new Memory(Sterling)
            };

            testCharacter.CharacterReacted += TestCharacter_CharacterReacted;

            Cronos.Instance.AddFollower(testCharacter);

            Action tornado = new Action
            {
                EventType = EventType.Weather,
                Target = "None",
                EventName = "Tornado",
                Source = "Unknown"
            };

            Cronos.Instance.NotifyAllCharactersInRegion(tornado, new Coordinates(), 500);
        }

        private void TestCharacter_CharacterReacted(object sender, List<Reaction> e)
        {
            Assert.IsNotNull(sender);
            Assert.IsTrue(sender is global::RNPC.Core.Character);
            Assert.IsNotNull(e);
            Assert.AreEqual(e[0].Message, ErrorMessages.IDontKnow);
        }

        [TestMethod]
        public void Hibernate_CronosInstanceWithFollowersAndKnowledge_FileWritten()
        {
            //ARRANGE
            OmniscienceFileController controller = new OmniscienceFileController();
            Cronos.Instance.FileController = controller;
            Cronos.Instance.DeactivateMemoryBackups();
            if (controller.FileExists())
                controller.DeleteFile();

            var referenceData = MemoryContentInitializer.CreateItemsAndLinkThem(new ItemLinkFactory());

            for (int i = 0; i < 50; i++)
            {
                Person newPerson = new Person("NPC" + i, Gender.Agender, Sex.Undefined, Orientation.Asexual, Guid.Empty);
                var newNpc = new global::RNPC.Core.Character(newPerson, Archetype.None);

                foreach (var data in referenceData)
                {
                    if (RandomValueGenerator.GeneratePercentileIntegerValue() < 34)
                        newNpc.AddContentToLongTermMemory(data);
                }
                Cronos.Instance.AddFollower(newNpc);
            }

            Cronos.Instance.AddReferenceDataList(referenceData);
            //ACT
            Cronos.Instance.Hibernate();
            //ASSERT
            Assert.IsTrue(controller.FileExists());
        }

        [TestMethod]
        public void WakeUp_CronosInstanceWithFollowersAndKnowledge_FileRead()
        {
            //ARRANGE
            //string path = Directory.GetCurrentDirectory();

            var controller = new OmniscienceFileController();
            Cronos.Instance.FileController = controller;

            if (!controller.FileExists())
                Assert.Fail();

            //ConfigurationDirectory.Instance.KnowledgeFilesDirectory = Directory.GetCurrentDirectory();
            //ACT
            Cronos.Instance.WakeUp();
            //ASSERT
            Assert.IsTrue(Cronos.Instance.GetAllKnowledge().MyFollowers.Count != 0);
        }

        [TestMethod]
        public void ActivateMemoryBackups_MethodCall_MemoryBackupActivated()
        {
            //ARRANGE
            var myGod = Cronos.Instance;
            //ACT
            myGod.ActivateMemoryBackups();
            //ASSERT
            Assert.IsTrue(myGod.MyOmniscience.BackupMemoryFiles);
        }

        [TestMethod]
        public void DeactivateMemoryBackups_MethodCall_MemoryBackupDeactivated()
        {
            //ARRANGE
            var myGod = Cronos.Instance;
            myGod.ActivateMemoryBackups();
            //ACT
            myGod.DeactivateMemoryBackups();
            //ASSERT
            Assert.IsFalse(myGod.MyOmniscience.BackupMemoryFiles);
        }
    }
}