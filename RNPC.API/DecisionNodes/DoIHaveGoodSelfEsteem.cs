using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class DoIHaveGoodSelfEsteem : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //Automatic Success
            if (traits.SelfEsteem >= ConfiguredPassFailValue)
            {
                return TestAttributeGreaterOrEqualThanSetValue(traits.SelfEsteem, ConfiguredPassFailValue, "AutomaticSuccess", Qualities.SelfEsteem.ToString(), CharacteristicType.Quality);
            }

            return TestAttributeAgainstRandomValue(traits.SelfEsteem, string.Empty, Qualities.SelfEsteem.ToString());
        }
    }
}