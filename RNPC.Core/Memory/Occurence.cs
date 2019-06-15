using System;
using RNPC.Core.Enums;
using RNPC.Core.Resources;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.Memory
{
    [Serializable]
    public class Occurence : MemoryItemLink
    {
        public Place LinkedLocation { get; }
        public PastEvent LinkedEvent { get; }
        public OccurenceType Type { get; set; }

        #region Constructors

        internal Occurence(Place linkedLocation, PastEvent linkedEvent, OccurenceType type, Guid referenceId) : base(type, referenceId)
        {
            LinkedLocation = linkedLocation;
            LinkedEvent = linkedEvent;
            ItemType = MemoryItemType.Occurence;
        }

        internal Occurence(Place linkedLocation, PastEvent linkedEvent, OccurenceType type, Guid referenceId, GameTime.GameTime started, GameTime.GameTime ended = null) : base(type, referenceId)
        {
            LinkedLocation = linkedLocation;
            LinkedEvent = linkedEvent;
            Started = started;
            Ended = ended;
            ItemType = MemoryItemType.Occurence;
        }

        #endregion

        #region Data copy methods
        /// <inheritdoc />
        public override MemoryItem GetAccurateCopy()
        {
            var copy = new Occurence(LinkedLocation, LinkedEvent, Type, ReferenceId)
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
            OccurenceType type = Type;

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
                    type = (OccurenceType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(OccurenceType)).Length);
                    break;
                case 4:
                    type = (OccurenceType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(OccurenceType)).Length);
                    variance = RandomValueGenerator.GenerateRealWithinValues(-10, 10);
                    started?.SetYear(started.GetYear() + variance);
                    break;
            }

            var copy = new Occurence(LinkedLocation, LinkedEvent, type, ReferenceId)
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
            var type = (OccurenceType)newType;

            Type = type;
            Description = OccurenceDescription.ResourceManager.GetString(type.ToString());
            ReverseDescription = OccurenceReverseDescription.ResourceManager.GetString(type.ToString());
        }
    }
}