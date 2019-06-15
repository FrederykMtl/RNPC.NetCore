using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    // ReSharper disable once InconsistentNaming
    internal class AmIInABadMood : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            return TestAttributeGreaterOrEqualThanSetValue(traits.ShortTermEmotions.Anger, 10, string.Empty, Emotions.Anger.ToString(), CharacteristicType.Emotion) ||
                   TestAttributeGreaterOrEqualThanSetValue(traits.ShortTermEmotions.Sadness, 15, string.Empty, Emotions.Sadness.ToString(), CharacteristicType.Emotion) ||
                    TestAttributeGreaterOrEqualThanSetValue(traits.ShortTermEmotions.Disgust, 25, string.Empty, Emotions.Disgust.ToString(), CharacteristicType.Emotion);
        }
    }
}