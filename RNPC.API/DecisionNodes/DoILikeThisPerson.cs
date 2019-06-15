using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class DoILikeThisPerson : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            var person = memory.Persons.FindPersonByName(perceivedEvent.Source);

            if (person == null)
                return false;

            var opinion = memory.WhatIsMyOpinionAbout(person);

            return opinion == OpinionType.Like || opinion == OpinionType.Love || opinion == OpinionType.Respect;
        }
    }
}