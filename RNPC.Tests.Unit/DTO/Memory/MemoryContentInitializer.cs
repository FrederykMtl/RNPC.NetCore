using System;
using System.Collections.Generic;
using RNPC.Core;
using RNPC.Core.Enums;
using RNPC.Core.GameTime;
using RNPC.Core.Memory;

// ReSharper disable InconsistentNaming

namespace RNPC.Tests.Unit.DTO.Memory
{
    public static class MemoryContentInitializer
    {
        #region Description Text

        private const string WitcherDesc = "A witcher (also wiccan or hexer or in the Elder Speech: vatt\'ghern) is someone who has undergone " +
                                           "extensive training, ruthless mental and physical conditioning, and mysterious rituals (which take " +
                                           "place at \"witcher schools\" such as Kaer Morhen) in preparation for becoming an itinerant monsterslayer for hire.";

        private const string GeraltDesc = @"Geralt is a witcher that kills monster in exchange for money. Known to be honorable, he follows his own code.";

        private const string FoltestDesc = "Foltest was the king of Temeria, prince of Sodden, sovereign of Pontaria and Mahakam as well as the senior " +
                                           "protector of Brugge and Ellander. He was the son of King Medell and Queen Sancia of Sodden, making him a " +
                                           "member of the Temerian Dynasty.";

        private const string CalantheDesc = "Calanthe Fiona Riannon of Cintra, known also as the 'Lioness of Cintra' and 'Ard Rhena'('High Queen' in Elder Speech)," +
                                            "was a queen of Cintra, the mother of Pavetta, and grandmother of Ciri.Her nephew, Crach an Craite, called her 'Modron'." +
                                            "She was known for her bravery and beauty.";

        private const string PavettaDesc = "Pavetta Fiona Elen was the granddaughter of Queen Adalia and daughter of Queen Calanthe and King Roegner of Ebbing. " +
                                           "She was a Source and the mother of Ciri.";

        private const string CiriDesc = "Cirilla Fiona Elen Riannon (also known as Ciri), was born in 1253, and most likely during the Belleteyn holiday." +
                                        " She was the sole princess of Cintra, the daughter of Pavetta and Emhyr var Emreis (who was using the alias 'Duny'" +
                                        " at the time) as well as Queen Calanthe's granddaughter.";

        private const string ConjunctionDesc = "The Conjunction of the Spheres is a cataclysm that trapped many 'unnatural' creatures" +
                                               " in this dimension, including ghouls, graveirs, and vampires.";

        private const string MigrationDesc = "Aen Seidhe elves arrive on their white ships";

        private const string SoddenDesc = "Sodden was a former kingdom and is located around the Yaruga river between Cintra, Brugge," +
                                          " and Riverdell. In the aftermath of the Nilfgaardian Wars, Sodden was split between Nilfgaard" +
                                          " and the Northern Kingdoms into Upper Sodden and Lower Sodden. However, with the Peace of " +
                                          "Cintra, the two halves were recombined into Sodden and it became a vassal state of Temeria.";

        private const string TemeriaDesc = "The kingdom of Temeria lies south of the Pontar river. " +
                                           "Its neighbors are Redania, Kerack, Kaedwen, Aedirn, Mahakam, Lower Sodden, and Cidaris.";
        private const string VizimaDesc = "Vizima or Wyzim is the capital city of Temeria, one of the Northern Kingdoms" +
                                          " and the seat of king Foltest at the intersection of important trade routes.";

        private const string ContinentDesc = "The unnamed Continent is where the Northern Kingdoms and the Nilfgaardian Empire lie.";
        private const string FenCaernDesc = "Fen Carn (Elder Speech: Meadow of the Barrows), An old elven cemetary";
        private const string NorthernWarsDesc = "The Northern Wars were a series of wars fought between the southern Nilfgaardian Empire and coalitions of Northern Kingdoms in the 13th century.";
        private const string BattleOfBrennaDesc = "The Battle of Brenna is one of the most famous battles of the Nilfgaard Wars.";
        private const string TheRivianPogromDesc = "The Rivian Pogrom is an incident which began in the marketplace of the city of Rivia.";
        private const string IndependanceOfKovirDesc = "The Appanage of Kovir declares itself as the independent Kingdom of Kovir, resulting inking war between the new kingdom and Redania.";
        private const string KingdomOfKovirDesc = "The Kingdom of Kovir and Poviss (also known shortly as Kovir and Poviss or simply Kovir) is one of the Northern Kingdoms located on the Gulf of Praxeda.";

        #endregion

        /// <summary>
        /// This method will return a full list of intialized and linked items of each category. For test purposes.
        /// </summary>
        /// <param name="itemLinkFactory">The factory that creates linked items</param>
        /// <returns>A list of all item types.</returns>
        public static List<MemoryItem> CreateItemsAndLinkThem(IItemLinkFactory itemLinkFactory)
        {
            //Organizations
            var witchers = new Organization("The Order of Witchers", OrganizationType.Trade, Guid.NewGuid(), WitcherDesc)
            {
                Started = new CustomDateTime(1031)
            };
            var governmentOfNilfgaard = new Organization("The Kingdom of Nilfgaard", OrganizationType.Government, Guid.NewGuid());
            var EmperorOfNilfgaard = new Occupation("Imperator", OccupationType.Political, Guid.NewGuid(), "A shadowy figure");

            var governmentOfCintra = new Organization("The Kingdom of Cintra", OrganizationType.Government, Guid.NewGuid());

            var organizations = new List<Organization> {witchers, governmentOfNilfgaard, governmentOfCintra};

            //Occupations
            //Geralt of Rivia, witcher.
            var GeraltOfRivia = new Person("Geralt of Rivia", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid(), GeraltDesc);

            var witcherGeralt = new Occupation("Witcher",GeraltOfRivia, OccupationType.Independant, Guid.NewGuid(), WitcherDesc)
            {
                Started = new CustomDateTime(1031)
            };

            GeraltOfRivia.AddOccupation(witcherGeralt);

            List<MemoryItem> linkObjects = new List<MemoryItem>();
            List<MemoryItemLink> intermediateObjects = new List<MemoryItemLink>();

            var association1 = itemLinkFactory.CreateAssociationBetweenPersonAndOrganization(GeraltOfRivia, witchers, AssociationType.Member);
            linkObjects.Add(association1);

            //Foltest, king of Temeria
            var Foltest = new Person("Foltest", Guid.NewGuid(), FoltestDesc)
            {
                Gender = Gender.Male,
                Sex = Sex.Male
            };

            Occupation kingOfTemeria = new Occupation("King of Temeria", Foltest, OccupationType.Political, Guid.NewGuid());

            Foltest.AddOccupation(kingOfTemeria);

            Person Meve = new Person("Meve", Guid.NewGuid());
            Occupation queen = new Occupation("Queen", OccupationType.Political, Guid.NewGuid());
            Meve.AddOccupation(queen);

            var relationship1 = itemLinkFactory.CreateRelationshipBetweenTwoPersons(Meve, Foltest, PersonalRelationshipTypeName.Wife, PersonalRelationshipTypeName.Husband);
            linkObjects.AddRange(relationship1);

            //Calanthe, The Lion Queen of Cintra
            var Calanthe = new Person("Calanthe", Gender.Female, Sex.Female, Orientation.Bisexual, Guid.NewGuid(), new CustomDateTime(1218), CalantheDesc)
            {
                DateOfDeath = new CustomDateTime(1265)
            };

            Occupation queenOfCintra = new Occupation("Queen of Cintra", Calanthe, OccupationType.Political, Guid.NewGuid(), new CustomDateTime(1232), new CustomDateTime(1263));

            Calanthe.AddOccupation(queenOfCintra);

            var association2 = itemLinkFactory.CreateAssociationBetweenPersonAndOrganization(Calanthe, governmentOfCintra, AssociationType.Leader, new CustomDateTime(1232), new CustomDateTime(1263));
            linkObjects.Add(association2);

            //Princess Pavetta of Cintra
            var Pavetta = new Person("Princess Pavetta", Gender.Female, Sex.Female, Orientation.Straight, Guid.NewGuid(), new CustomDateTime(1237), PavettaDesc)
            {
                DateOfDeath = new CustomDateTime(1257)
            };

            //Ciri, princess of Cintra. No, not the Apple thing.
            var Ciri = new Person("Princess Cirilla Fiona Elen Riannon", Gender.Female, Sex.Female, Orientation.Straight, Guid.NewGuid(), new CustomDateTime(1253, 5, 1), CiriDesc);

            var relationships1 = itemLinkFactory.CreateRelationshipBetweenTwoPersons(Calanthe, Pavetta, PersonalRelationshipTypeName.Mother, PersonalRelationshipTypeName.Daughter);
            var relationships2 = itemLinkFactory.CreateRelationshipBetweenTwoPersons(Pavetta, Ciri, PersonalRelationshipTypeName.Mother, PersonalRelationshipTypeName.Daughter, new CustomDateTime(1253, 5, 1));

            linkObjects.AddRange(relationships1);
            linkObjects.AddRange(relationships2);

            var persons = new List<Person> {GeraltOfRivia, Foltest, Calanthe, Pavetta, Ciri, Meve};

            //Events
            PastEvent ageOfMigration = new PastEvent("the Age of Migration", Guid.NewGuid(), MigrationDesc)
            {
                Type = PastEventType.Social,
                Started = new CustomDateTime(-2230),
                Ended = new CustomDateTime(-2230)
            };

            PastEvent theConjunction = new PastEvent("The Conjunction of the Spheres", Guid.NewGuid(), ConjunctionDesc, new CustomDateTime(-230), new CustomDateTime(-230))
            {
                Type = PastEventType.Magical
            };

            PastEvent theResurrection = new PastEvent("The Resurrection", Guid.NewGuid(), "", new CustomDateTime(1))
            {
                Type = PastEventType.Magical,
                Ended = new CustomDateTime(1),
            };

            var link1 = itemLinkFactory.CreateLinkBetweenTwoEvents(theResurrection, theConjunction, EventRelationshipType.Preceded);
            linkObjects.AddRange(link1);

            PastEvent NilfgaardInvasion = new PastEvent("The Northern  Wars", Guid.NewGuid(), NorthernWarsDesc, new CustomDateTime(1239));
            PastEvent conquestOfRedania = new PastEvent("Conquest of Redania", Guid.NewGuid());
            PastEvent battleOfBrenna = new PastEvent("The Battle Of Brenna", Guid.NewGuid(), BattleOfBrennaDesc, new CustomDateTime(1268, 03, 01));
            PastEvent conquestOfCintra = new PastEvent("Conquest of Cintra", Guid.NewGuid());

            var link2 = itemLinkFactory.CreateLinkBetweenTwoEvents(NilfgaardInvasion, battleOfBrenna, EventRelationshipType.Included);
            linkObjects.AddRange(link2);

            var involvement1 = itemLinkFactory.CreateInvolvementBetweenPersonAndEvent(Foltest, NilfgaardInvasion, PersonalInvolvementType.WasCaughtIn);
            var involvement2 = itemLinkFactory.CreateInvolvementBetweenOccupationAndEvent(queenOfCintra, NilfgaardInvasion, OccupationalInvolvementType.DiedDuring);

            var involvement = itemLinkFactory.CreateInvolvementBetweenPersonAndEvent(GeraltOfRivia, NilfgaardInvasion, PersonalInvolvementType.FoughtIn);

            intermediateObjects.Add(involvement);
            intermediateObjects.Add(involvement1);
            intermediateObjects.Add(involvement2);

            PastEvent theRivianPogrom = new PastEvent("Rivian Pogrom", Guid.NewGuid(), TheRivianPogromDesc, new CustomDateTime(1268, 6, 6));

            itemLinkFactory.CreateInvolvementBetweenPersonAndEvent(GeraltOfRivia, theRivianPogrom, PersonalInvolvementType.DiedDuring, new CustomDateTime(1268, 6, 6));

            PastEvent independenceOfKovir = new PastEvent("Independence of Kovir", Guid.NewGuid(), IndependanceOfKovirDesc, new CustomDateTime(1140));

            Occupation kingOfRedania = new Occupation("King of Redania", OccupationType.Political, Guid.NewGuid(), "The King of Redania was known to wage war against Kovir when they declared their independance.");

            var involvement4 = itemLinkFactory.CreateInvolvementBetweenOccupationAndEvent(kingOfRedania, independenceOfKovir, OccupationalInvolvementType.WasCaughtIn, new CustomDateTime(1140));
            intermediateObjects.Add(involvement4);

            var events = new List<PastEvent> {ageOfMigration, theConjunction, theResurrection, NilfgaardInvasion, battleOfBrenna, theRivianPogrom, independenceOfKovir, conquestOfRedania, conquestOfCintra };

            //Places
            Place theContinent = new Place("The Continent", Guid.NewGuid(), ContinentDesc)
            {
                Coordinates = new Coordinates { X = 1, Y = 1, Z = 1 },
                Type = PlaceType.Continent
            };

            var conjunctionOccurence = itemLinkFactory.CreateOccurenceBetweenEventAndPlace(theConjunction, theContinent, OccurenceType.HappenedIn);
            intermediateObjects.Add(conjunctionOccurence);

            var occurence2 = itemLinkFactory.CreateOccurenceBetweenEventAndPlace(NilfgaardInvasion, theContinent, OccurenceType.HappenedIn);
            intermediateObjects.Add(occurence2);

            Place Temeria = new Place("Temeria", Guid.NewGuid(), new Coordinates { X = -700, Y = 333, Z = -2 }, TemeriaDesc)
            {
                Type = PlaceType.Kingdom
            };

            var link3 = itemLinkFactory.CreateLinkBetweenTwoPlaces(theContinent, Temeria, GeographicRelationshipType.Includes);
            var tie1 = itemLinkFactory.CreateTieBetweenPersonAndPlace(Foltest, Temeria, PersonalTieType.Led);

            linkObjects.AddRange(link3);
            linkObjects.Add(tie1);

            Place kingdomOfRedania = new Place("Redania", Guid.NewGuid())
            {
                Type = PlaceType.Kingdom
            };

            var redaniaLink = itemLinkFactory.CreateTieBetweenOccupationAndPlace(kingOfRedania, kingdomOfRedania, OccupationalTieType.Led);
            intermediateObjects.Add(redaniaLink);

            var conquest = itemLinkFactory.CreateOccurenceBetweenEventAndPlace(conquestOfRedania, kingdomOfRedania, OccurenceType.Conquest);

            intermediateObjects.Add(conquest);
            itemLinkFactory.CreateInvolvementBetweenOccupationAndEvent(EmperorOfNilfgaard, conquestOfRedania, OccupationalInvolvementType.Led);

            Place Sodden = new Place("Sodden", Guid.NewGuid(), SoddenDesc)
            {
                Type = PlaceType.State,
                Coordinates = new Coordinates { X = -1023, Y = 566, Z = 50 }
            };

            var link4 = itemLinkFactory.CreateLinkBetweenTwoPlaces(Temeria, Sodden, GeographicRelationshipType.Includes);

            linkObjects.AddRange(link4);

            Place Vizima = new Place("Vizima", Guid.NewGuid(), new Coordinates { X = -746, Y = 656, Z = 142 }, VizimaDesc, new CustomDateTime(849))
            {
                Type = PlaceType.City
            };

            var link5 = itemLinkFactory.CreateLinkBetweenTwoPlaces(Temeria, Vizima, GeographicRelationshipType.Includes, new CustomDateTime(849));

            linkObjects.AddRange(link5);

            Place theNecropolis = new Place("The necropolis", Guid.NewGuid(), FenCaernDesc)
            {
                Type = PlaceType.Cemetary,
                Creation = new CustomDateTime(760)
            };

            Place Cintra = new Place("Cintra", Guid.NewGuid())
            {
                Type = PlaceType.Kingdom,
            };

            var tie2 = itemLinkFactory.CreateTieBetweenPersonAndPlace(Calanthe, Cintra, PersonalTieType.Led, new CustomDateTime(1232), new CustomDateTime(1263));
            var tie3 = itemLinkFactory.CreateTieBetweenOccupationAndPlace(queenOfCintra, Cintra, OccupationalTieType.DiedIn);
            var occurence3 = itemLinkFactory.CreateOccurenceBetweenEventAndPlace(conquestOfCintra, Cintra, OccurenceType.Conquest);
            var tie5 = itemLinkFactory.CreateTieBetweenOccupationAndPlace(EmperorOfNilfgaard, Cintra, OccupationalTieType.Conquered);

            linkObjects.Add(tie2);
            linkObjects.Add(tie3);

            intermediateObjects.Add(occurence3);
            intermediateObjects.Add(tie5);

            Place Nilfgaard = new Place("Nilfgaard", Guid.NewGuid())
            {
                Type = PlaceType.Kingdom,
            };

            //TODO
            var link6 = itemLinkFactory.CreateLinkBetweenTwoPlaces(Cintra, Nilfgaard, GeographicRelationshipType.PartOf);
            var link7 = itemLinkFactory.CreateLinkBetweenTwoPlaces(Nilfgaard, Temeria, GeographicRelationshipType.SouthOf);
            var tie4 = itemLinkFactory.CreateTieBetweenOccupationAndPlace(EmperorOfNilfgaard, Nilfgaard, OccupationalTieType.Led);

            linkObjects.AddRange(link6);
            linkObjects.AddRange(link7);

            intermediateObjects.Add(tie4);

            Place Skellige = new Place("The Isles of Skellige", Guid.NewGuid());
            Occupation Vikings = new Occupation("Vikings", OccupationType.Independant, Guid.NewGuid(), "Brutal warriors");

            var link8 = itemLinkFactory.CreateTieBetweenOccupationAndPlace(Vikings, Skellige, OccupationalTieType.OriginatedFrom);
            linkObjects.Add(link8);

            var Aedirn = new Place("Aedirn", Guid.NewGuid())
            {
                Type = PlaceType.Kingdom,
                Creation = new CustomDateTime(813)
            };

            var foundationOfAedirn = new PastEvent("The Foundation of Aedirn", Guid.NewGuid(), "", new CustomDateTime(813), new CustomDateTime(813)) {Type = PastEventType.Political};
            events.Add(foundationOfAedirn);

            var link9 = itemLinkFactory.CreateOccurenceBetweenEventAndPlace(foundationOfAedirn, Aedirn, OccurenceType.Creation);
            linkObjects.Add(link9);

            Place kingdomOfKovir = new Place("Kovir", Guid.NewGuid(),  new Coordinates(), KingdomOfKovirDesc, new CustomDateTime(1140))
            {
                Type = PlaceType.Kingdom
            };

            var involvement3 = itemLinkFactory.CreateTieBetweenOccupationAndPlace(kingOfRedania, kingdomOfKovir, OccupationalTieType.FoughtAgainst, new CustomDateTime(1140));
            intermediateObjects.Add(involvement3);

            var occurence1 = itemLinkFactory.CreateOccurenceBetweenEventAndPlace(independenceOfKovir, kingdomOfKovir, OccurenceType.Creation, new CustomDateTime(1140));
            intermediateObjects.Add(occurence1);

            var places = new List<Place> {theContinent, Temeria, Sodden, Vizima, theNecropolis, Cintra, Nilfgaard, kingdomOfKovir, Skellige, Aedirn, kingdomOfRedania };
            var occupations = new List<Occupation> { EmperorOfNilfgaard, queenOfCintra, kingOfRedania, kingOfTemeria, witcherGeralt, Vikings, queen};

            var items = new List<MemoryItem>();
            items.AddRange(persons);
            items.AddRange(events);
            items.AddRange(places);
            items.AddRange(organizations);
            items.AddRange(occupations);
            items.AddRange(linkObjects);
            items.AddRange(intermediateObjects);

            return items;
        }

        /// <summary>
        /// A list of Places for test purposes
        /// </summary>
        /// <param name="itemLinkFactory"></param>
        /// <returns></returns>
        public static List<MemoryItem> CreatePlacesAndPersons(IItemLinkFactory itemLinkFactory)
        {
            var items = new List<MemoryItem>();

            for (int i = 0; i < 25; i++)
            {
                Place newPlace = new Place("Place" + i, Guid.NewGuid());
                items.Add(newPlace);
                Person newPerson = new Person("Person" + i, Guid.NewGuid());
                items.Add(newPerson);
                var bornIn = itemLinkFactory.CreateTieBetweenPersonAndPlace(newPerson, newPlace, PersonalTieType.BornIn);
                items.Add(bornIn);
            }
            return items;
        }

        /// <summary>
        /// Evvents and links between them
        /// </summary>
        /// <returns>A list of past events</returns>
        public static List<MemoryItem> CreateEventsAndPersons(IItemLinkFactory itemLinkFactory)
        {
            var eventsAndPersons = new List<MemoryItem>();

            PastEvent event1 = new PastEvent("the Age of Migration", Guid.NewGuid(), MigrationDesc)
            {
                Type = PastEventType.Social,
                Started = new CustomDateTime(-2230),
                Ended = new CustomDateTime(-2230)
            };

            PastEvent event2 = new PastEvent("The Conjunction of the Spheres", Guid.NewGuid(), ConjunctionDesc)
            {
                Type = PastEventType.Magical,
                Started = new CustomDateTime(-230),
                Ended = new CustomDateTime(-230)
            };

            PastEvent event3 = new PastEvent("The Resurrection", Guid.NewGuid())
            {
                Type = PastEventType.Magical,
                Started = new CustomDateTime(1),
                Ended = new CustomDateTime(1),
            };
            PastEvent event4 = new PastEvent("The Nilfgaardian invasion", Guid.NewGuid())
            {
                Type = PastEventType.Battle,
                Description = "Invasion by Nilfgaard"
            };

            EventRelationship link1 = new EventRelationship(event2, EventRelationshipType.Preceded, Guid.NewGuid());
            event3.AddEventLink(link1);

            EventRelationship link2 = new EventRelationship(event4, EventRelationshipType.Followed, Guid.NewGuid());
            event3.AddEventLink(link2);

            Person Anya = new Person("Anya", Gender.Female, Sex.Female, Orientation.Straight, Guid.NewGuid(), new CustomDateTime(1166, 6, 7));
            Person Hanselt = new Person("Hanselt", Gender.Male, Sex.Male, Orientation.Bisexual, Guid.NewGuid(), new CustomDateTime(1159, 3, 19));
            Person Rilbert = new Person("Rilbert", Guid.NewGuid());

            PastEvent conflict1 = new PastEvent("Hanselt and Rilbert had a fight in a tavern", Guid.NewGuid())
            {
                Type = PastEventType.Conflict,
                Description = "There was enmity between Hanselt and Rilbert fought in a tavern",
                Started = new CustomDateTime(1179, 5, 11),
                Ended = new CustomDateTime(1179, 5, 11)
            };

            PastEvent conflict2 = new PastEvent("Hanselt and Rilbert had a fight in a tavern", Guid.NewGuid())
            {
                Type = PastEventType.Conflict,
                Description = "Hanselt grew jealous of Rilbert after Anya fell in love with Rilbert",
                Started = new CustomDateTime(1183, 2, 30)
            };

            itemLinkFactory.CreateInvolvementBetweenPersonAndEvent(Hanselt, conflict1, PersonalInvolvementType.ParticipatedIn);
            itemLinkFactory.CreateInvolvementBetweenPersonAndEvent(Rilbert, conflict1, PersonalInvolvementType.ParticipatedIn);

            itemLinkFactory.CreateInvolvementBetweenPersonAndEvent(Hanselt, conflict2, PersonalInvolvementType.ParticipatedIn);
            itemLinkFactory.CreateInvolvementBetweenPersonAndEvent(Rilbert, conflict2, PersonalInvolvementType.ParticipatedIn);

            eventsAndPersons.Add(event1);
            eventsAndPersons.Add(event2);
            eventsAndPersons.Add(event3);
            eventsAndPersons.Add(event4);
            eventsAndPersons.Add(Anya);
            eventsAndPersons.Add(Hanselt);
            eventsAndPersons.Add(Rilbert);
            eventsAndPersons.Add(conflict1);
            eventsAndPersons.Add(conflict2);

            return eventsAndPersons;
        }

        /// <summary>
        /// A list of Places for test purposes
        /// </summary>
        /// <param name="itemLinkFactory"></param>
        /// <returns></returns>
        public static List<MemoryItem> CreatePlaces(IItemLinkFactory itemLinkFactory)
        {
            var places = new List<MemoryItem>();

            Place place1 = new Place("The Continent", Guid.NewGuid(), ContinentDesc)
            {
                Coordinates = new Coordinates { X = 1, Y = 1, Z = 1 },
                Type = PlaceType.Continent
            };

            Place place2 = new Place("Temeria", Guid.NewGuid(), TemeriaDesc)
            {
                Type = PlaceType.Kingdom,
                Coordinates = new Coordinates { X = -700, Y = 333, Z = -2 }
            };

            itemLinkFactory.CreateLinkBetweenTwoPlaces(place1, place2, GeographicRelationshipType.Includes);

            Place place2A = new Place("Sodden", Guid.NewGuid(), SoddenDesc)
            {
                Type = PlaceType.State,
                Coordinates = new Coordinates { X = -1023, Y = 566, Z = 50 }
            };

            itemLinkFactory.CreateLinkBetweenTwoPlaces(place2, place2A, GeographicRelationshipType.Includes);

            Place place2B = new Place("Vizima", Guid.NewGuid(), VizimaDesc)
            {
                Type = PlaceType.City,
                Coordinates = new Coordinates { X = -746, Y = 656, Z = 142 }
            };

            itemLinkFactory.CreateLinkBetweenTwoPlaces(place2, place2B, GeographicRelationshipType.Includes);

            Place place3 = new Place("The necropolis", Guid.NewGuid(), FenCaernDesc)
            {
                Type = PlaceType.Cemetary,
                Creation = new CustomDateTime(760)
            };

            Place place4 = new Place("Cintra", Guid.NewGuid())
            {
                Type = PlaceType.Kingdom,
            };

            Place place5 = new Place("Nilfgaard", Guid.NewGuid())
            {
                Type = PlaceType.Kingdom,
            };

            itemLinkFactory.CreateLinkBetweenTwoPlaces(place4, place5, GeographicRelationshipType.PartOf);
            itemLinkFactory.CreateLinkBetweenTwoPlaces(place5, place2, GeographicRelationshipType.SouthOf);

            places.Add(place1);
            places.Add(place2);
            places.Add(place2A);
            places.Add(place2B);
            places.Add(place3);
            places.Add(place4);
            places.Add(place5);

            return places;
        }

        /// <summary>
        /// A list of persons for test purposes
        /// </summary>
        /// <param name="itemLinkFactory"></param>
        /// <returns></returns>
        public static List<MemoryItem> CreatePersonsAndOccupationsForTest(IItemLinkFactory itemLinkFactory)
        {
            var itemList = new List<MemoryItem>();

            Person Anya = new Person("Anya", Gender.Female, Sex.Female, Orientation.Straight, Guid.NewGuid(), new CustomDateTime(1166, 6, 7));
            Person Hanselt = new Person("Hanselt", Gender.Male, Sex.Male, Orientation.Bisexual, Guid.NewGuid(), new CustomDateTime(1159, 3, 19));
            Person Rilbert = new Person("Rilbert", Guid.NewGuid());

            Place Rivia = new Place("Rivia", Guid.NewGuid());
            Occupation mason = new Occupation("Mason", Rilbert, OccupationType.Contractor, Guid.NewGuid());
            Rilbert.AddOccupation(mason);

            itemLinkFactory.CreateRelationshipBetweenTwoPersons(Anya, Hanselt, PersonalRelationshipTypeName.Girlfriend, PersonalRelationshipTypeName.Boyfriend, new CustomDateTime(1185));
            itemLinkFactory.CreateTieBetweenPersonAndPlace(Anya, Rivia, PersonalTieType.BornIn);

            itemList.Add(Anya);
            itemList.Add(Hanselt);
            itemList.Add(Rilbert);
            itemList.Add(Rivia);
            itemList.Add(mason);

            return itemList;
        }

        /// <summary>
        /// A list of persons for test purposes
        /// </summary>
        /// <param name="itemLinkFactory"></param>
        /// <returns></returns>
        public static List<MemoryItem> CreateOccupationsAndEventsForTest(IItemLinkFactory itemLinkFactory)
        {
            var items = new List<MemoryItem>();

            Place Kaedwen = new Place("Kaedwen", Guid.NewGuid());
            Occupation mason = new Occupation("Mason", OccupationType.Contractor, Guid.NewGuid());
            Occupation king = new Occupation("King", OccupationType.Political, Guid.NewGuid());
            PastEvent savingTheKing = new PastEvent("The king was saved!", Guid.NewGuid())
            {
                Type = PastEventType.Life
            };

            items.Add(Kaedwen);
            items.Add(mason);
            items.Add(king);
            items.Add(savingTheKing);

            itemLinkFactory.CreateOccurenceBetweenEventAndPlace(savingTheKing, Kaedwen, OccurenceType.HappenedIn);
            itemLinkFactory.CreateInvolvementBetweenOccupationAndEvent(mason, savingTheKing, OccupationalInvolvementType.ParticipatedIn);
            itemLinkFactory.CreateInvolvementBetweenOccupationAndEvent(king, savingTheKing, OccupationalInvolvementType.WasCaughtIn);

            return items;
        }


        /// <summary>
        /// A sample list of Organizations
        /// </summary>
        /// <returns>A list of Organizations</returns>
        public static List<MemoryItem> CreateOrganizations()
        {
            var organizations = new List<MemoryItem>();

            var witchers = new Organization("The Order of Witchers", OrganizationType.Trade, Guid.NewGuid())
            {
                Started = new CustomDateTime(1031)
            };

            var nilfgaard = new Organization("The Kingdom of Nilfgaard", OrganizationType.Government, Guid.NewGuid());


            var cintra = new Organization("The Kingdom of Cintra", OrganizationType.Government, Guid.NewGuid());

            organizations.Add(witchers);
            organizations.Add(nilfgaard);
            organizations.Add(cintra);

            return organizations;
        }
    }
}
