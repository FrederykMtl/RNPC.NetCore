using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmIAfraidOfLosingThisRelationship : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            var person = memory.Persons.FindPersonByName(perceivedEvent.Source);

            if (person == null)
                return false;

            var relationship = memory.Me.WhatIsMyCurrentRelationshipWithThisPerson(person);

            if(relationship == null)
	    	    return false;

            if (relationship.Type == PersonalRelationshipType.Enmity)
                return false;

            var myOpinionOnThisRelationship = memory.WhatIsMyOpinionAbout(relationship);

            if (myOpinionOnThisRelationship == OpinionType.Like || myOpinionOnThisRelationship == OpinionType.Love)
            {
                return TestAttributeAgainstRandomValue(traits.Confidence, "Conditional on:Opinion(Like,Love)", Qualities.Confidence.ToString());
            }

            return false;
        }
    }
}