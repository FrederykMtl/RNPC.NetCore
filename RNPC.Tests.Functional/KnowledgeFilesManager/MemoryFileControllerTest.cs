using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using RNPC.FileManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.API;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.Tests.Functional.KnowledgeFilesManager
{
    [TestClass]
    public class MemoryFileControllerTest
    {
        #region Performance tests
        [TestMethod]
        public void WriteToFile_PerformanceTests_StatisticsReturned()
        {
            //ARRANGE
            MemoryFileController myController = new MemoryFileController(Directory.GetCurrentDirectory());//Cronos.Instance.BackupMemoryFiles);

            var knowledge = MemoryContentInitializer.CreateItemsAndLinkThem(new ItemLinkFactory());
            var testCharacter = CreateTestCharacter(knowledge);

            for (int i = 0; i <= 5; i++)
            {
                if(i > 0)
                {
                    testCharacter.AddContentToLongTermMemory(knowledge);
                }

                for (int k = 1; k <= 10; k++)
                {
                    Stopwatch timer = new Stopwatch();

                    timer.Start();
                    SerializeAndEncryptData(testCharacter);
                    timer.Stop();

                    Debug.Write((i + 1) + "\t" + timer.ElapsedMilliseconds);

                    timer.Restart();
                    DeserializeAndDecryptData(testCharacter);
                    timer.Stop();
                    Debug.Write("\t" + timer.ElapsedMilliseconds + "\r");

                    myController.DeleteFile(testCharacter.UniqueId);
                }
            }
        }

        private void SerializeAndEncryptData(global::RNPC.Core.Character testCharacter)
        {
            MemoryFileController myController = new MemoryFileController(Directory.GetCurrentDirectory());
            myController.WriteToFile(testCharacter.UniqueId, testCharacter.MyMemory);
        }

        private void DeserializeAndDecryptData(global::RNPC.Core.Character testCharacter)
        {
            MemoryFileController myController = new MemoryFileController(Directory.GetCurrentDirectory());
            myController.ReadFromFile(testCharacter.UniqueId);
        }        
        #endregion

        [TestMethod]
        public void WriteToFile_MemoriesWithContent_MemoriesWrittenToFile()
        {
            //ARRANGE
            MemoryFileController myController = new MemoryFileController(Directory.GetCurrentDirectory());
            List<MemoryItem> knowledge = MemoryContentInitializer.CreateItemsAndLinkThem(new ItemLinkFactory());

            var testCharacter = CreateTestCharacter(knowledge);

            //ACT
            myController.WriteToFile(testCharacter.UniqueId, testCharacter.MyMemory);

            //Assert
            myController.FileExists(testCharacter.UniqueId);

            //Cleanup
            myController.DeleteFile(testCharacter.UniqueId);
        }

        [TestMethod]
        public void WriteToFile_MemoriesWithContent_MemoriesReadFromile()
        {
            //ARRANGE
            MemoryFileController myController = new MemoryFileController(Directory.GetCurrentDirectory());

            var testCharacter = CreateTestCharacter(MemoryContentInitializer.CreateItemsAndLinkThem(new ItemLinkFactory()));
            myController.WriteToFile(testCharacter.UniqueId, testCharacter.MyMemory);

            Stopwatch perfTime = new Stopwatch();
            perfTime.Start();

            //ACT
            var memory = myController.ReadFromFile(testCharacter.UniqueId);

            perfTime.Stop();
            Debug.Write("Time spent: " + perfTime.ElapsedMilliseconds);

            //Assert
            Assert.IsNotNull(memory);
            Assert.AreEqual(77, memory.HowManyThingsDoIknow());

            var knowledge = memory.WhatDoIKnow();
            Assert.IsTrue(knowledge[0].ItemType == MemoryItemType.Person);
            Assert.IsTrue(knowledge[1].ItemType == MemoryItemType.Person);
            Assert.IsTrue(knowledge[7].ItemType == MemoryItemType.PastEvent);
            Assert.IsTrue(knowledge[9].ItemType == MemoryItemType.PastEvent);
            Assert.IsTrue(knowledge[16].ItemType == MemoryItemType.Place);
            Assert.IsTrue(knowledge[21].ItemType == MemoryItemType.Place);
            Assert.IsTrue(knowledge[27].ItemType == MemoryItemType.Organization);
            Assert.IsTrue(knowledge[32].ItemType == MemoryItemType.Occupation);
            Assert.IsTrue(knowledge[39].ItemType == MemoryItemType.PersonalRelationship);
            Assert.IsTrue(knowledge[40].ItemType == MemoryItemType.Association);
            Assert.IsTrue(knowledge[46].ItemType == MemoryItemType.EventRelationship);
            Assert.IsTrue(knowledge[51].ItemType == MemoryItemType.PersonalTie);
            Assert.IsTrue(knowledge[54].ItemType == MemoryItemType.PlaceRelationship);
            Assert.IsTrue(knowledge[57].ItemType == MemoryItemType.OccupationalTie);

            //Cleanup
            myController.DeleteFile(testCharacter.UniqueId);
        }

        /// <summary>
        /// Returns a test character - bugs bunny
        /// </summary>
        /// <returns></returns>
        private global::RNPC.Core.Character CreateTestCharacter(List<MemoryItem> knowledge)
        {
            var bugs = new Person("Bugs Bunny", Gender.Genderfluid, Sex.Undefined, Orientation.Asexual, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(bugs, Archetype.None)
                {
                    FileController = new DecisionTreeFileController(),
                    DecisionTreeBuilder = new DecisionTreeBuilder(),
                    KnowledgeRandomizer = new TestKnowledgeRandomization()
                };

            character.AddContentToLongTermMemory(knowledge);

            return character;
        }
    }
}