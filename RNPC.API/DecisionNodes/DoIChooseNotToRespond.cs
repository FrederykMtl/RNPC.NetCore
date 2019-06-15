using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class DoIChooseNotToRespond : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //Automatic Success
            if (traits.Determination >= ConfiguredPassFailValue)
            {
                return TestAttributeGreaterOrEqualThanSetValue(traits.Determination, ConfiguredPassFailValue, "AutomaticSuccess", Qualities.Determination.ToString(), CharacteristicType.Quality);
            }

            if (traits.Quietude > traits.Tact)
                return TestAttributeAgainstRandomValue(traits.Quietude, string.Empty, Qualities.Quietude.ToString());

            return TestAttributeAgainstRandomValue(traits.Tact, string.Empty, Qualities.Tact.ToString());
        }
    }
}