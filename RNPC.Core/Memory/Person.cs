using System;
using System.Collections.Generic;
using System.Linq;
using RNPC.Core.Enums;
using RNPC.Core.Exceptions;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.Memory
{
    [Serializable]
    public class Person : MemoryItem
    {
        public Gender Gender;
        public Sex Sex;
        public Orientation Orientation;

        //dates of birth and death
        public GameTime.GameTime DateOfBirth;
        public GameTime.GameTime DateOfDeath;

        /// <summary>
        /// Organizations associated with this person
        /// </summary>
        private List<Association> _associations;
        //These are the events a person was involved with. The type of involvement is defined
        //with this object, as well as the dates/time
        private List<PersonalInvolvement> _linkedEvents;
        //Occupations a person has or had.
        private List<Occupation> _occupations;
        //Places linked to a person, like where they live or where they come from.
        private List<PersonalTie> _linkedPlaces;
        //A person's relationships: Parents, spouses, friends
        private List<PersonalRelationship> _relationships;

        #region Constructors

        public Person(string personName, string description = "") : base(Guid.NewGuid())
        {
            ItemType = MemoryItemType.Person;
            Name = personName;
            Description = description;
            ItemType = MemoryItemType.Person;
        }

        public Person(string personName, Guid referenceId, string description = "") : base(referenceId)
        {
            ItemType = MemoryItemType.Person;
            Name = personName;
            Description = description;
            ItemType = MemoryItemType.Person;
        }

        public Person(string personName, Gender gender, Sex sex, Orientation orientation, Guid referenceId, string description = "") : base(referenceId)
        {
            ItemType = MemoryItemType.Person;
            Name = personName;
            Description = description;
            Orientation = orientation;
            Gender = gender;
            Sex = sex;
            ItemType = MemoryItemType.Person;
        }

        public Person(string personName, Gender gender, Sex sex, Orientation orientation, Guid referenceId, GameTime.GameTime dateOfBirth, string description = "") : base(referenceId)
        {
            ItemType = MemoryItemType.Person;
            Name = personName;
            Description = description;
            Orientation = orientation;
            Gender = gender;
            Sex = sex;
            DateOfBirth = dateOfBirth;
            ItemType = MemoryItemType.Person;
        }

        #endregion

        #region Associations
        /// <summary>
        /// Add an association to an organization. This should be called by the factory.
        /// </summary>
        /// <param name="newAssociation">Association link</param>
        internal void AddAssociation(Association newAssociation)
        {
            if (_associations == null)
                _associations = new List<Association>();

            if (!_associations.Contains(newAssociation))
            {
                _associations.Add(newAssociation);
            }
        }

        public Association FindAssociation(Organization withOrganization, AssociationType type)
        {
            return _associations.FirstOrDefault(p => p.AssociatedOrganization.Equals(withOrganization) && p.Type == type);
        }

        public Association FindAssociation(Organization withOrganization, AssociationType type, GameTime.GameTime startingAt)
        {
            return _associations.FirstOrDefault(p => p.AssociatedOrganization.Equals(withOrganization) && p.Type == type && p.Started == startingAt);
        }

        public List<Association> FindAssociations(Organization withOrganization)
        {
            return _associations.Where(p => p.AssociatedOrganization.Equals(withOrganization)).ToList();
        }

        #endregion

        #region PastEvents
        /// <summary>
        /// Add a link to an event. This should be called only by the factory
        /// </summary>
        /// <param name="eventInvolvement">link to event to add</param>
        internal void AddPersonalInvolvement(PersonalInvolvement eventInvolvement)
        {
            if (_linkedEvents == null)
                _linkedEvents = new List<PersonalInvolvement>();

            if (!_linkedEvents.Exists(e => e.Name == eventInvolvement.Name))
                _linkedEvents.Add(eventInvolvement);
        }

        //public PastEvent FindLinkedEventByName(string eventName)
        //{
        //    return _linkedEvents.FirstOrDefault(e => e.LinkedEvent.Name == eventName)?.LinkedEvent;
        //}

        /// <summary>
        /// Find an event by the way this person was involved in it.
        /// </summary>
        /// <param name="involvementType">Type of involvement based on the enum</param>
        /// <returns>a list of all the relevant events</returns>
        public List<PastEvent> FindLinkedEventsByInvolvementType(PersonalInvolvementType involvementType)
        {
            return _linkedEvents.Where(involvement => involvement.Type == involvementType).Select(i => i.LinkedEvent).ToList();
        }

        /// <summary>
        /// Find the link object to an event by the way this person was involved in it.
        /// </summary>
        /// <param name="involvementType">Type of involvement based on the enum</param>
        /// <returns>a list of all the relevant PersonalInvolvement</returns>
        public PersonalInvolvement GetPersonalInvolvementByType(PersonalInvolvementType involvementType)
        {
            return _linkedEvents.FirstOrDefault(i => i.Type == involvementType);
        }

        public PersonalInvolvement GetPersonalInvolvement(string linkedEventName, PersonalInvolvementType involvementType)
        {
            return _linkedEvents.FirstOrDefault(i => i.LinkedEvent.Name == linkedEventName && i.Type == involvementType);
        }

        #endregion

        #region Occupations
        /// <summary>
        /// Add an occupation for a Person
        /// </summary>
        /// <param name="newOccupation">A Person's new Occupation</param>
        public void AddOccupation(Occupation newOccupation)
        {
            if(_occupations == null)
                _occupations = new List<Occupation>();

            if (!_occupations.Exists(o => o.Name == newOccupation.Name &&
                                          o.Started == newOccupation.Started))
            {
                _occupations.Add(newOccupation);
            }
        }

        /// <summary>
        /// Find instances where I had/have an occupation by its name
        /// </summary>
        /// <param name="occupationName">Name of the occupation i.e. mason</param>
        /// <returns>a List of all the instances of an occupation. The reason that
        /// there might be more tha one is that they are dated. For I example, I was
        /// a mason, became a mayor for 3 years then went back to being a mason.</returns>
        public List<Occupation> FindOccupations(string occupationName)
        {
            return _occupations.Where(o => o.Name == occupationName).ToList();
        }

        /// <summary>
        /// What is my current occupation?
        /// If there is more than one only the last one will be returned
        /// </summary>
        /// <returns>current occupation</returns>
        public Occupation GetCurrentOccupation()
        {
            return _occupations?.LastOrDefault(o => o.Ended == null);
        }

        /// <summary>
        /// What is my current occupations?
        /// </summary>
        /// <returns>all of my current occupations</returns>
        public List<Occupation> GetCurrentOccupations()
        {
            return _occupations.Where(o => o.Ended == null).ToList();
        }

        #endregion

        #region Places
        internal void AddLinkedPlace(PersonalTie newLinkedPlace)
        {
            if (_linkedPlaces == null)
                _linkedPlaces = new List<PersonalTie>();

            if (!_linkedPlaces.Exists(p => p.LinkedLocation.Name == newLinkedPlace.LinkedLocation.Name && p.LinkedLocation.Type == newLinkedPlace.LinkedLocation.Type))
                _linkedPlaces.Add(newLinkedPlace);
        }

        public Place FindLinkedPlace(string placeName, PlaceType placeType)
        {
            return _linkedPlaces?.FirstOrDefault(p => p.LinkedLocation.Name == placeName && p.LinkedLocation.Type == placeType)?.LinkedLocation;
        }

        public PersonalTie FindPersonalTie(string placeName, PersonalTieType type)
        {
            return _linkedPlaces?.FirstOrDefault(p => p.LinkedLocation.Name == placeName && p.Type == type);
        }

        public Place FindPlaceByPersonalTieType(PersonalTieType type)
        {
            return _linkedPlaces?.FirstOrDefault(p => p.Type == type)?.LinkedLocation;
        }

        #endregion

        #region Relationships
        /// <summary>
        /// Adds a relationship. Relationships should not be removed - they end.
        /// To do so, get a relationship with FindRelationship and modify it.
        /// </summary>
        /// <param name="newPersonalRelationship"></param>
        internal void AddRelationship(PersonalRelationship newPersonalRelationship)
        {
            if(_relationships == null)
                _relationships = new List<PersonalRelationship>();

            
            _relationships.Add(newPersonalRelationship);
        }

        ///// <summary>
        ///// Allows to find a relationship for read or update
        ///// </summary>
        ///// <param name="relationshipWith"></param>
        ///// <param name="started">Time the relationship started. Can be null</param>
        ///// <returns></returns>
        //public PersonalRelationship FindRelationship(Person relationshipWith, GameTime.GameTime started)
        //{
        //    return _relationships.FirstOrDefault(r => r.RelatedPerson.Equals(relationshipWith) && r.Started == started);
        //}

        /// <summary>
        /// Fin all the relationships to a person
        /// </summary>
        /// <param name="relationshipWith">The person to look up</param>
        /// <returns>A list of those relationships, independant of type</returns>
        public List<PersonalRelationship> FindRelationships(Person relationshipWith)
        {
            if(_relationships == null || !_relationships.Any())
                return new List<PersonalRelationship>();

            return _relationships.Where(r => r.RelatedPerson.Equals(relationshipWith)).ToList();
        }

        /// <summary>
        /// Returns the relationship of a person for a certain type, i.e. friends or family members.
        /// </summary>
        /// <param name="type">Type of relationship desired</param>
        /// <returns>A list of relationships of the applicable type</returns>
        public List<PersonalRelationship> FindRelationshipsByType(PersonalRelationshipType type)
        {
            return _relationships.Where(r => r.Type == type).ToList();
        }

        public PersonalRelationship WhatIsMyCurrentRelationshipWithThisPerson(Person thatPerson)
        {
            if (thatPerson == null)
                throw new RnpcParameterException("Come on. You can't have a relationship with nothing.", new Exception("No person specified!"));

            return FindRelationships(thatPerson).LastOrDefault(r => r.Ended == null);
        }

        public List<PersonalRelationship> GetAllMyRelationshipsWithThisPerson(Person thatPerson)
        {
            if (thatPerson == null)
                throw new RnpcParameterException("Come on. You can't have a relationship with nothing.", new Exception("No person specified!"));

            return FindRelationships(thatPerson);
        }

        internal bool RemoveRelationship(PersonalRelationship relationship)
        {
            return _relationships.Remove(relationship);
        }

        #endregion

        #region Data copy methods

        /// <inheritdoc />
        /// <summary>
        /// Returns a copy of the object with all the information copied accurately
        /// </summary>
        /// <returns>Item with the information</returns>
        public override MemoryItem GetAccurateCopy()
        {
            var copy = new Person(Name, Gender, Sex, Orientation, ReferenceId)
            {
                DateOfBirth = DateOfBirth,
                DateOfDeath = DateOfDeath,
                Description = Description,
                ItemType =  ItemType,
                _associations = _associations,
                _linkedEvents = _linkedEvents,
                _linkedPlaces = _linkedPlaces,
                _occupations = _occupations,
                _relationships = _relationships
            };

            return copy;
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns a copy of the object with all the information copied inaccurately
        /// this represents the character having wrong information
        /// </summary>
        /// <returns>Item with the information</returns>
        public override MemoryItem GetInaccurateCopy()
        {
            GameTime.GameTime birthDate = DateOfBirth;
            GameTime.GameTime deathDate = DateOfDeath;
            Orientation orientation = Orientation;
            //TODO : Randomize name

            int falsificationCase = RandomValueGenerator.GenerateIntWithMaxValue(4);

            switch (falsificationCase)
            {
                case 1:
                    int variance = RandomValueGenerator.GenerateRealWithinValues(-30, 30);
                    birthDate?.SetYear(birthDate.GetYear() + variance);
                    break;
                case 2:
                    int deathVariance = RandomValueGenerator.GenerateRealWithinValues(-30, 30);
                    deathDate?.SetYear(deathDate.GetYear() + deathVariance);
                    break;
                case 3:
                    orientation = (Orientation)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(Orientation)).Length);
                    break;
                case 4:
                    variance = RandomValueGenerator.GenerateRealWithinValues(-30, 30);
                    birthDate?.SetYear(birthDate.GetYear() + variance);
                    orientation = (Orientation)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(Orientation)).Length);
                    break;
            }

            Person copy = new Person(Name, Gender, Sex, orientation, ReferenceId)
            {
                DateOfBirth = birthDate,
                DateOfDeath = deathDate,
                Description = Description,
                ItemType = ItemType,
                _associations = _associations,
                _linkedEvents = _linkedEvents,
                _linkedPlaces = _linkedPlaces,
                _occupations = _occupations,
                _relationships = _relationships
            };

            return copy;
        }

        #endregion

        /// <summary>
        /// Returns the age of the person
        /// </summary>
        /// <returns></returns>
        public int Age(GameTime.GameTime currentGameTime)
        {
            if(DateOfDeath != null)
                return (int)DateOfBirth.TimeElapsedInYearsSince(DateOfDeath);

            return (int)DateOfBirth.TimeElapsedInYearsSince(currentGameTime);
        }
    }
}