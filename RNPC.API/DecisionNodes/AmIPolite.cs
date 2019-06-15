using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmIPolite : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //Automatic success
            if (CharacterHasPersonalValue(PersonalValues.Respect, traits))
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