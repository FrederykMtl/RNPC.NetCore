using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class DoIHideMyFeelings : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            if (CharacterHasPersonalValue(PersonalValues.Openness, traits) || CharacterHasPersonalValue(PersonalValues.Honesty, traits))
                return false;

            return TestAttributeAgainstRandomValue(traits.Expressiveness, string.Empty, Qualities.Expressiveness.ToString());
        }
    }
}