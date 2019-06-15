using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class IsThisPersonFriendOrFamily : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            var person = memory.Persons.FindPersonByName(perceivedEvent.Source);

            if (person == null)
                return false;

            var relationship = memory.Me.WhatIsMyCurrentRelationshipWithThisPerson(person);

            return relationship != null && (relationship.Type == PersonalRelationshipType.Friendship || 
                   relationship.Type == PersonalRelationshipType.Romantic || 
                   relationship.Type == PersonalRelationshipType.Family);
        }
    }
}