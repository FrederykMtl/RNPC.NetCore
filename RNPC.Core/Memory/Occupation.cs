using System;
using System.Collections.Generic;
using System.Linq;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.Memory
{
    [Serializable]
    public class Occupation : MemoryItem
    {
        public GameTime.GameTime Started;
        public GameTime.GameTime Ended;
        public OccupationType Type { get; }

        //The one who holds/held the occupation
        public Person OccupationHolder { get; set; }
        //These would be large-scale events, such as wars and coronations.
        private List<OccupationalInvolvement> _linkedEvents;
        //Could be the kingdom associated with a kingship, or the location of a battle
        private List<OccupationalTie> _linkedPlaces;

        #region Constructors

        public Occupation(string occupationName, OccupationType type, Guid referenceId, string description = "") : base(referenceId)
        {
            Name = occupationName;
            Type = type;
            Description = description;
            ItemType = MemoryItemType.Occupation;
        }

        public Occupation(string occupationName, Person occupationHolder, OccupationType type, Guid referenceId, string description = "") : this(occupationName, type, referenceId, description)
        {
            OccupationHolder = occupationHolder;
        }

        public Occupation(string occupationName, Person occupationHolder, OccupationType type, Guid referenceId, GameTime.GameTime started, GameTime.GameTime ended = null,  string description = "") : 
                            this(occupationName, occupationHolder, type, referenceId, description)
        {
            Started = started;
            Ended = ended;
        }

        #endregion

        #region Public Methods

        public bool AmIAnEmployee()
        {
            return Type == OccupationType.Professional || Type == OccupationType.Trade;
        }

        public bool AmIMyOwnBoss()
        {
            return Type == OccupationType.Contractor || Type == OccupationType.Entrepreneur || Type == OccupationType.Independant;
        }

        public bool DoIWorkForTheGovernment()
        {
            return Type == OccupationType.Political || Type == OccupationType.Bureaucrat;
        }

        #endregion

        #region Occupations
        #endregion

        #region PastEvents
        public void AddLinkedEvent(OccupationalInvolvement pastEvent)
        {
            if(_linkedEvents == null)
                _linkedEvents = new List<OccupationalInvolvement>();

            if(!_linkedEvents.Exists(e => e.Name == pastEvent.Name))
                _linkedEvents.Add(pastEvent);
        }

        //public PastEvent FindLinkedEvent(string eventName)
        //{
        //    return _linkedEvents?.FirstOrDefault(i => i.LinkedEvent.Name == eventName)?.LinkedEvent;
        //}

        public List<PastEvent> FindLinkedEventsByInvolvementType(OccupationalInvolvementType involvementType)
        {
            return _linkedEvents.Where(involvement => involvement.Type == involvementType).Select(i => i.LinkedEvent).ToList();
        }

        #endregion

        #region Places
        public void AddLinkedPlace(OccupationalTie newPlace)
        {
            if(_linkedPlaces == null)
                _linkedPlaces = new List<OccupationalTie>();

            if(!_linkedPlaces.Exists(p => p.Name == newPlace.Name && p.Type == newPlace.Type))
                _linkedPlaces.Add(newPlace);
        }

        public Place FindLinkedPlace(string placeName, PlaceType placeType)
        {
            return _linkedPlaces?.FirstOrDefault(p => p.Name == placeName && p.LinkedLocation.Type == placeType)?.LinkedLocation;
        }

        public Place FindLinkedPlaceByType(OccupationalTieType tieType)
        {
            return _linkedPlaces?.FirstOrDefault(o =>o.Type == tieType)?.LinkedLocation;
        }

        #endregion

        #region Data copy methods
        /// <summary>
        /// Returns a copy of the object with all the information copied accurately
        /// </summary>
        /// <returns>Item with the information</returns>
        public override MemoryItem GetAccurateCopy()
        {
            Occupation copy = new Occupation(Name, OccupationHolder, Type, ReferenceId, Started, Ended, Description)
            {
                ItemType = ItemType,
                _linkedPlaces = _linkedPlaces,
                _linkedEvents = _linkedEvents
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
            GameTime.GameTime started = Started;
            GameTime.GameTime ended = Ended;
            OccupationType type = Type;

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
                    type = (OccupationType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(OccupationType)).Length);
                    break;
                case 4:
                    type = (OccupationType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(OccupationType)).Length);
                    variance = RandomValueGenerator.GenerateRealWithinValues(-15, 15);
                    started?.SetYear(started.GetYear() + variance);
                    break;
            }

            var copy = new Occupation(Name, OccupationHolder, type, ReferenceId, Started, Ended, Description)
            {
                ItemType = ItemType,
                _linkedPlaces = _linkedPlaces,
                _linkedEvents = _linkedEvents
            };

            return copy;
        }
        #endregion
    }
}