using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmIWitty : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            return TestAttributeGreaterOrEqualThanSetValue(traits.Acuity, ConfiguredPassFailValue, "Conditional on:Eval(Energy,30)", Qualities.Acuity.ToString(), CharacteristicType.Quality) &&
                   TestAttributeGreaterOrEqualThanSetValue(traits.Energy, 20, $"Conditional on:Eval(Acuity,{ConfiguredPassFailValue})", "Energy", CharacteristicType.Energy);
        }
    }
}