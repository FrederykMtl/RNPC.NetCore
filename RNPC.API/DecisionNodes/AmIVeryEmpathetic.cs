using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmIVeryEmpathetic : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //Automatic Success
            if (traits.Energy >= 25 && traits.Compassion >= ConfiguredPassFailValue)
            {
                return TestAttributeGreaterOrEqualThanSetValue(traits.Energy, 25, "Conditional on:Eval(Energy,25)", "Energy", CharacteristicType.Energy) &&
                        TestAttributeGreaterOrEqualThanSetValue(traits.Compassion, ConfiguredPassFailValue, 
                            $"Conditional on:Eval(Compassion,{ConfiguredPassFailValue})", Qualities.Compassion.ToString(), CharacteristicType.Quality);
            }

            return TestAttributeAgainstRandomValue(traits.Compassion, string.Empty, Qualities.Compassion.ToString());
        }
    }
}