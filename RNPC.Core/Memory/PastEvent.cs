using System;
using System.Collections.Generic;
using System.Linq;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.Memory
{
    [Serializable]
    public class PastEvent : MemoryItem
    {
        //Type of event
        public PastEventType Type;
        //Event dates
        public GameTime.GameTime Started;
        public GameTime.GameTime Ended;

        #region Places

        //Places linked to an event
        private List<Occurence> _linkedPlaces;

        public void AddLinkedPlace(Occurence newPlace)
        {
            if (_linkedPlaces == null)
                _linkedPlaces = new List<Occurence>();

            if (!_linkedPlaces.Exists(p => p.Name == newPlace.Name && p.Type == newPlace.Type))
                _linkedPlaces.Add(newPlace);
        }

        public Place FindLinkedPlace(string placeName, PlaceType placeType)
        {
            return _linkedPlaces.Select(o => o.LinkedLocation).FirstOrDefault(p => p.Name == placeName && p.Type == placeType);
        }

        #endregion

        #region Persons
        //People involved in a past event. Probably historical figures!
        private List<PersonalInvolvement> _linkedPersons;

        internal void AddAssociatedPerson(PersonalInvolvement newinvolvedPerson)
        {
            if (_linkedPersons == null)
                _linkedPersons = new List<PersonalInvolvement>();

            if (!_linkedPersons.Exists(p => p.LinkedPerson.Name == newinvolvedPerson.Name))
                _linkedPersons.Add(newinvolvedPerson);
        }

        public Person FindAssociatedPerson(string personName)
        {
            return _linkedPersons?.FirstOrDefault(p => p.LinkedPerson.Name == personName)?.LinkedPerson;
        }

        public bool IsPersonAssociatedWithThisEvent(string personName)
        {
            return _linkedPersons != null && _linkedPersons.Exists(p => p.LinkedPerson.Name == personName);
        }

        public Place FindLinkedPlaceByOccurenceType(OccurenceType occurenceType)
        {
            return _linkedPlaces.FirstOrDefault(o => o.Type == occurenceType)?.LinkedLocation;
        }

        #endregion

        #region Occupations
        //Occupations linked to an event, like the dumb king that started the war.
        private List<OccupationalInvolvement> _linkedOccupations;
        public void AddOccupation(OccupationalInvolvement newOccupation)
        {
            if (_linkedOccupations == null)
                _linkedOccupations = new List<OccupationalInvolvement>();

            if (!_linkedOccupations.Exists(o => o.Name == newOccupation.Name &&
                                    o.LinkedOccupation.OccupationHolder == newOccupation.LinkedOccupation.OccupationHolder))
            {
                _linkedOccupations.Add(newOccupation);
            }
        }

        public List<Occupation> FindOccupationsByType(OccupationType occupationType)
        {
            return _linkedOccupations?.Select(o => o.LinkedOccupation).Where(o => o.Type == occupationType).ToList();
        }

        public List<Occupation> FindOccupationsByInvolvementType(OccupationalInvolvementType involvementType)
        {
            return _linkedOccupations?.Where(o => o.Type == involvementType).Select(o => o.LinkedOccupation).ToList();
        }

        public List<Occupation> FindOccupationsByName(string occupationName)
        {
            return _linkedOccupations?.Select(o => o.LinkedOccupation).Where(o => o.Name == occupationName).ToList();
        }

        #endregion

        #region Related Events

        //These would be related events, like events that lead to this one.
        private List<EventRelationship> _linkedEvents;

        internal void AddEventLink(EventRelationship relationship)
        {
            if(_linkedEvents == null)
                _linkedEvents = new List<EventRelationship>();

            if (!_linkedEvents.Exists(e => e.LinkedEvent == relationship.LinkedEvent))
            {
                _linkedEvents.Add(relationship);
            }
        }

        public EventRelationshipType FindEventLinkType(PastEvent linkedEvent)
        {
            return _linkedEvents.Find(e => e.LinkedEvent == linkedEvent).Type;
        }

        #endregion

        #region Constructors

        public PastEvent(string name, Guid referenceId, string description = "") : base(referenceId)
        {
            Name = name;
            Description = description;
            ItemType = MemoryItemType.PastEvent;
        }

        public PastEvent(string name, Guid referenceId, string description, GameTime.GameTime startedOn = null) : base(referenceId)
        {
            Name = name;
            Description = description;
            Started = startedOn;
            ItemType = MemoryItemType.PastEvent;
        }

        public PastEvent(string name, Guid referenceId, string description, GameTime.GameTime startedOn, GameTime.GameTime endedOn = null) : base(referenceId)
        {
            Name = name;
            Description = description;
            Started = startedOn;
            Ended = endedOn;
            ItemType = MemoryItemType.PastEvent;
        }

        #endregion

        #region Data copy methods
        /// <summary>
        /// Returns a copy of the object with all the information copied accurately
        /// </summary>
        /// <returns>Item with the information</returns>
        public override MemoryItem GetAccurateCopy()
        {
            PastEvent copy = new PastEvent(Name, ReferenceId, Description, Started, Ended)
            {
                ItemType = ItemType,
                _linkedPlaces = _linkedPlaces,
                _linkedEvents = _linkedEvents,
                _linkedOccupations = _linkedOccupations,
                _linkedPersons = _linkedPersons,
                Type =  Type
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
            PastEventType type = Type;

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
                    type = (PastEventType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(PastEventType)).Length);
                    break;
                case 4:
                    type = (PastEventType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(PastEventType)).Length);
                    variance = RandomValueGenerator.GenerateRealWithinValues(-15, 15);
                    started?.SetYear(started.GetYear() + variance);
                    break;
            }

            var copy = new PastEvent(Name, ReferenceId, Description, started, ended)
            {
                ItemType = ItemType,
                _linkedPlaces = _linkedPlaces,
                _linkedEvents = _linkedEvents,
                _linkedOccupations = _linkedOccupations,
                _linkedPersons = _linkedPersons,
                Type = type
            };

            return copy;
        }
        #endregion
    }
}