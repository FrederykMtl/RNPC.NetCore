using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class IsThereEnmityBetweenUs : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            Person personInConflictWith = memory.Persons.FindPersonByName(perceivedEvent.Source);

            if(personInConflictWith == null)
                return false;

            var relationship = memory.Me.WhatIsMyCurrentRelationshipWithThisPerson(personInConflictWith);

            return relationship != null && relationship.Type == PersonalRelationshipType.Enmity;
        }
    }
}