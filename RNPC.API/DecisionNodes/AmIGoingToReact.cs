using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmIGoingToReact : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            return TestAttributeGreaterOrEqualThanSetValue(traits.Gregariousness, ConfiguredPassFailValue, "AutomaticFailure", Qualities.Gregariousness.ToString(), CharacteristicType.Quality);
        }
    }
}