using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.API;
using RNPC.Core.Enums;
using RNPC.Core.Memory;
using RNPC.FileManager;
using RNPC.Tests.Unit.DTO.Memory;
using RNPC.Tests.Unit.Stubs;

namespace RNPC.Tests.Unit.Character
{
    [TestClass]
    public class KnowledgeTest
    {
        [TestMethod]
        public void KnowledgeTest_OccupationAndOrigin_AnswerReturned()
        {
            //Where do Vikings come from?
            //from Skellige
            //ARRANGE
            var knowledge = MemoryContentInitializer.CreateItemsAndLinkThem(new ItemLinkFactory());

            var guineaPig = new Person("Guinea Pig", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            var testCharacter = new global::RNPC.Core.Character(guineaPig, Archetype.None, knowledge)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder(),
                KnowledgeRandomizer = new TestKnowledgeRandomization()
            };

            testCharacter.AddContentToLongTermMemory(knowledge);
            //ACT
            var vikings = testCharacter.MyMemory.Occupations.FindOccupationByName("Vikings");
            //ASSERT
            Assert.IsNotNull(vikings);

            Place vikingOrigin = vikings.FindLinkedPlaceByType(OccupationalTieType.OriginatedFrom);

            Assert.IsNotNull(vikingOrigin);
            Assert.IsTrue(vikingOrigin.Name.Contains("Skellige"));
        }

        [TestMethod]
        public void KnowledgeTest_OccupationAndRelationship_AnswerReturned()
        {
            //Who was married to Queen Meve?
            //The King of Temeria
            //ARRANGE
            var knowledge = MemoryContentInitializer.CreateItemsAndLinkThem(new ItemLinkFactory());

            var guineaPig = new Person("Guinea Pig", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            var testCharacter = new global::RNPC.Core.Character(guineaPig, Archetype.None, knowledge)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder(),
                KnowledgeRandomizer = new TestKnowledgeRandomization()
            };

            testCharacter.AddContentToLongTermMemory(knowledge);
            //ACT
            var meve = testCharacter.MyMemory.Persons.FindPersonByName("Meve");
            //ASSERT
            Assert.IsNotNull(meve);

            var husband = meve.FindRelationshipsByType(PersonalRelationshipType.Family).FirstOrDefault(r => r.Description == "wife-of")?.RelatedPerson;

            Assert.IsNotNull(husband);

            var occupation = husband.GetCurrentOccupation();

            Assert.IsNotNull(occupation);
            Assert.AreEqual("King of Temeria", occupation.Name);
        }

        [TestMethod]
        public void KnowledgeTest_FoundationOfAKingdom_AnswerReturned()
        {
            // When was Aedirn founded
            // in 813
            //ARRANGE
            var knowledge = MemoryContentInitializer.CreateItemsAndLinkThem(new ItemLinkFactory());

            var guineaPig = new Person("Guinea Pig", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            var testCharacter = new global::RNPC.Core.Character(guineaPig, Archetype.None, knowledge)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder(),
                KnowledgeRandomizer = new TestKnowledgeRandomization()
            };

            testCharacter.AddContentToLongTermMemory(knowledge);
            //ACT
            var dateOfFoundation = testCharacter.MyMemory.Places.WhenWasThisPlaceFounded("Aedirn");
            //ASSERT
            Assert.IsNotNull(dateOfFoundation);
            Assert.AreEqual(813, dateOfFoundation[0].GetYear());
        }

        [TestMethod]
        public void KnowledgeTest_ConquestOfRedania_AnswerReturned()
        {
            //Who conquered Redania?
            //The Emperor of Nilfgaard conquered Redania
            //ARRANGE
            var knowledge = MemoryContentInitializer.CreateItemsAndLinkThem(new ItemLinkFactory());

            var guineaPig = new Person("Guinea Pig", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            var testCharacter = new global::RNPC.Core.Character(guineaPig, Archetype.None, knowledge)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder(),
                KnowledgeRandomizer = new TestKnowledgeRandomization()
            };

            testCharacter.AddContentToLongTermMemory(knowledge);
            //ACT
            // ReSharper disable once InconsistentNaming
            var Redania = testCharacter.MyMemory.Places.FindPlaceByName("Redania");
            //ASSERT
            Assert.IsNotNull(Redania);

            var theConquest = Redania.FindLinkedEventByType(OccurenceType.Conquest);

            Assert.IsNotNull(theConquest);

            var leader = theConquest.FindOccupationsByInvolvementType(OccupationalInvolvementType.Led);
            Assert.IsNotNull(leader);
            Assert.IsTrue(leader.Count == 1);
            Assert.AreEqual("Imperator", leader[0].Name);
            Debug.Write($"The {leader[0].Name} of the {leader[0].FindLinkedPlaceByType(OccupationalTieType.Led)} led the {theConquest.Name}");
        }

        [TestMethod]
        public void KnowledgeTest_AnyasBirthplace_AnswerReturned()
        {
            //Where was Anya born?
            //Anya was born in Rivia
            //ARRANGE
            var knowledge = new List<MemoryItem>();
            knowledge.AddRange(MemoryContentInitializer.CreatePersonsAndOccupationsForTest(new ItemLinkFactory()));

            var guineaPig = new Person("Guinea Pig", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            var testCharacter = new global::RNPC.Core.Character(guineaPig, Archetype.None, knowledge)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder(),
                KnowledgeRandomizer = new TestKnowledgeRandomization()
            };

            testCharacter.AddContentToLongTermMemory(knowledge);
            //ACT
            // ReSharper disable once InconsistentNaming
            var Anya = testCharacter.MyMemory.Persons.FindPersonByName("Anya");
            //ASSERT
            Assert.IsNotNull(Anya);

            // ReSharper disable once InconsistentNaming
            var Rivia = Anya.FindPlaceByPersonalTieType(PersonalTieType.BornIn);

            Assert.IsNotNull(Rivia);
            Assert.AreEqual("Rivia", Rivia.Name);
        }

        [TestMethod]
        public void KnowledgeTest_AnyasBoyfriend_AnswerReturned()
        {
            //Who is Anya dating?
            //Anya and Henselt are dating
            //ARRANGE
            var knowledge = new List<MemoryItem>();
            knowledge.AddRange(MemoryContentInitializer.CreatePersonsAndOccupationsForTest(new ItemLinkFactory()));

            var guineaPig = new Person("Guinea Pig", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            var testCharacter = new global::RNPC.Core.Character(guineaPig, Archetype.None)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder(),
                KnowledgeRandomizer = new TestKnowledgeRandomization()
            };

            testCharacter.AddContentToLongTermMemory(knowledge);
            //ACT
            // ReSharper disable once InconsistentNaming
            var Anya = testCharacter.MyMemory.Persons.FindPersonByName("Anya");
            //ASSERT
            Assert.IsNotNull(Anya);

            // ReSharper disable once InconsistentNaming
            var relationships = Anya.FindRelationshipsByType(PersonalRelationshipType.Romantic);

            Assert.IsNotNull(relationships);
            Assert.IsTrue(relationships.Count == 1);

            var boyfriend = relationships[0].RelatedPerson;

            Assert.IsNotNull(boyfriend);
            Assert.AreEqual("Hanselt", boyfriend.Name);
        }

        [TestMethod]
        public void KnowledgeTest_RilbertsTrade_AnswerReturned()
        {
            //What is Ribert's trade?
            //Rilbert is a Mason
            //ARRANGE
            var knowledge = new List<MemoryItem>();
            knowledge.AddRange(MemoryContentInitializer.CreatePersonsAndOccupationsForTest(new ItemLinkFactory()));

            var guineaPig = new Person("Guinea Pig", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            var testCharacter = new global::RNPC.Core.Character(guineaPig, Archetype.None, knowledge)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder(),
                KnowledgeRandomizer = new TestKnowledgeRandomization()
            };

            testCharacter.AddContentToLongTermMemory(knowledge);
            //ACT
            // ReSharper disable once InconsistentNaming
            var Rilbert = testCharacter.MyMemory.Persons.FindPersonByName("Rilbert");
            //ASSERT
            Assert.IsNotNull(Rilbert);

            var occupation = Rilbert.GetCurrentOccupation();

            Assert.IsNotNull(occupation);
            Assert.AreEqual("Mason", occupation.Name);
        }

        [TestMethod]
        public void KnowledgeTest_SavingTheKing_AnswerReturned()
        {
            //Who saved the king and where?
            //A mason saved the king from death in Kaedwen
            //ARRANGE
            var knowledge = new List<MemoryItem>();
            knowledge.AddRange(MemoryContentInitializer.CreateOccupationsAndEventsForTest(new ItemLinkFactory()));

            var guineaPig = new Person("Guinea Pig", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            var testCharacter = new global::RNPC.Core.Character(guineaPig, Archetype.None, knowledge)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder(),
                KnowledgeRandomizer = new TestKnowledgeRandomization()
            };

            testCharacter.AddContentToLongTermMemory(knowledge);
            //ACT
            // ReSharper disable once InconsistentNaming
            var king = testCharacter.MyMemory.Occupations.FindOccupationByName("King");
            //ASSERT
            Assert.IsNotNull(king);

            var linkedEvents = king.FindLinkedEventsByInvolvementType(OccupationalInvolvementType.WasCaughtIn);

            Assert.IsNotNull(linkedEvents);
            Assert.IsTrue(linkedEvents.Count == 1);

            // ReSharper disable once InconsistentNaming
            var Kaedwen = linkedEvents[0].FindLinkedPlaceByOccurenceType(OccurenceType.HappenedIn);

            Assert.IsNotNull(Kaedwen);
            Assert.AreEqual("Kaedwen", Kaedwen.Name);

            var linkedOccupation = linkedEvents[0].FindOccupationsByInvolvementType(OccupationalInvolvementType.ParticipatedIn);
            Assert.IsNotNull(linkedOccupation);
            Assert.IsTrue(linkedOccupation.Count == 1);
            Assert.AreEqual("Mason", linkedOccupation[0].Name);
        }

        [TestMethod]
        public void KnowledgeTest_BattleOfCintra_AnswerReturned()
        {
            //Who conquered Cintra?
            //Nilfgaard conquered Cintra at the battle of Cintra
            //ARRANGE
            var knowledge = MemoryContentInitializer.CreateItemsAndLinkThem(new ItemLinkFactory());

            var guineaPig = new Person("Guinea Pig", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            var testCharacter = new global::RNPC.Core.Character(guineaPig, Archetype.None, knowledge)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder(),
                KnowledgeRandomizer = new TestKnowledgeRandomization()
            };

            testCharacter.AddContentToLongTermMemory(knowledge);
            //ACT
            // ReSharper disable once InconsistentNaming
            var Cintra = testCharacter.MyMemory.Places.FindPlaceByName("Cintra");
            //ASSERT
            Assert.IsNotNull(Cintra);

            var conquest = Cintra.FindLinkedEventByType(OccurenceType.Conquest);

            Assert.IsNotNull(conquest);

            var conqueringKing = conquest.FindOccupationsByInvolvementType(OccupationalInvolvementType.Led);

            Assert.IsNull(conqueringKing);

            var emperator = Cintra.FindOccupationsByOccupationalTieType(OccupationalTieType.Conquered);

            Assert.IsNotNull(emperator);
            Assert.IsTrue(emperator.Count == 1);
            Assert.AreEqual("Imperator", emperator[0].Name);
        }

        [TestMethod]
        public void KnowledgeTest_TheConjunction_AnswerReturned()
        {
            //Where was the Conjunction of the Spheres?
            //The Continent saw the Conjunction of the Spheres
            //ARRANGE
            var knowledge = MemoryContentInitializer.CreateItemsAndLinkThem(new ItemLinkFactory());

            var guineaPig = new Person("Guinea Pig", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            var testCharacter = new global::RNPC.Core.Character(guineaPig, Archetype.None, knowledge)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder(),
                KnowledgeRandomizer = new TestKnowledgeRandomization()
            };

            testCharacter.AddContentToLongTermMemory(knowledge);
            //ACT
            var theConjuction = testCharacter.MyMemory.Events.FindEventByName("Conjunction");
            //ASSERT
            Assert.IsNotNull(theConjuction);

            var continent = theConjuction.FindLinkedPlaceByOccurenceType(OccurenceType.HappenedIn);

            Assert.IsNotNull(continent);
            Assert.AreEqual("The Continent", continent.Name);
        }

        [TestMethod]
        public void KnowledgeTest_TheNorthernWars_AnswerReturned()
        {
            //What battle did Geralt fight in?
            //Geralt fought in the battle against Nilfgaard
            //ARRANGE
            var knowledge = MemoryContentInitializer.CreateItemsAndLinkThem(new ItemLinkFactory());

            var guineaPig = new Person("Guinea Pig", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            var testCharacter = new global::RNPC.Core.Character(guineaPig, Archetype.None, knowledge)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder(),
                KnowledgeRandomizer = new TestKnowledgeRandomization()
            };

            testCharacter.AddContentToLongTermMemory(knowledge);
            //ACT
            // ReSharper disable once InconsistentNaming
            var Geralt = testCharacter.MyMemory.Persons.FindPersonByName("Geralt");
            //ASSERT
            Assert.IsNotNull(Geralt);

            var eventsInvolved = Geralt.FindLinkedEventsByInvolvementType(PersonalInvolvementType.FoughtIn);

            Assert.IsNotNull(eventsInvolved);
            Assert.IsTrue(eventsInvolved.Count != 0);
            Assert.AreEqual("The Northern  Wars", eventsInvolved[0].Name);
        }
    }
}