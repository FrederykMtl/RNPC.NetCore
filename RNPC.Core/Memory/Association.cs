using System;
using RNPC.Core.Enums;
using RNPC.Core.Resources;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.Memory
{
    [Serializable]
    public class Association : MemoryItemLink
    {
        public Person AssociatedPerson { get; }
        public Organization AssociatedOrganization { get; }
        public AssociationType Type { get; set; }

        #region Constructors
        internal Association(Person associatedPerson, Organization associatedOrganization, AssociationType type, Guid referenceId) : base(type, referenceId)
        {
            AssociatedPerson = associatedPerson;
            AssociatedOrganization = associatedOrganization;
            ItemType = MemoryItemType.Association;
        }

        internal Association(Person associatedPerson, Organization associatedOrganization, AssociationType type, Guid referenceId, global::RNPC.Core.GameTime.GameTime started, global::RNPC.Core.GameTime.GameTime ended) : base(type, referenceId)
        {
            AssociatedPerson = associatedPerson;
            AssociatedOrganization = associatedOrganization;
            Started = started;
            Ended = ended;
            ItemType = MemoryItemType.Association;
        }
        #endregion

        #region Data copy methods
        /// <inheritdoc />
        public override MemoryItem GetAccurateCopy()
        {
            Association copy = new Association(AssociatedPerson, AssociatedOrganization, Type, ReferenceId, Started, Ended)
            {
                ItemType = ItemType,
                ReverseDescription = ReverseDescription
            };

            return copy;
        }

        /// <inheritdoc />
        public override MemoryItem GetInaccurateCopy()
        {
            global::RNPC.Core.GameTime.GameTime started = Started;
            global::RNPC.Core.GameTime.GameTime ended = Ended;
            AssociationType type = Type;

            //TODO : Randomize name

            int falsificationCase = RandomValueGenerator.GenerateIntWithMaxValue(4);

            switch (falsificationCase)
            {
                case 1:
                    int variance = RandomValueGenerator.GenerateRealWithinValues(-15, 15);
                    started?.SetYear(started.GetYear() + variance);
                    break;
                case 2:
                    int deathVariance = RandomValueGenerator.GenerateRealWithinValues(-15, 15);
                    ended?.SetYear(ended.GetYear() + deathVariance);
                    break;
                case 3:
                    type = (AssociationType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(AssociationType)).Length);
                    break;
                case 4:
                    type = (AssociationType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(AssociationType)).Length);
                    variance = RandomValueGenerator.GenerateRealWithinValues(-15, 15);
                    started?.SetYear(started.GetYear() + variance);
                    break;
            }

            Association copy = new Association(AssociatedPerson, AssociatedOrganization, type, ReferenceId, started, ended)
            {
                ItemType = ItemType,
                ReverseDescription = ReverseDescription
            };

            return copy;
        }
        #endregion

        public override void ChangeType(Enum newType)
        {
            var type = (AssociationType)newType;

            Type = type;
            Description = AssociationDescription.ResourceManager.GetString(type.ToString());
            ReverseDescription = AssociationReverseDescription.ResourceManager.GetString(type.ToString());
        }
    }
}