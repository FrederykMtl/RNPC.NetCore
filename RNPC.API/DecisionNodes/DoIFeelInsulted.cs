using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class DoIFeelInsulted : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //If respect is important I'll automatically feel insulted
            if (CharacterHasPersonalValue(PersonalValues.Respect, traits))
            {
                return true;
            }

            //If my ego is "too big" I'll be incapable of choosing not to feel insulted
            if (traits.Modesty <= ConfiguredPassFailValue)
            {
                return TestAttributeSmallerOrEqualThanSetValue(traits.Modesty, ConfiguredPassFailValue, "AutomaticFailure", Qualities.Modesty.ToString());
            }

            return TestAttributeAgainstRandomValue(traits.Quietude, string.Empty, Qualities.Quietude.ToString());
        }
    }
}