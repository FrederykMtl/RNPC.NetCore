using System;
using System.Collections.Generic;
using System.Linq;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.Memory
{
    [Serializable]
    public class Place : MemoryItem
    {
        public Coordinates Coordinates;
        public PlaceType Type;
        public GameTime.GameTime Creation;
        public GameTime.GameTime Destruction;

        //List of places linked to this one, like neighbours or sub structures
        private List<PlaceRelationship> _linkedPlaces;
        //These would be large-scale events, such as wars and coronations.
        private List<Occurence> _linkedEvents;
        //Occupations linked to a place, such as kings, counts, barons or prime ministers?
        private List<OccupationalTie> _relatedOccupations;
        //Can be used to remember a list of known kings, or the guy who cut the ribbon!
        private List<PersonalTie> _linkedPersons;

        #region Constructors

        public Place(string name, Guid referenceId, string description = "") : base(referenceId)
        {
            Name = name;
            Description = description;
            ItemType = MemoryItemType.Place;
        }

        public Place(string name, Guid referenceId, Coordinates coordinates, string description = "") : base(referenceId)
        {
            Name = name;
            Description = description;
            Coordinates = coordinates;
            ItemType = MemoryItemType.Place;
        }

        public Place(string name, Guid referenceId, Coordinates coordinates, string description = "", GameTime.GameTime creation = null, GameTime.GameTime destruction = null) : base(referenceId)
        {
            Name = name;
            Description = description;
            Coordinates = coordinates;
            Creation = creation;
            Destruction = destruction;
            ItemType = MemoryItemType.Place;
        }

        #endregion

        #region Public methods

        public bool IsThisAFeudalTerritory()
        {
            return Type == PlaceType.Duchy || Type == PlaceType.Earldom || Type == PlaceType.Kingdom;
        }

        public bool IsThisASmallTown()
        {
            return Type == PlaceType.Hamlet || Type == PlaceType.Village;
        }

        public bool IsThisABigTown()
        {
            return Type == PlaceType.City || Type == PlaceType.Metropolis || Type == PlaceType.Polis || Type == PlaceType.Megalopolis || Type == PlaceType.Cosmopolis;
        }

        public bool IsThisALargeGeopoliticalTerritory()
        {
            return Type == PlaceType.Kingdom || Type == PlaceType.Country || Type == PlaceType.Province || Type == PlaceType.Republic || Type == PlaceType.State;
        }

        public bool IsThisASmallGeopoliticalTerritory()
        {
            return Type == PlaceType.County || Type == PlaceType.Borough || Type == PlaceType.Municipality;
        }

        public bool IsThisAGeographicalLandmass()
        {
            return Type == PlaceType.Continent || Type == PlaceType.Atoll || Type == PlaceType.Cape ||
                   Type == PlaceType.Delta || Type == PlaceType.Island || Type == PlaceType.Peninsula;
        }

        public bool IsThisASimpleLocation()
        {
            return Type == PlaceType.Cemetary || Type == PlaceType.Place || Type == PlaceType.Park;
        }

        #endregion

        #region Places
        /// <summary>
        /// Adds a new link between this place and another one.
        /// </summary>
        /// <param name="newPlaceRelationship">The link to add. This method should be called by the PlaceRelationship object itself, as they add themselves automatically.</param>
        internal void AddLinkedPlace(PlaceRelationship newPlaceRelationship)
        {
            if (_linkedPlaces == null)
                _linkedPlaces = new List<PlaceRelationship>();

            if(!_linkedPlaces.Exists(l => l.LinkedPlace == newPlaceRelationship.LinkedPlace && l.Type == newPlaceRelationship.Type))
                _linkedPlaces.Add(newPlaceRelationship);
        }

        public Place FindLinkedPlace(string placeName, PlaceType placeType)
        {
            return _linkedPlaces.FirstOrDefault(p => p.LinkedPlace.Name == placeName &&
                                                    p.LinkedPlace.Type == placeType)?.LinkedPlace;
        }

        #endregion

        #region PastEvents
        internal void AddLinkedEvent(Occurence pastEvent)
        {
            if (_linkedEvents == null)
                _linkedEvents = new List<Occurence>();

            if (!_linkedEvents.Exists(e => e.Name == pastEvent.Name))
                _linkedEvents.Add(pastEvent);
        }

        public PastEvent FindLinkedEvent(string eventName)
        {
            var eventToFind = _linkedEvents?.FirstOrDefault(e => e.LinkedEvent.Name == eventName)?.LinkedEvent;

            return eventToFind ?? _linkedEvents?.FirstOrDefault(e => e.LinkedEvent.Name.Contains(eventName))?.LinkedEvent;
        }

        public PastEvent FindLinkedEventByType(OccurenceType type)
        {
            return _linkedEvents?.FirstOrDefault(e => e.Type == type)?.LinkedEvent;
        }
        #endregion

        #region Occupations
        public void AddLinkedOccupation(OccupationalTie newOccupation)
        {
            if (_relatedOccupations == null)
                _relatedOccupations = new List<OccupationalTie>();

            if (!_relatedOccupations.Exists(o => o.Name == newOccupation.Name &&
                                                 o.LinkedOccupation.OccupationHolder == newOccupation.LinkedOccupation.OccupationHolder))
            {
                _relatedOccupations.Add(newOccupation);
            }
        }

        public List<Occupation> FindOccupationsByOccupationalTieType(OccupationalTieType tieType)
        {
            return _relatedOccupations.Where(o => o.Type == tieType).Select(t => t.LinkedOccupation).ToList();
        }

        public List<Occupation> FindOccupationsByType(OccupationType occupationType)
        {
            return _relatedOccupations.Select(o => o.LinkedOccupation).Where(o => o.Type == occupationType).ToList();
        }

        public List<Occupation> FindOccupationsByName(string occupationName)
        {
            return _relatedOccupations.Select(o => o.LinkedOccupation).Where(o => o.Name == occupationName).ToList();
        }

        #endregion

        #region Persons
        internal void AddLinkedPerson(PersonalTie newLinkedPerson)
        {
            if (_linkedPersons == null)
                _linkedPersons = new List<PersonalTie>();

            if (!_linkedPersons.Exists(p => p.Name == newLinkedPerson.Name))
                _linkedPersons.Add(newLinkedPerson);
        }

        public Person FindAssociatedPerson(string personName)
        {
            return _linkedPersons.FirstOrDefault(p => p.LinkedPerson.Name == personName)?.LinkedPerson;
        }
        #endregion

        #region Data copy methods
        /// <summary>
        /// Returns a copy of the object with all the information copied accurately
        /// </summary>
        /// <returns>Item with the information</returns>
        public override MemoryItem GetAccurateCopy()
        {
            Place copy = new Place(Name, ReferenceId, Coordinates, Description, Creation, Destruction)
            {
                ItemType = ItemType,
                _linkedPlaces = _linkedPlaces,
                _linkedEvents = _linkedEvents,
                _linkedPersons = _linkedPersons,
                _relatedOccupations = _relatedOccupations
            };

            return copy;
        }

        /// <summary>
        /// Returns a copy of the object with all the information copied inaccurately
        /// this represents the character having wrong information
        /// </summary>
        /// <returns>Item with the information</returns>
        public override MemoryItem GetInaccurateCopy()
        {
            GameTime.GameTime creation = Creation;
            GameTime.GameTime destruction = Destruction;
            PlaceType placeType = Type;

            //TODO : Randomize name

            int falsificationCase = RandomValueGenerator.GenerateIntWithMaxValue(4);

            switch (falsificationCase)
            {
                case 1:
                    int variance = RandomValueGenerator.GenerateRealWithinValues(-30, 30);
                    creation?.SetYear(creation.GetYear() + variance);
                    break;
                case 2:
                    int deathVariance = RandomValueGenerator.GenerateRealWithinValues(-30, 30);
                    destruction?.SetYear(destruction.GetYear() + deathVariance);
                    break;
                case 3:
                    placeType = (PlaceType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(PlaceType)).Length);
                    break;
                case 4:
                    placeType = (PlaceType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(PlaceType)).Length);
                    variance = RandomValueGenerator.GenerateRealWithinValues(-30, 30);
                    creation?.SetYear(creation.GetYear() + variance);
                    break;
            }

            var copy = new Place(Name, ReferenceId, Coordinates, Description, Creation, Destruction)
            {
                Type = placeType,
                ItemType = ItemType,
                _linkedPlaces = _linkedPlaces,
                _linkedEvents = _linkedEvents,
                _linkedPersons = _linkedPersons,
                _relatedOccupations = _relatedOccupations
            };

            return copy;
        }
        #endregion
    }
}