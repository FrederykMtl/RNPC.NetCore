using System;
using RNPC.Core.Enums;
using RNPC.Core.Resources;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.Memory
{
    /// <inheritdoc />
    [Serializable]
    public class EventRelationship : MemoryItemRelationship
    {
        public EventRelationshipType Type { get; private set; }
        public PastEvent LinkedEvent { get; private set; }

        internal EventRelationship(PastEvent linkedEvent, EventRelationshipType type, Guid referenceId, string description = "") : base(type, referenceId)
        {
            LinkedEvent = linkedEvent;
            Description = description;
            ItemType = MemoryItemType.EventRelationship;
        }

        #region Data copy methods
        /// <inheritdoc />
        public override MemoryItem GetAccurateCopy()
        {
            var copy = new EventRelationship(LinkedEvent, Type, ReferenceId)
            {
                ItemType = ItemType,
                Description = Description,
                Started = Started,
                Ended = Ended
            };

            return copy;
        }

        /// <inheritdoc />
        public override MemoryItem GetInaccurateCopy()
        {
            GameTime.GameTime started = Started;
            GameTime.GameTime ended = Ended;
            EventRelationshipType type = Type;

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
                    type = (EventRelationshipType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(EventRelationshipType)).Length);
                    break;
                case 4:
                    type = (EventRelationshipType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(EventRelationshipType)).Length);
                    variance = RandomValueGenerator.GenerateRealWithinValues(-15, 15);
                    started?.SetYear(started.GetYear() + variance);
                    break;
            }

            var copy = new EventRelationship(LinkedEvent, type, ReferenceId)
            {
                ItemType = ItemType,
                Description = Description,
                Started = started,
                Ended = ended
            };

            return copy;
        }
        #endregion

        public override void ChangeType(Enum newType)
        {
            var type = (EventRelationshipType)newType;

            Type = type;
            Description = EventRelationshipDescriptions.ResourceManager.GetString(type.ToString());
        }
    }
}