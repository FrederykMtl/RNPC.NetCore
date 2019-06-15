using System;
using System.Collections.Generic;
using System.Linq;
using RNPC.FileManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.Core.Enums;
using RNPC.Core.Memory;
using RNPC.Core.TraitGeneration;

namespace RNPC.Tests.Functional.KnowledgeFilesManager
{
    [TestClass]
    public class OmniscienceFileControllerTest : AbstractFunctionalTest
    {
        [TestMethod]
        public void WriteToFile_OmniscienceFile_FileWritten()
        {
            //ARRANGE
            OmniscienceFileController controller = new OmniscienceFileController();

            if (controller.FileExists())
                controller.DeleteFile();

            var referenceData = MemoryContentInitializer.CreateItemsAndLinkThem(new ItemLinkFactory());
            Dictionary<Guid, MemoryItem> dataDictionary = new Dictionary<Guid, MemoryItem>();

            foreach (var item in referenceData)
            {
                dataDictionary.Add(Guid.NewGuid(), item);
            }

            Omniscience allKnowledge = new Omniscience(dataDictionary);

            for (int i = 0; i < 50; i++)
            {
                Person newPerson = new Person("NPC" + i, Gender.Agender, Sex.Undefined, Orientation.Asexual, Guid.Empty);
               var newNpc = new global::RNPC.Core.Character(newPerson, Archetype.None);

                for (int j = 0; j < referenceData.Count; j++)
                {
                    if(RandomValueGenerator.GeneratePercentileIntegerValue() < 34)
                       newNpc.AddContentToLongTermMemory(dataDictionary.ElementAt(j).Value);
                }

                allKnowledge.AddFollower(newNpc);
            }
            //ACT
            controller.WriteToFile(allKnowledge);
            //ASSERT
            controller.FileExists();
        }

        [TestMethod]
        public void ReadFromFile_OmniscienceFile_FileRead()
        {
            //ARRANGE
            OmniscienceFileController controller = new OmniscienceFileController();

            if (!controller.FileExists())
                Assert.Fail();

            //ACT
            var omniscience = controller.ReadFromFile();
            //ASSERT
            Assert.IsNotNull(omniscience);
            Assert.IsTrue(omniscience.MyFollowers.Count == 50);
        }
    }
}