using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class DoIWantToPlacateHim : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //Automatic Success
            if (traits.Quietude >= ConfiguredPassFailValue)
            {
                return TestAttributeGreaterOrEqualThanSetValue(traits.Quietude, ConfiguredPassFailValue, "AutomaticSuccess", Qualities.Quietude.ToString(), CharacteristicType.Quality);
            }

            //Automatic Success
            if (traits.Tact >= ConfiguredPassFailValue)
            {
                return TestAttributeGreaterOrEqualThanSetValue(traits.Tact, ConfiguredPassFailValue, "AutomaticSuccess", Qualities.Tact.ToString(), CharacteristicType.Quality);
            }

            if (CharacterHasPersonalValue(PersonalValues.Peace, traits))
                return true;

            if(traits.Quietude > traits.Tact)
                return TestAttributeAgainstRandomValue(traits.Quietude, string.Empty, Qualities.Quietude.ToString());

            return TestAttributeAgainstRandomValue(traits.Tact, string.Empty, Qualities.Tact.ToString());
        }
    }
}