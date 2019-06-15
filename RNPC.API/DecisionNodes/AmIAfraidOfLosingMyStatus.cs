using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmIAfraidOfLosingMyStatus : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //Is status important to me?
            return CharacterHasPersonalValue(PersonalValues.Status, traits) && TestAttributeAgainstRandomValue(traits.Confidence, "Conditional on:Value(Status)", Qualities.Confidence.ToString());
        }
    }
}