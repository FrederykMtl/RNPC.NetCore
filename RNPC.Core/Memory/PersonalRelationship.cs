using System;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.Memory
{
    /// <summary>
    /// Establishes a relationship between two people
    /// The name will describe the terms for the participants in the relationship
    /// while the description will describe the relationship from the first party to the second
    /// </summary>
    /// <inheritdoc />
    [Serializable]
    public class PersonalRelationship : MemoryItemRelationship, IDisposable
    {
        //People involved in the relationship
        public Person RelatedPerson { get; private set; }
        //Type of relationship
        public PersonalRelationshipType Type { get; set; }

        internal PersonalRelationship(Person relatedPerson, PersonalRelationshipType personalRelationshipType, Guid referenceId, string description = "") : base(personalRelationshipType, referenceId)
        {
            if(relatedPerson == null)
                throw new ArgumentNullException(nameof(relatedPerson), @"You should know that a relationship needs two parties to be established...");

            RelatedPerson = relatedPerson;
            Description = description;

            ItemType = MemoryItemType.PersonalRelationship;
        }

        #region Data copy methods
        /// <inheritdoc />
        public override MemoryItem GetAccurateCopy()
        {
            var copy = new PersonalRelationship(RelatedPerson, Type, ReferenceId)
            {
                ItemType = ItemType,
                Description = Description,
                Started = Started,
                Ended = Ended,
                Name = Name
            };

            return copy;
        }

        /// <inheritdoc />
        public override MemoryItem GetInaccurateCopy()
        {
            GameTime.GameTime started = Started;
            GameTime.GameTime ended = Ended;
            PersonalRelationshipType type = Type;

            //TODO : Randomize name

            int falsificationCase = RandomValueGenerator.GenerateIntWithMaxValue(4);

            switch (falsificationCase)
            {
                case 1:
                    int variance = RandomValueGenerator.GenerateRealWithinValues(-10, 10);
                    started?.SetYear(started.GetYear() + variance);
                    break;
                case 2:
                    int deathVariance = RandomValueGenerator.GenerateRealWithinValues(-10, 10);
                    ended?.SetYear(ended.GetYear() + deathVariance);
                    break;
                case 3:
                    type = (PersonalRelationshipType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(PersonalRelationshipType)).Length);
                    break;
                case 4:
                    type = (PersonalRelationshipType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(PersonalRelationshipType)).Length);
                    variance = RandomValueGenerator.GenerateRealWithinValues(-10, 10);
                    started?.SetYear(started.GetYear() + variance);
                    break;
            }

            var copy = new PersonalRelationship(RelatedPerson, type, ReferenceId)
            {
                ItemType = ItemType,
                Description = Description,
                Started = started,
                Ended = ended,
                Name = Name
            };

            return copy;
        }
        #endregion

        public override void ChangeType(Enum newType)
        {
            var type = (PersonalRelationshipType)newType;

            Type = type;
        }

        public void Dispose()
        {
            RelatedPerson = null;
        }
    }
}