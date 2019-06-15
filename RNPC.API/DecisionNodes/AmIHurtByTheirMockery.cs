using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmIHurtByTheirMockery : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //Automatic success
            if (traits.Modesty >= ConfiguredPassFailValue)
            {
                return TestAttributeGreaterOrEqualThanSetValue(traits.Modesty, ConfiguredPassFailValue, "AutomaticSuccess", Qualities.Modesty.ToString(), CharacteristicType.Quality);
            }

            return TestAttributeAgainstRandomValue(traits.Modesty, string.Empty, Qualities.Modesty.ToString());
        }
    }
}