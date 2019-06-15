using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class DoILikeMeetingNewPeople : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //Automatic Success
            if (traits.Gregariousness >= ConfiguredPassFailValue)
            {
                return TestAttributeGreaterOrEqualThanSetValue(traits.Gregariousness, ConfiguredPassFailValue, "AutomaticSuccess", Qualities.Gregariousness.ToString(), CharacteristicType.Quality);
            }

            return TestAttributeAgainstRandomValue(traits.Gregariousness, string.Empty, Qualities.Gregariousness.ToString());
        }
    }
}