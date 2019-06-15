using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmIBotheredByIntrusiveBehaviour : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //if they're brae enough to do that I respect that
            if (CharacterHasPersonalValue(PersonalValues.Boldness, traits))
            {
                return false;
            }

            //You should introduce yourself before asking a question!
            if (CharacterHasPersonalValue(PersonalValues.Tradition, traits))
            {
                return true;
            }

            if (traits.Gregariousness <= ConfiguredPassFailValue)
            {
                return TestAttributeSmallerOrEqualThanSetValue(traits.Gregariousness, ConfiguredPassFailValue, "AutomaticFailure", Qualities.Gregariousness.ToString());
            }

            return TestAttributeAgainstRandomValue(traits.Gregariousness, string.Empty, Qualities.Gregariousness.ToString());
        }
    }
}