using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;
using RNPC.Core.Resources;

namespace RNPC.API.DecisionNodes
{
    internal class DoIWantToAvoidAConfrontation : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //Too blunt to avoid it
            if (traits.Tact <= ConfiguredPassFailValue)
            {
                return TestAttributeSmallerOrEqualThanSetValue(traits.Tact, ConfiguredPassFailValue, "AutomaticFailure", Qualities.Determination.ToString());
            }

            //Too determined to let it go
            if (traits.Determination >= Constants.MinStrongPoint)
            {
                return TestAttributeGreaterOrEqualThanSetValue(traits.Determination, Constants.MinStrongPoint, "AutonmaticFailure", Qualities.Determination.ToString(), CharacteristicType.Quality);
            }

            //If their determination is strong enough, they won't shy from a confrontation.
            return !TestAttributeAgainstRandomValue(traits.Determination, string.Empty, Qualities.Determination.ToString());
        }
    }
}