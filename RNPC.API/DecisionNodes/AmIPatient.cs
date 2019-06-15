using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmIPatient : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //Automatic Success
            if (traits.Energy >= 40 && traits.Tolerance > ConfiguredPassFailValue)
            {
                return TestAttributeGreaterOrEqualThanSetValue(traits.Energy, ConfiguredPassFailValue, "AutomaticSuccess", "Energy", CharacteristicType.Energy) &&
                       TestAttributeGreaterOrEqualThanSetValue(traits.Tolerance, ConfiguredPassFailValue, "AutomaticSuccess", Qualities.Tolerance.ToString(), CharacteristicType.Quality);
            }

            return TestAttributeAgainstRandomValue(traits.Tolerance, string.Empty, Qualities.Tolerance.ToString());
        }
    }
}