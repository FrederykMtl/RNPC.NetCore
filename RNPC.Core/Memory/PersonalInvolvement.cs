using System;
using RNPC.Core.Enums;
using RNPC.Core.Resources;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.Memory
{
    [Serializable]
    public class PersonalInvolvement : MemoryItemLink
    {
        public PastEvent LinkedEvent { get; }
        public Person LinkedPerson { get; }
        public PersonalInvolvementType Type { get; set; }

        #region Constructors

        internal PersonalInvolvement(PastEvent linkedEvent, Person linkedPerson, PersonalInvolvementType type, Guid referenceId) : base(type, referenceId)
        {
            LinkedEvent = linkedEvent;
            LinkedPerson = linkedPerson;
            ItemType = MemoryItemType.PersonalInvolvement;
        }

        internal PersonalInvolvement(PastEvent linkedEvent, Person linkedPerson, PersonalInvolvementType type, Guid referenceId, GameTime.GameTime started, GameTime.GameTime ended = null) : base(type, referenceId)
        {
            LinkedEvent = linkedEvent;
            LinkedPerson = linkedPerson;
            Started = started;
            Ended = ended;
            ItemType = MemoryItemType.PersonalInvolvement;
        }
        #endregion

        #region Data copy methods
        /// <inheritdoc />
        public override MemoryItem GetAccurateCopy()
        {
            var copy = new PersonalInvolvement(LinkedEvent, LinkedPerson, Type, ReferenceId)
            {
                ItemType = ItemType,
                Description = Description,
                ReverseDescription = ReverseDescription,
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
            PersonalInvolvementType type = Type;

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
                    type = (PersonalInvolvementType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(PersonalInvolvementType)).Length);
                    break;
                case 4:
                    type = (PersonalInvolvementType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(PersonalInvolvementType)).Length);
                    variance = RandomValueGenerator.GenerateRealWithinValues(-10, 10);
                    started?.SetYear(started.GetYear() + variance);
                    break;
            }

            var copy = new PersonalInvolvement(LinkedEvent, LinkedPerson, type, ReferenceId)
            {
                ItemType = ItemType,
                Description = Description,
                ReverseDescription = ReverseDescription,
                Started = started,
                Ended = ended,
                Name = Name
            };

            return copy;
        }
        #endregion

        public override void ChangeType(Enum newType)
        {
            var type = (PersonalInvolvementType)newType;

            Type = type;
            Description = PersonalInvolvementDescription.ResourceManager.GetString(type.ToString());
            ReverseDescription = PersonalInvolvementReverseDescription.ResourceManager.GetString(type.ToString());
        }
    }
}