using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmIQuickToAnger : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //Automatic failure
            //TODO: re-evalute the current number
            if (traits.ShortTermEmotions.Anger >= 30)
            {
                TestAttributeGreaterOrEqualThanSetValue(traits.ShortTermEmotions.Anger, 30, "AutomaticFailure", Emotions.Anger.ToString(), CharacteristicType.Emotion);
                return true;
            }

            //Automatic failure
            if (traits.Tolerance <= ConfiguredPassFailValue && traits.Quietude <= ConfiguredPassFailValue)
            {
                return TestAttributeSmallerOrEqualThanSetValue(traits.Tolerance, ConfiguredPassFailValue, $"Conditional on:Eval(Quietude, {ConfiguredPassFailValue})", Qualities.Tolerance.ToString()) &&
                       TestAttributeSmallerOrEqualThanSetValue(traits.Quietude, ConfiguredPassFailValue, $"Conditional on:Eval(Tolerance, {ConfiguredPassFailValue})", Qualities.Quietude.ToString());
            }

            return false;
        }
    }
}