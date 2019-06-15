using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    // ReSharper disable once InconsistentNaming
    internal class AmIInAGoodMood : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            if(traits.ShortTermEmotions.Sadness <= 3 && traits.ShortTermEmotions.Anger <= 3)
                return TestAttributeGreaterOrEqualThanSetValue(traits.ShortTermEmotions.Happiness, 8, "Conditional on:Eval(Sadness,2)&&Eval(Anger,3)", 
                        Emotions.Happiness.ToString(), CharacteristicType.Emotion);

            return false;
        }
    }
}