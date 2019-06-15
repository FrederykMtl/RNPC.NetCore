using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmIWillingToGetPhysical : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //Automatic failure
            if (traits.ShortTermEmotions.Anger >= 75)
            {
                return TestAttributeGreaterOrEqualThanSetValue(traits.ShortTermEmotions.Anger, 75, string.Empty, Emotions.Anger.ToString(), CharacteristicType.Emotion);
            }

            //Automatic failure
            if (traits.Conscience <= ConfiguredPassFailValue && traits.Tolerance <= ConfiguredPassFailValue)
            {
                return TestAttributeSmallerOrEqualThanSetValue(traits.Conscience, ConfiguredPassFailValue, $"Conditional on:Eval(Energy,{ConfiguredPassFailValue})", Qualities.Conscience.ToString()) &&
                       TestAttributeSmallerOrEqualThanSetValue(traits.Tolerance, ConfiguredPassFailValue, $"Conditional on:Eval(Compassion,{ConfiguredPassFailValue})", Qualities.Tolerance.ToString());
            }

            return false;
        }
    }
}