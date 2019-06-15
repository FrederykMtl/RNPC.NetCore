using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;
using Action = RNPC.Core.Action.Action;

namespace RNPC.API.DecisionNodes
{
    internal class DoesItFeelSincere : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            var action = perceivedEvent as Action;

            if (action?.Tone == Tone.Apologetic)
            {
                //if i'm insecure I'll believe it's insincere
                return !TestAttributeSmallerOrEqualThanSetValue(traits.Confidence, ConfiguredPassFailValue, string.Empty, Qualities.Confidence.ToString());
            }

            //if my awareness is low I won't realize it's not sincere
            return TestAttributeSmallerOrEqualThanSetValue(traits.Awareness, ConfiguredPassFailValue, string.Empty, Qualities.Awareness.ToString());
        }
    }
}