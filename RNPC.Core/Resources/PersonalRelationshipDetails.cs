using System.Collections.Generic;
using RNPC.Core.Enums;

namespace RNPC.Core.Resources
{
    public sealed class PersonalRelationshipDetails
    {
        private struct PersonalRelationshipInformation
        {
            public PersonalRelationshipInformation(string description, PersonalRelationshipType type)
            {
                Description = description;
                Type = type;
            }
            public string Description { get;  }
            public PersonalRelationshipType Type { get; }
        }

        private readonly Dictionary<string, PersonalRelationshipInformation> _details;

        public PersonalRelationshipType GetRelationshipTypeName(string relationshipName)
        {
            return _details[relationshipName].Type;
        }

        public string GetRelationshipDescription(string relationshipName)
        {
            return _details[relationshipName].Description;
        }

        public static PersonalRelationshipDetails Instance { get; } = new PersonalRelationshipDetails();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static PersonalRelationshipDetails()
        {
        }

        private PersonalRelationshipDetails()
        {
            _details =new Dictionary<string, PersonalRelationshipInformation>
            {
                {"Acquaintance", new PersonalRelationshipInformation("acquaintance-of", PersonalRelationshipType.Friendship) },
                {"Associate", new PersonalRelationshipInformation("associate-of", PersonalRelationshipType.Business) },
                {"Aunt", new PersonalRelationshipInformation("aunt-of", PersonalRelationshipType.Family) },
                {"BestFriend", new PersonalRelationshipInformation("best-friend-of", PersonalRelationshipType.Friendship) },
                {"Boss", new PersonalRelationshipInformation("boss-of", PersonalRelationshipType.Hierarchical) },
                {"Boyfriend", new PersonalRelationshipInformation("boyfriend-of", PersonalRelationshipType.Romantic) },
                {"Brother", new PersonalRelationshipInformation("brother-of", PersonalRelationshipType.Family) },
                {"BrotherInLaw", new PersonalRelationshipInformation("brother-in-law-of", PersonalRelationshipType.Family) },
                {"BusinessPartner", new PersonalRelationshipInformation("business-partner-of", PersonalRelationshipType.Business) },
                {"Child", new PersonalRelationshipInformation("child-of", PersonalRelationshipType.Family) },
                {"Daughter", new PersonalRelationshipInformation("daughter-of", PersonalRelationshipType.Family) },
                {"DaughterInLaw", new PersonalRelationshipInformation("daughter-in-law-of", PersonalRelationshipType.Family) },
                {"Employee", new PersonalRelationshipInformation("employee-of", PersonalRelationshipType.Hierarchical) },
                {"Enemy", new PersonalRelationshipInformation("enemy-of", PersonalRelationshipType.Enmity) },
                {"Father", new PersonalRelationshipInformation("father-of", PersonalRelationshipType.Family) },
                {"FatherInLaw", new PersonalRelationshipInformation("father-in-law-of", PersonalRelationshipType.Family) },
                {"Fiancé", new PersonalRelationshipInformation("fiancé-of", PersonalRelationshipType.Romantic) },
                {"Fiancée", new PersonalRelationshipInformation("fiancée-of", PersonalRelationshipType.Romantic) },
                {"Frenemy", new PersonalRelationshipInformation("frenemy-of", PersonalRelationshipType.Enmity) },
                {"Friend", new PersonalRelationshipInformation("friend-of", PersonalRelationshipType.Friendship) },
                {"Girlfriend", new PersonalRelationshipInformation("girlfriend-of", PersonalRelationshipType.Romantic) },
                {"Grandaunt", new PersonalRelationshipInformation("grand-aunt-of", PersonalRelationshipType.Family) },
                {"Grandchild", new PersonalRelationshipInformation("grandchild-of", PersonalRelationshipType.Family) },
                {"Granddaughter", new PersonalRelationshipInformation("granddaughter-of", PersonalRelationshipType.Family) },
                {"Grandfather", new PersonalRelationshipInformation("grandfather-of", PersonalRelationshipType.Family) },
                {"Grandmother", new PersonalRelationshipInformation("grandmother-of", PersonalRelationshipType.Family) },
                {"Grandnephew", new PersonalRelationshipInformation("grandnephew-of", PersonalRelationshipType.Family) },
                {"Grandniece", new PersonalRelationshipInformation("grandniece-of", PersonalRelationshipType.Family) },
                {"Grandparent", new PersonalRelationshipInformation("grandparent-of", PersonalRelationshipType.Family) },
                {"Grandson", new PersonalRelationshipInformation("grandson-of", PersonalRelationshipType.Family) },
                {"Granduncle", new PersonalRelationshipInformation("granduncle-of", PersonalRelationshipType.Family) },
                {"Husband", new PersonalRelationshipInformation("husband-of", PersonalRelationshipType.Family) },
                {"LifePartner", new PersonalRelationshipInformation("life-partner-of", PersonalRelationshipType.Family) },
                {"Lover", new PersonalRelationshipInformation("lover-of", PersonalRelationshipType.Romantic) },
                {"Master", new PersonalRelationshipInformation("master-of", PersonalRelationshipType.Hierarchical) },
                {"Mother", new PersonalRelationshipInformation("mother-of", PersonalRelationshipType.Family) },
                {"MotherInLaw", new PersonalRelationshipInformation("mother-in-law-of", PersonalRelationshipType.Family) },
                {"Nemesis", new PersonalRelationshipInformation("nemesof", PersonalRelationshipType.Enmity) },
                {"Nephew", new PersonalRelationshipInformation("nephew-of", PersonalRelationshipType.Family) },
                {"Niece", new PersonalRelationshipInformation("niece-of", PersonalRelationshipType.Family) },
                {"Parent", new PersonalRelationshipInformation("parent-of", PersonalRelationshipType.Family) },
                {"Sister", new PersonalRelationshipInformation("sister-of", PersonalRelationshipType.Family) },
                {"SisterInLaw", new PersonalRelationshipInformation("sister-in-law-of", PersonalRelationshipType.Family) },
                {"Slave", new PersonalRelationshipInformation("slave-of", PersonalRelationshipType.Hierarchical) },
                {"Son", new PersonalRelationshipInformation("son-of", PersonalRelationshipType.Family) },
                {"SonInLaw", new PersonalRelationshipInformation("son-in-law-of", PersonalRelationshipType.Family) },
                {"Spouse", new PersonalRelationshipInformation("spouse-of", PersonalRelationshipType.Family) },
                {"Stepbrother", new PersonalRelationshipInformation("stepbrother-of", PersonalRelationshipType.Family) },
                {"Stepdaughter", new PersonalRelationshipInformation("stepdaughter-of", PersonalRelationshipType.Family) },
                {"Stepfather", new PersonalRelationshipInformation("stepfather-of", PersonalRelationshipType.Family) },
                {"Stepmother", new PersonalRelationshipInformation("stepmother-of", PersonalRelationshipType.Family) },
                {"Stepsister", new PersonalRelationshipInformation("stepsister-of", PersonalRelationshipType.Family) },
                {"Stepson", new PersonalRelationshipInformation("stepson-of", PersonalRelationshipType.Family) },
                {"Uncle", new PersonalRelationshipInformation("uncle-of", PersonalRelationshipType.Family) },
                {"Wife", new PersonalRelationshipInformation("wife-of", PersonalRelationshipType.Family) },
                {"WorstEnemy", new PersonalRelationshipInformation("worst-enemy-of", PersonalRelationshipType.Enmity) },
            };
        }
    }
}