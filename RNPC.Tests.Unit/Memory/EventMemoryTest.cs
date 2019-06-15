using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.API;
using RNPC.Core.Enums;
using RNPC.Core.Memory;
using RNPC.FileManager;
using RNPC.Tests.Unit.DTO.Memory;
using RNPC.Tests.Unit.Stubs;

namespace RNPC.Tests.Unit.Memory
{
    [TestClass]
    public class EventMemoryTest
    {
        [TestMethod]
        public void FindEventsByType_MemoryLoadedWithEventsAndPersons_TwoOfEach()
        {
            //ARRANGE
            var knowledge = MemoryContentInitializer.CreateEventsAndPersons(new ItemLinkFactory());

            var guineaPig = new Person("Guinea Pig", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            var testCharacter = new global::RNPC.Core.Character(guineaPig, Archetype.None)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            testCharacter.KnowledgeRandomizer = new TestKnowledgeRandomization();

            testCharacter.AddContentToLongTermMemory(knowledge);
            //ACT
            var magical = testCharacter.MyMemory.Events.FindEventsByType(PastEventType.Magical);
            var conflict = testCharacter.MyMemory.Events.FindEventsByType(PastEventType.Conflict);
            //ASSERT
            Assert.AreEqual(2, magical.Count);
            Assert.AreEqual(2, conflict.Count);
        }

        [TestMethod]
        public void FindEventByName_MemoryLoadedWithEventsAndPersons_EventFound()
        {
            string eventName = "The Conjunction of the Spheres";
            //ARRANGE
            var knowledge = MemoryContentInitializer.CreateEventsAndPersons(new ItemLinkFactory());

            var guineaPig = new Person("Guinea Pig", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            var testCharacter = new global::RNPC.Core.Character(guineaPig, Archetype.None)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder(),
                KnowledgeRandomizer = new TestKnowledgeRandomization()
            };

            testCharacter.AddContentToLongTermMemory(knowledge);
            //ACT
            var conjunction = testCharacter.MyMemory.Events.FindEventByName(eventName);
            //ASSERT
            Assert.IsNotNull(conjunction);
            Assert.AreEqual(eventName, conjunction.Name);
        }

        [TestMethod]
        public void FindEventByTypeAndRelatedPerson_MemoryLoadedWithEventsAndPersons_EventFound()
        {
            //ARRANGE
            var knowledge = MemoryContentInitializer.CreateEventsAndPersons(new ItemLinkFactory());

            var guineaPig = new Person("Guinea Pig", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            var testCharacter = new global::RNPC.Core.Character(guineaPig, Archetype.None)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder(),
                KnowledgeRandomizer = new TestKnowledgeRandomization()
            };

            testCharacter.AddContentToLongTermMemory(knowledge);
            //ACT
            var conflicts = testCharacter.MyMemory.Events.FindEventsByTypeAndRelatedPerson(PastEventType.Conflict, "Rilbert");
            //ASSERT
            Assert.IsNotNull(conflicts);
            Assert.AreEqual(2, conflicts.Count);
        }

        [TestMethod]
        public void FindEventByTypeAndRelatedPerson_MemoryLoadedWithEventsAndPersonsUsingPersonObject_EventFound()
        {
            //ARRANGE
            var knowledge = MemoryContentInitializer.CreateEventsAndPersons(new ItemLinkFactory());

            var guineaPig = new Person("Guinea Pig", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            var testCharacter = new global::RNPC.Core.Character(guineaPig, Archetype.None)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder(),
                KnowledgeRandomizer = new TestKnowledgeRandomization()
            };

            testCharacter.AddContentToLongTermMemory(knowledge);
            Person rilbert = (Person)knowledge.FirstOrDefault(item => item.ItemType == MemoryItemType.Person && item.Name == "Rilbert");
            //ACT
            var conflicts = testCharacter.MyMemory.Events.FindEventsByTypeAndRelatedPerson(PastEventType.Conflict, rilbert);
            //ASSERT
            Assert.IsNotNull(conflicts);
            Assert.AreEqual(2, conflicts.Count);
        }
    }
}