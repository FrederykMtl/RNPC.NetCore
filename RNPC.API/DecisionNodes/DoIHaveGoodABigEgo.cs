using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class DoIHaveGoodABigEgo : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //Automatic failure
            if (traits.Modesty <= ConfiguredPassFailValue)
            {
                return TestAttributeSmallerOrEqualThanSetValue(traits.Modesty, ConfiguredPassFailValue, "AutomaticFailure", Qualities.Modesty.ToString());
            }

            return TestAttributeAgainstRandomValue(traits.Modesty, string.Empty, Qualities.Modesty.ToString());
        }
    }
}