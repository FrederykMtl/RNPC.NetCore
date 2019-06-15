using System.Collections.Generic;
using RNPC.Core.Enums;

namespace RNPC.Core.Memory
{
    public interface IItemLinkFactory
    {
        List<PersonalRelationship> CreateRelationshipBetweenTwoPersons(Person firstParty, Person secondParty, PersonalRelationshipTypeName firstPartyRole, PersonalRelationshipTypeName secondPartyRole);
        List<PersonalRelationship> CreateRelationshipBetweenTwoPersons(Person firstParty, Person secondParty, PersonalRelationshipTypeName firstPartyRole, PersonalRelationshipTypeName secondPartyRole, GameTime.GameTime started, GameTime.GameTime ended = null);
        List<EventRelationship> CreateLinkBetweenTwoEvents(PastEvent firstEvent, PastEvent secondEvent, EventRelationshipType firstToSecondEventRelationship);
        List<PlaceRelationship> CreateLinkBetweenTwoPlaces(Place firstPlaceToLink, Place secondPlaceToLink, GeographicRelationshipType firstToSecondPlaceRelationship);
        List<PlaceRelationship> CreateLinkBetweenTwoPlaces(Place firstPlaceToLink, Place secondPlaceToLink, GeographicRelationshipType firstToSecondPlaceRelationship, GameTime.GameTime started, GameTime.GameTime ended = null);
        //TODO: 2 occupations
        //TODO 2 organizations

        PersonalTie CreateTieBetweenPersonAndPlace(Person linkedPerson, Place linkedPlace, PersonalTieType type);
        PersonalTie CreateTieBetweenPersonAndPlace(Person linkedPerson, Place linkedPlace, PersonalTieType type, GameTime.GameTime started, GameTime.GameTime ended = null);
        PersonalInvolvement CreateInvolvementBetweenPersonAndEvent(Person linkedPerson, PastEvent linkedEvent, PersonalInvolvementType type);
        PersonalInvolvement CreateInvolvementBetweenPersonAndEvent(Person linkedPerson, PastEvent linkedEvent, PersonalInvolvementType type, GameTime.GameTime started, GameTime.GameTime ended = null);
        OccupationalTie CreateTieBetweenOccupationAndPlace(Occupation linkedOccupation, Place linkedPlace, OccupationalTieType type);
        OccupationalTie CreateTieBetweenOccupationAndPlace(Occupation linkedOccupation, Place linkedPlace, OccupationalTieType type, GameTime.GameTime started, GameTime.GameTime ended = null);
        OccupationalInvolvement CreateInvolvementBetweenOccupationAndEvent(Occupation linkedOccupation, PastEvent linkedEvent, OccupationalInvolvementType type);
        OccupationalInvolvement CreateInvolvementBetweenOccupationAndEvent(Occupation linkedOccupation, PastEvent linkedEvent, OccupationalInvolvementType type, GameTime.GameTime started, GameTime.GameTime ended = null);
        Occurence CreateOccurenceBetweenEventAndPlace(PastEvent linkedEvent, Place linkedPlace, OccurenceType type);
        Occurence CreateOccurenceBetweenEventAndPlace(PastEvent linkedEvent, Place linkedPlace, OccurenceType type, GameTime.GameTime started, GameTime.GameTime ended = null);
        Association CreateAssociationBetweenPersonAndOrganization(Person linkedPerson, Organization linkedOrganization, AssociationType type);
        Association CreateAssociationBetweenPersonAndOrganization(Person linkedPerson, Organization linkedOrganization, AssociationType type, GameTime.GameTime started, GameTime.GameTime ended = null);
    }
}