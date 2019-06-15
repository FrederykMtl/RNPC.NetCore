using System;
using RNPC.Core.Enums;
using RNPC.Core.Resources;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.Memory
{
    [Serializable]
    public class PersonalTie : MemoryItemLink
    {
        public Place LinkedLocation { get; }
        public Person LinkedPerson { get; }
        public PersonalTieType Type { get; set; }

        #region Constructors
        internal PersonalTie(Place linkedLocation, Person linkedPerson, PersonalTieType type, Guid referenceId) : base(type, referenceId)
        {
  
            LinkedLocation = linkedLocation;
            LinkedPerson = linkedPerson;
            ItemType = MemoryItemType.PersonalTie;
        }

        internal PersonalTie(Place linkedLocation, Person linkedPerson, PersonalTieType type, Guid referenceId, GameTime.GameTime started, GameTime.GameTime ended = null) : base(type, referenceId)
        {
            LinkedLocation = linkedLocation;
            LinkedPerson = linkedPerson;
            Started = started;
            Ended = ended;
            ItemType = MemoryItemType.PersonalTie;
        }
        #endregion

        #region Data copy methods
        /// <inheritdoc />
        public override MemoryItem GetAccurateCopy()
        {
            var copy = new PersonalTie(LinkedLocation, LinkedPerson, Type, ReferenceId, Started, Ended)
            {
                ItemType = ItemType,
                Description = Description,
                ReverseDescription = ReverseDescription,
                Name = Name
            };

            return copy;
        }

        /// <inheritdoc />
        public override MemoryItem GetInaccurateCopy()
        {
            GameTime.GameTime started = Started;
            GameTime.GameTime ended = Ended;
            PersonalTieType type = Type;

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
                    type = (PersonalTieType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(PersonalTieType)).Length);
                    break;
                case 4:
                    type = (PersonalTieType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(PersonalTieType)).Length);
                    variance = RandomValueGenerator.GenerateRealWithinValues(-10, 10);
                    started?.SetYear(started.GetYear() + variance);
                    break;
            }

            var copy = new PersonalTie(LinkedLocation, LinkedPerson, type, ReferenceId, started, ended)
            {
                ItemType = ItemType,
                Description = Description,
                ReverseDescription = ReverseDescription,
                Name = Name
            };

            return copy;
        }
        #endregion

        public override void ChangeType(Enum newType)
        {
            var type = (PersonalTieType)newType;

            Type = type;
            Description = PersonalTieDescription.ResourceManager.GetString(type.ToString());
            ReverseDescription = PersonalTieReverseDescription.ResourceManager.GetString(type.ToString());
        }
    }
}