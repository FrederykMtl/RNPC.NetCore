using System;
using RNPC.Core.Enums;
using RNPC.Core.Resources;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.Memory
{
    [Serializable]
    public class OccupationalTie : MemoryItemLink
    {
        public Place LinkedLocation { get; }
        public Occupation LinkedOccupation { get; }
        public OccupationalTieType Type { get; set; }

        #region Constructors
        internal OccupationalTie(Place linkedLocation, Occupation linkedOccupation, OccupationalTieType type, Guid referenceId) : base(type, referenceId)
        {
            LinkedLocation = linkedLocation;
            LinkedOccupation = linkedOccupation;
            ItemType = MemoryItemType.OccupationalTie;
        }

        internal OccupationalTie(Place linkedLocation, Occupation linkedOccupation, OccupationalTieType type, Guid referenceId, GameTime.GameTime started, GameTime.GameTime ended = null) : base(type, referenceId)
        {
            LinkedLocation = linkedLocation;
            LinkedOccupation = linkedOccupation;
            Started = started;
            Ended = ended;
            ItemType = MemoryItemType.OccupationalTie;
        }
        #endregion

        #region Data copy methods
        /// <inheritdoc />
        public override MemoryItem GetAccurateCopy()
        {
            var copy = new OccupationalTie(LinkedLocation, LinkedOccupation, Type, ReferenceId)
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
            OccupationalTieType type = Type;

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
                    type = (OccupationalTieType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(OccupationalTieType)).Length);
                    break;
                case 4:
                    type = (OccupationalTieType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(OccupationalTieType)).Length);
                    variance = RandomValueGenerator.GenerateRealWithinValues(-10, 10);
                    started?.SetYear(started.GetYear() + variance);
                    break;
            }

            var copy = new OccupationalTie(LinkedLocation, LinkedOccupation, type, ReferenceId)
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
            var type = (OccupationalTieType)newType;

            Type = type;
            Description = OccupationalTieDescription.ResourceManager.GetString(type.ToString());
            ReverseDescription = OccupationalTieReverseDescription.ResourceManager.GetString(type.ToString());
        }
    }
}