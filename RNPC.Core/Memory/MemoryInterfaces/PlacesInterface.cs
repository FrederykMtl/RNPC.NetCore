using System;
using System.Collections.Generic;
using System.Linq;
using RNPC.Core.Enums;

// ReSharper disable once CheckNamespace
namespace RNPC.Core.Memory
{
    public partial class Memory
    {
        [NonSerialized]
        internal PlacesInterface Places;

        internal class PlacesInterface
        {
            //Reference to the parent class
            private readonly Memory _parent;

            internal PlacesInterface(Memory parent)
            {
                _parent = parent ?? throw new ArgumentNullException($"I can't find my memories!");
            }

            /// <summary>
            /// Returns the date when a place was founded
            /// </summary>
            /// <param name="placeName"></param>
            /// <returns>3 possible scenarios can be returned:
            /// First, I don't have the information, i'll return null
            /// Second, I know the information, I'll return a list with one item
            /// Third I have conflicting information, I'll return two dates
            /// </returns>
            public List<GameTime.GameTime> WhenWasThisPlaceFounded(string placeName)
            {
                var dates = new List<GameTime.GameTime>();

                var placesIKnow = _parent._longTermMemory.Where(o => o.ItemType == MemoryItemType.Place).ToList();

                //I don't know any places
                if (placesIKnow.Count == 0)
                    return null;

                //check if we can find the place
                var place = (Place)(placesIKnow.FirstOrDefault(o => o.Name == placeName) ?? placesIKnow.FirstOrDefault(o => o.Name.Contains(placeName)));

                if (place == null)
                    return null;

                var creationDate = place.Creation;

                var foundationDate = place.FindLinkedEventByType(OccurenceType.Creation)?.Started;

                if (creationDate == null && foundationDate == null)
                    return null;

                if (creationDate != null)
                    dates.Add(creationDate);

                if (foundationDate != null)
                    dates.Add(foundationDate);

                return dates;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="placeName"></param>
            /// <returns></returns>
            public Place FindPlaceByName(string placeName)
            {
                var placeToFind = _parent._longTermMemory.FirstOrDefault(p => p.ItemType == MemoryItemType.Place && p.Name == placeName) ??
                                  _parent._longTermMemory.FirstOrDefault(p => p.ItemType == MemoryItemType.Place && p.Name.Contains(placeName));

                return (Place)placeToFind;
            }
        }
    }
}