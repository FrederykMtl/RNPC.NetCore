using System;
using RNPC.Core.Enums;
using RNPC.Core.Resources;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.Memory
{
    [Serializable]
    public class OccupationalInvolvement : MemoryItemLink
    {
        public PastEvent LinkedEvent { get; }
        public Occupation LinkedOccupation { get; }
        public OccupationalInvolvementType Type { get; set; }

        #region Constructors

        internal OccupationalInvolvement(PastEvent linkedEvent, Occupation linkedOccupation, OccupationalInvolvementType type, Guid referenceId) : base(type, referenceId)
        {
            LinkedEvent = linkedEvent;
            LinkedOccupation = linkedOccupation;
            ItemType = MemoryItemType.OccupationalInvolvement;
        }

        internal OccupationalInvolvement(PastEvent linkedEvent, Occupation linkedOccupation, OccupationalInvolvementType type, Guid referenceId, 
                                                        GameTime.GameTime started, GameTime.GameTime ended = null) : base(type, referenceId)
        {
            LinkedEvent = linkedEvent;
            LinkedOccupation = linkedOccupation;
            Started = started;
            Ended = ended;
            ItemType = MemoryItemType.OccupationalInvolvement;
        }

        #endregion

        #region Data copy methods
        /// <inheritdoc />
        public override MemoryItem GetAccurateCopy()
        {
            var copy = new OccupationalInvolvement(LinkedEvent, LinkedOccupation, Type, ReferenceId)
            {
                ItemType = ItemType,
                Description = Description,
                Started = Started,
                Ended = Ended,
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
            OccupationalInvolvementType type = Type;

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
                    type = (OccupationalInvolvementType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(OccupationalInvolvementType)).Length);
                    break;
                case 4:
                    type = (OccupationalInvolvementType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(OccupationalInvolvementType)).Length);
                    variance = RandomValueGenerator.GenerateRealWithinValues(-10, 10);
                    started?.SetYear(started.GetYear() + variance);
                    break;
            }

            var copy = new OccupationalInvolvement(LinkedEvent, LinkedOccupation, type, ReferenceId)
            {
                ItemType = ItemType,
                Description = Description,
                Started = started,
                Ended = ended,
                ReverseDescription = ReverseDescription,
                Name = Name
            };

            return copy;
        }
        #endregion

        public override void ChangeType(Enum newType)
        {
            var type = (OccupationalInvolvementType)newType;

            Type = type;
            Description = OccupationalInvolvementDescription.ResourceManager.GetString(type.ToString());
            ReverseDescription = OccupationalInvolvementReverseDescription.ResourceManager.GetString(type.ToString());
        }
    }
}