using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmISarcastic : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            return TestAttributeGreaterOrEqualThanSetValue(traits.Acuity, ConfiguredPassFailValue, $"Conditional on:Eval(Expressiveness,{ConfiguredPassFailValue})", Qualities.Acuity.ToString(), CharacteristicType.Quality) &&
                   TestAttributeGreaterOrEqualThanSetValue(traits.Expressiveness, ConfiguredPassFailValue, $"Conditional on:Eval(Acuity,{ConfiguredPassFailValue})", Qualities.Expressiveness.ToString(), CharacteristicType.Quality);
        }
    }
}