using System;
using System.Collections.Generic;
using RNPC.Core.Enums;
using RNPC.Core.Resources;

namespace RNPC.Core.Memory
{
    public class ItemLinkFactory : IItemLinkFactory
    {
        #region Link classes construction

        /// <summary>
        /// This will create relationship objects linking two Occupations together and add a reference to both
        /// </summary>
        /// <param name="firstParty">The first Person in the relationship. If the terms for each party is different, this will be the left term
        /// Example: Mother-Daughter relationship. firstParty will be the mother.</param>
        /// <param name="secondParty">The second person in the relationship and the one associated with the term on the right</param>
        /// <param name="firstPartyRole">Name of the relationship from the first party's perspective, i,e mother. Use the enum.</param>
        /// <param name="secondPartyRole">Name of the relationship from the second party's perspective, i,e daughter. Use the enum</param>
        /// <returns>The new links (2)</returns>      
        public List<PersonalRelationship> CreateRelationshipBetweenTwoPersons(Person firstParty, Person secondParty, PersonalRelationshipTypeName firstPartyRole, PersonalRelationshipTypeName secondPartyRole)
        {
            string description = PersonalRelationshipDetails.Instance.GetRelationshipDescription(firstPartyRole.ToString());
            string reverseDescription = PersonalRelationshipDetails.Instance.GetRelationshipDescription(secondPartyRole.ToString());
            var type = PersonalRelationshipDetails.Instance.GetRelationshipTypeName(firstPartyRole.ToString());

            var newRelationship1 = new PersonalRelationship(secondParty, type, Guid.NewGuid(), description)
            {
                Name = firstPartyRole + "-" + secondPartyRole,
                Started = null
            };

            firstParty.AddRelationship(newRelationship1);

            var newRelationship2 = new PersonalRelationship(firstParty, type, Guid.NewGuid(), reverseDescription)
            {
                Name = firstPartyRole + "-" + secondPartyRole,
                Started = null
            };

            secondParty.AddRelationship(newRelationship2);

            return new List<PersonalRelationship>{ newRelationship1, newRelationship2 };
        }

        /// <summary>
        /// This will create relationship objects linking two Occupations together and add a reference to both
        /// </summary>
        /// <param name="firstParty">The first Person in the relationship. If the terms for each party is different, this will be the left term
        /// Example: Mother-Daughter relationship. firstParty will be the mother.</param>
        /// <param name="secondParty">The second person in the relationship and the one associated with the term on the right</param>
        /// <param name="firstPartyRole">Name of the relationship from the first party's perspective, i,e mother. Use the enum.</param>
        /// <param name="secondPartyRole">Name of the relationship from the second party's perspective, i,e daughter. Use the enum</param>
        /// <param name="started">Date relationship started. Is used to diffentiate relationships that have started and stopped and started again (i.e. on and off friends)
        /// </param>
        /// <param name="ended">Date relationship ended</param>
        /// <returns>The new links (2)</returns>      
        public List<PersonalRelationship> CreateRelationshipBetweenTwoPersons(Person firstParty, Person secondParty, PersonalRelationshipTypeName firstPartyRole, 
                                                                        PersonalRelationshipTypeName secondPartyRole, GameTime.GameTime started, GameTime.GameTime ended = null)
        {
            string description = PersonalRelationshipDetails.Instance.GetRelationshipDescription(firstPartyRole.ToString());
            string reverseDescription = PersonalRelationshipDetails.Instance.GetRelationshipDescription(secondPartyRole.ToString());
            var type = PersonalRelationshipDetails.Instance.GetRelationshipTypeName(firstPartyRole.ToString());

            var newRelationship1 = new PersonalRelationship(secondParty, type, Guid.NewGuid(), description)
            {
                Name = firstPartyRole + "-" + secondPartyRole,
                Started = started,
                Ended = ended
            };

            firstParty.AddRelationship(newRelationship1);

            var newRelationship2 = new PersonalRelationship(firstParty, type, Guid.NewGuid(), reverseDescription)
            {
                Name = firstPartyRole + "-" + secondPartyRole,
                Started = started,
                Ended = ended
            };

            secondParty.AddRelationship(newRelationship2);

            return new List<PersonalRelationship> { newRelationship1, newRelationship2 };
        }

        /// <summary>
        /// Creates and defines a link between 2 events and add a reference to both Events
        /// </summary>
        /// <param name="firstEvent">The first Event to link</param>
        /// <param name="secondEvent">The second Event to link</param>
        /// <param name="firstToSecondEventRelationship">The nature of the link</param>
        /// <returns>The new links (2)</returns>      
        public List<EventRelationship> CreateLinkBetweenTwoEvents(PastEvent firstEvent, PastEvent secondEvent, EventRelationshipType firstToSecondEventRelationship)
        {
            string description = string.Empty;
            string reverseDescription = string.Empty;
            EventRelationshipType inverseRelationshipType = EventRelationshipType.Followed;

            switch (firstToSecondEventRelationship)
            {
                case EventRelationshipType.Caused:
                    description = EventRelationshipDescriptions.Caused;
                    reverseDescription = EventRelationshipDescriptions.CausedBy;
                    inverseRelationshipType = EventRelationshipType.CausedBy;
                    break;
                case EventRelationshipType.CausedBy:
                    description = EventRelationshipDescriptions.CausedBy;
                    reverseDescription = EventRelationshipDescriptions.Caused;
                    inverseRelationshipType = EventRelationshipType.Caused;
                    break;
                case EventRelationshipType.Followed:
                    description = EventRelationshipDescriptions.Followed;
                    reverseDescription = EventRelationshipDescriptions.Preceded;
                    inverseRelationshipType = EventRelationshipType.Preceded;
                    break;
                case EventRelationshipType.PartOf:
                    description = EventRelationshipDescriptions.PartOf;
                    reverseDescription = EventRelationshipDescriptions.Included;
                    inverseRelationshipType = EventRelationshipType.Included;
                    break;
                case EventRelationshipType.Preceded:
                    description = EventRelationshipDescriptions.Preceded;
                    reverseDescription = EventRelationshipDescriptions.Followed;
                    inverseRelationshipType = EventRelationshipType.Followed;
                    break;
            }

            EventRelationship directRelationship = new EventRelationship(secondEvent, firstToSecondEventRelationship, Guid.NewGuid(), description);
            firstEvent.AddEventLink(directRelationship);

            EventRelationship reverseRelationship = new EventRelationship(firstEvent, inverseRelationshipType, Guid.NewGuid(), reverseDescription);
            secondEvent.AddEventLink(reverseRelationship);

            return new List<EventRelationship> { directRelationship, reverseRelationship };
        }

        /// <summary>
        /// This creates a link between two places and adds the reference to both places
        /// </summary>
        /// <param name="firstPlaceToLink">First place to link</param>
        /// <param name="secondPlaceToLink">Second Place to link</param>
        /// <param name="firstToSecondPlaceRelationship">Nature of their link</param>
        /// <returns>The new links (2)</returns>        
        public List<PlaceRelationship> CreateLinkBetweenTwoPlaces(Place firstPlaceToLink, Place secondPlaceToLink, GeographicRelationshipType firstToSecondPlaceRelationship)
        {
            PlaceRelationship directRelationship = new PlaceRelationship(secondPlaceToLink, firstToSecondPlaceRelationship, Guid.NewGuid());
            firstPlaceToLink.AddLinkedPlace(directRelationship);

            PlaceRelationship reverseRelationship = new PlaceRelationship(firstPlaceToLink, GetReverseLinkType(firstToSecondPlaceRelationship), Guid.NewGuid());
            secondPlaceToLink.AddLinkedPlace(reverseRelationship);

            return new List<PlaceRelationship> {directRelationship, reverseRelationship};
        }

        /// <summary>
        /// This creates a link between two places and adds the reference to both places
        /// </summary>
        /// <param name="firstPlaceToLink">First place to link</param>
        /// <param name="secondPlaceToLink">Second Place to link</param>
        /// <param name="firstToSecondPlaceRelationship">Nature of their link</param>
        /// <param name="started">Date Started</param>
        /// <param name="ended">Date Ended</param>
        /// <returns>The new links (2)</returns>        
        public List<PlaceRelationship> CreateLinkBetweenTwoPlaces(Place firstPlaceToLink, Place secondPlaceToLink, GeographicRelationshipType firstToSecondPlaceRelationship, GameTime.GameTime started, GameTime.GameTime ended = null)
        {
            PlaceRelationship directRelationship = new PlaceRelationship(secondPlaceToLink, firstToSecondPlaceRelationship, Guid.NewGuid(), started, ended);
            firstPlaceToLink.AddLinkedPlace(directRelationship);

            PlaceRelationship reverseRelationship = new PlaceRelationship(firstPlaceToLink, GetReverseLinkType(firstToSecondPlaceRelationship), Guid.NewGuid(), started, ended);
            secondPlaceToLink.AddLinkedPlace(reverseRelationship);

            return new List<PlaceRelationship> { directRelationship, reverseRelationship };
        }

        /// <summary>
        /// Returns the opposite of the link
        /// </summary>
        /// <param name="relationshipType">The type of link</param>
        /// <returns>The reverse of the link</returns>
        private GeographicRelationshipType GetReverseLinkType(GeographicRelationshipType relationshipType)
        {
            switch (relationshipType)
            {
                case GeographicRelationshipType.EastOf:
                    return GeographicRelationshipType.WestOf;
                case GeographicRelationshipType.Includes:
                    return GeographicRelationshipType.PartOf;
                case GeographicRelationshipType.NorthEastOf:
                    return GeographicRelationshipType.SouthWestOf;
                case GeographicRelationshipType.NorthOf:
                    return GeographicRelationshipType.SouthOf;
                case GeographicRelationshipType.NorthWestOf:
                    return GeographicRelationshipType.SouthEastOf;
                case GeographicRelationshipType.PartOf:
                    return GeographicRelationshipType.Includes;
                case GeographicRelationshipType.SouthEastOf:
                    return GeographicRelationshipType.NorthWestOf;
                case GeographicRelationshipType.SouthOf:
                    return GeographicRelationshipType.NorthOf;
                case GeographicRelationshipType.SouthWestOf:
                    return GeographicRelationshipType.NorthEastOf;
                case GeographicRelationshipType.WestOf:
                    return GeographicRelationshipType.EastOf;
                default:
                    return GeographicRelationshipType.PartOf;
            }
        }

        #endregion

        #region Intermediate classes construction

        /// <summary>
        /// Creates a link between a Person and a Place.
        /// </summary>
        /// <param name="linkedPerson">Linked Person</param>
        /// <param name="linkedPlace">Linked Place</param>
        /// <param name="type">The nature of the tie between a Person and a Place</param>
        /// <returns>The new link</returns>        
        public PersonalTie CreateTieBetweenPersonAndPlace(Person linkedPerson, Place linkedPlace, PersonalTieType type)
        {
            var personalTie = new PersonalTie(linkedPlace, linkedPerson, type, Guid.NewGuid());

            linkedPerson.AddLinkedPlace(personalTie);
            linkedPlace.AddLinkedPerson(personalTie);

            return personalTie;
        }

        /// <summary>
        /// Creates a link between a Person and a Place.
        /// </summary>
        /// <param name="linkedPerson">Linked Person</param>
        /// <param name="linkedPlace">Linked Place</param>
        /// <param name="type">The nature of the tie between a Person and a Place</param>
        /// <param name="started">Date Started</param>
        /// <param name="ended">Date Ended</param>
        /// <returns>The new link</returns>        
        public PersonalTie CreateTieBetweenPersonAndPlace(Person linkedPerson, Place linkedPlace, PersonalTieType type, GameTime.GameTime started, GameTime.GameTime ended = null)
        {
            var personalTie = new PersonalTie(linkedPlace, linkedPerson, type, Guid.NewGuid(), started, ended);

            linkedPerson.AddLinkedPlace(personalTie);
            linkedPlace.AddLinkedPerson(personalTie);

            return personalTie;
        }

        /// <summary>
        /// Creates a link that defines a link between a Person and an Event
        /// </summary>
        /// <param name="linkedPerson">Person linked to the event</param>
        /// <param name="linkedEvent">Event linked to the Person</param>
        /// <param name="type">How a Person was involved in an Event</param>
        public PersonalInvolvement CreateInvolvementBetweenPersonAndEvent(Person linkedPerson, PastEvent linkedEvent, PersonalInvolvementType type)
        {
            var personalInvolvement = new PersonalInvolvement(linkedEvent, linkedPerson, type, Guid.NewGuid());

            linkedPerson.AddPersonalInvolvement(personalInvolvement);
            linkedEvent.AddAssociatedPerson(personalInvolvement);

            return personalInvolvement;
        }

        /// <summary>
        /// Creates a link that defines a link between a Person and an Event
        /// </summary>
        /// <param name="linkedPerson">Linked Person</param>
        /// <param name="linkedEvent">Event linked</param>
        /// <param name="type">How a Person was involved in an Event</param>
        /// <param name="started">Date Started</param>
        /// <param name="ended">Date Ended</param>
        public PersonalInvolvement CreateInvolvementBetweenPersonAndEvent(Person linkedPerson, PastEvent linkedEvent, PersonalInvolvementType type, GameTime.GameTime started, GameTime.GameTime ended = null)
        {
            var personalInvolvement = new PersonalInvolvement(linkedEvent, linkedPerson, type, Guid.NewGuid(), started, ended);

            linkedPerson.AddPersonalInvolvement(personalInvolvement);
            linkedEvent.AddAssociatedPerson(personalInvolvement);

            return personalInvolvement;
        }

        /// <summary>
        /// This relationship links an Occupation and an Place. It was created to admit that someone might not know the Person linked to an Occupation, i.e. the King of Temeria.
        /// </summary>
        /// <param name="linkedOccupation">Occupation linked</param>
        /// <param name="linkedPlace">Linked Place</param>
        /// <param name="type">Nature of the link between Occupation and Place</param>
        /// <returns>The new link</returns>        
        public OccupationalTie CreateTieBetweenOccupationAndPlace(Occupation linkedOccupation, Place linkedPlace, OccupationalTieType type)
        {
            var occupationalTie = new OccupationalTie(linkedPlace, linkedOccupation, type, Guid.NewGuid());

            linkedOccupation.AddLinkedPlace(occupationalTie);
            linkedPlace.AddLinkedOccupation(occupationalTie);

            return occupationalTie;
        }

        /// <summary>
        /// This relationship links an Occupation and an Place. It was created to admit that someone might not know the Person linked to an Occupation, i.e. the King of Temeria.
        /// </summary>
        /// <param name="linkedOccupation">Occupation linked</param>
        /// <param name="linkedPlace">Linked Place</param>
        /// <param name="type">Nature of the link between Occupation and Place</param>
        /// <param name="started">Date Started</param>
        /// <param name="ended">Date Ended</param>
        /// <returns>The new link</returns>        
        public OccupationalTie CreateTieBetweenOccupationAndPlace(Occupation linkedOccupation, Place linkedPlace, OccupationalTieType type, GameTime.GameTime started, GameTime.GameTime ended = null)
        {
            var occupationalTie = new OccupationalTie(linkedPlace, linkedOccupation, type, Guid.NewGuid(), started, ended);

            linkedOccupation.AddLinkedPlace(occupationalTie);
            linkedPlace.AddLinkedOccupation(occupationalTie);

            return occupationalTie;
        }

        /// <summary>
        /// This relationship links an Occupation and an Event. It was created to admit that someone might not know the Person linked to an Occupation, i.e. the King of Temeria.
        /// </summary>
        /// <param name="linkedOccupation">Occupation linked</param>
        /// <param name="linkedEvent">Event linked</param>
        /// <param name="type">Nature of the link between Occupation and Event</param>
        /// <returns>The new link</returns>        
        public OccupationalInvolvement CreateInvolvementBetweenOccupationAndEvent(Occupation linkedOccupation, PastEvent linkedEvent, OccupationalInvolvementType type)
        {
            var occupationalInvolvement = new OccupationalInvolvement(linkedEvent, linkedOccupation, type, Guid.NewGuid());

            linkedOccupation.AddLinkedEvent(occupationalInvolvement);
            linkedEvent.AddOccupation(occupationalInvolvement);

            return occupationalInvolvement;
        }

        /// <summary>
        /// This relationship links an Occupation and an Event. It was created to admit that someone might not know the Person linked to an Occupation, i.e. the King of Temeria.
        /// </summary>
        /// <param name="linkedOccupation">Occupation linked</param>
        /// <param name="linkedEvent">Event linked</param>
        /// <param name="type">Nature of the link between Occupation and Event</param>
        /// <param name="started">Date Started</param>
        /// <param name="ended">Date Ended</param>
        /// <returns>The new link</returns>        
        public OccupationalInvolvement CreateInvolvementBetweenOccupationAndEvent(Occupation linkedOccupation, PastEvent linkedEvent, OccupationalInvolvementType type, GameTime.GameTime started, GameTime.GameTime ended = null)
        {
            var occupationalInvolvement = new OccupationalInvolvement(linkedEvent, linkedOccupation, type, Guid.NewGuid(), started, ended);

            linkedOccupation.AddLinkedEvent(occupationalInvolvement);
            linkedEvent.AddOccupation(occupationalInvolvement);

            return occupationalInvolvement;
        }

        /// <summary>
        /// Creates a link between an Event and a Place
        /// </summary>
        /// <param name="linkedEvent">Event linked</param>
        /// <param name="linkedPlace">Linked Place</param>
        /// <param name="type">Nature of the link between Place and Event</param>
        /// <returns>The new link</returns>        
        public Occurence CreateOccurenceBetweenEventAndPlace(PastEvent linkedEvent, Place linkedPlace, OccurenceType type)
        {
            var occurence = new Occurence(linkedPlace, linkedEvent, type, Guid.NewGuid());

            linkedPlace.AddLinkedEvent(occurence);
            linkedEvent.AddLinkedPlace(occurence);

            return occurence;
        }

        /// <summary>
        /// Creates a link between an Event and a Place
        /// </summary>
        /// <param name="linkedEvent">Event linked</param>
        /// <param name="linkedPlace">Linked Place</param>
        /// <param name="type">Nature of the link between Place and Event</param>
        /// <param name="started">Date Started</param>
        /// <param name="ended">Date Ended</param>
        /// <returns>The new link</returns>        
        public Occurence CreateOccurenceBetweenEventAndPlace(PastEvent linkedEvent, Place linkedPlace, OccurenceType type, GameTime.GameTime started, GameTime.GameTime ended = null)
        {
            var occurence = new Occurence(linkedPlace, linkedEvent, type, Guid.NewGuid(), started, ended);

            linkedPlace.AddLinkedEvent(occurence);
            linkedEvent.AddLinkedPlace(occurence);

            return occurence;
        }

        /// <summary>
        /// Creates an association between Person and Organization
        /// </summary>
        /// <param name="linkedPerson">Linked Person</param>
        /// <param name="linkedOrganization">Linked Organization</param>
        /// <param name="type">Nature of the Association</param>
        /// <returns>The new link</returns>        
        public Association CreateAssociationBetweenPersonAndOrganization(Person linkedPerson, Organization linkedOrganization, AssociationType type)
        {
            var association = new Association(linkedPerson, linkedOrganization, type, Guid.NewGuid());

            linkedPerson.AddAssociation(association);
            linkedOrganization.AddAssociation(association);
            
            return association;
        }

        /// <summary>
        /// Creates an association  between Person and Organization
        /// </summary>
        /// <param name="linkedPerson">Linked Person</param>
        /// <param name="linkedOrganization">Linked Organization</param>
        /// <param name="type">Nature of the Association</param>
        /// <param name="started">Date Started</param>
        /// <param name="ended">Date Ended</param>
        /// <returns>The new link</returns>        
        public Association CreateAssociationBetweenPersonAndOrganization(Person linkedPerson, Organization linkedOrganization, AssociationType type, GameTime.GameTime started, GameTime.GameTime ended = null)
        {
            var association = new Association(linkedPerson, linkedOrganization, type, Guid.NewGuid(), started, ended);

            linkedPerson.AddAssociation(association);
            linkedOrganization.AddAssociation(association);

            return association;
        }

        #endregion
    }
}