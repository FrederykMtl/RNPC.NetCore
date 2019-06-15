using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class WillIRespondVerbally : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //Automatic Success
            if (traits.Expressiveness >= ConfiguredPassFailValue)
            {
                return TestAttributeGreaterOrEqualThanSetValue(traits.Expressiveness, ConfiguredPassFailValue, "AutomaticSuccess", Qualities.Expressiveness.ToString(), CharacteristicType.Quality);
            }

            return TestAttributeAgainstRandomValue(traits.Expressiveness, string.Empty, Qualities.Expressiveness.ToString());
        }
    }
}