using System;
using RNPC.Core.Enums;
using RNPC.Core.Resources;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.Memory
{
    /// <inheritdoc />
    [Serializable]
    public class PlaceRelationship : MemoryItemRelationship
    {
        public GeographicRelationshipType Type { get; private set; }
        public Place LinkedPlace { get; }

        #region Constructors
        internal PlaceRelationship(Place linkedPlace, GeographicRelationshipType firstToSecondPlaceRelationship, Guid referenceId) : base(firstToSecondPlaceRelationship, referenceId)
        {
            if(linkedPlace == null)
                throw new ArgumentNullException(nameof(linkedPlace), @"Cannot link a Place to nothing.");

            LinkedPlace = linkedPlace;
            //SetDescription(firstToSecondPlaceRelationship);

            ItemType = MemoryItemType.PlaceRelationship;
        }

        internal PlaceRelationship(Place linkedPlace, GeographicRelationshipType firstToSecondPlaceRelationship, Guid referenceId, GameTime.GameTime started, GameTime.GameTime ended = null) : base(firstToSecondPlaceRelationship, referenceId)
        {
            if (linkedPlace == null)
                throw new ArgumentNullException(nameof(linkedPlace), @"Cannot link a Place to nothing.");

            LinkedPlace = linkedPlace;
           // SetDescription(firstToSecondPlaceRelationship);

            Started = started;
            Ended = ended;

            ItemType = MemoryItemType.PlaceRelationship;
        }

        private void SetDescription(GeographicRelationshipType relationshipType)
        {
            Type = relationshipType;
            Description = GeographicRelationshipDescription.ResourceManager.GetString(relationshipType.ToString());
        }

        public override void ChangeType(Enum newType)
        {
            SetDescription((GeographicRelationshipType)newType);
        }
        #endregion

        #region Data copy methods
        /// <inheritdoc />
        public override MemoryItem GetAccurateCopy()
        {
            var copy = new PlaceRelationship(LinkedPlace, Type, ReferenceId)
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
            GeographicRelationshipType type = Type;

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
                    type = (GeographicRelationshipType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(GeographicRelationshipType)).Length);
                    break;
                case 4:
                    type = (GeographicRelationshipType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(GeographicRelationshipType)).Length);
                    variance = RandomValueGenerator.GenerateRealWithinValues(-10, 10);
                    started?.SetYear(started.GetYear() + variance);
                    break;
            }

            var copy = new PlaceRelationship(LinkedPlace, type, ReferenceId)
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
    }
}