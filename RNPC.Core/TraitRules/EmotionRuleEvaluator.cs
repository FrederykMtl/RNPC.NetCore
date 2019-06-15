using System.Collections.Generic;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.TraitRules
{
    internal class EmotionRuleEvaluator : IRuleEvaluator
    {
        /// <summary>
        /// Evaluates all rules and applies them
        /// </summary>
        /// <param name="traits">character traits</param>
        public void EvaluateAndApplyAllRules(CharacterTraits traits)
        {
            var rules = GetAllEmotionalTraitRules();

            foreach (var rule in rules)
            {
                var quality = traits.GetType().GetProperty(rule.Quality);
                var emotion = traits.LongTermEmotions.GetType().GetProperty(rule.Emotion);

                if (quality == null || emotion == null)
                    continue;

                int randomValue = RandomValueGenerator.GeneratePercentileIntegerValue();
                int qualityValue = (int)quality.GetValue(traits);
                int currentEmotionValue = (int)emotion.GetValue(traits.LongTermEmotions);

                if (rule.IsComparedBelowQualityValue && qualityValue > randomValue ||
                        (!rule.IsComparedBelowQualityValue && qualityValue < randomValue))
                {
                    //If conditions are met, emotion is modified, but by at most 3.
                    emotion.SetValue(traits.LongTermEmotions, currentEmotionValue + RandomValueGenerator.GenerateIntWithMaxValue(3));
                }
            }
        }

        /// <summary>
        /// returns a list of all the rules that initialize emotions
        /// </summary>
        /// <returns>a list of emotion rules</returns>
        private static IEnumerable<EmotionalTraitRule> GetAllEmotionalTraitRules()
        {
            return new List<EmotionalTraitRule>
            {
                new EmotionalTraitRule { Quality = "Adaptiveness", Emotion = "Happiness", IsComparedBelowQualityValue = true},
                new EmotionalTraitRule { Quality = "Ambition", Emotion = "Anger", IsComparedBelowQualityValue = true},
                new EmotionalTraitRule { Quality = "Awareness", Emotion = "Anger", IsComparedBelowQualityValue = true},
                new EmotionalTraitRule { Quality = "Changing", Emotion = "Happiness", IsComparedBelowQualityValue = true},
                new EmotionalTraitRule { Quality = "Compassion", Emotion = "Sadness", IsComparedBelowQualityValue = true},
                new EmotionalTraitRule { Quality = "CriticalSense", Emotion = "Fear", IsComparedBelowQualityValue = true},
                new EmotionalTraitRule { Quality = "Memory", Emotion = "Fear", IsComparedBelowQualityValue = true},
                new EmotionalTraitRule { Quality = "Quietude", Emotion = "Happiness", IsComparedBelowQualityValue = true},
                new EmotionalTraitRule { Quality = "Acuity", Emotion = "Sadness", IsComparedBelowQualityValue = false},
                new EmotionalTraitRule { Quality = "Adaptiveness", Emotion = "Sadness", IsComparedBelowQualityValue = false},
                new EmotionalTraitRule { Quality = "Ambition", Emotion = "Happiness", IsComparedBelowQualityValue = false},
                new EmotionalTraitRule { Quality = "Awareness", Emotion = "Happiness", IsComparedBelowQualityValue = false},
                new EmotionalTraitRule { Quality = "Charisma", Emotion = "Anger", IsComparedBelowQualityValue = false},
                new EmotionalTraitRule { Quality = "Confidence ", Emotion = "Fear", IsComparedBelowQualityValue = false},
                new EmotionalTraitRule { Quality = "CriticalSense", Emotion = "Sadness", IsComparedBelowQualityValue = false},
                new EmotionalTraitRule { Quality = "Introspection", Emotion = "Fear", IsComparedBelowQualityValue = false},
                new EmotionalTraitRule { Quality = "Memory", Emotion = "Happiness", IsComparedBelowQualityValue = false},
                new EmotionalTraitRule { Quality = "Quietude", Emotion = "Anger", IsComparedBelowQualityValue = false},
                new EmotionalTraitRule { Quality = "Outlook", Emotion = "Sadness", IsComparedBelowQualityValue = false},
                new EmotionalTraitRule { Quality = "SelfEsteem", Emotion = "Anger", IsComparedBelowQualityValue = false},
                new EmotionalTraitRule { Quality = "Tolerance", Emotion = "Anger", IsComparedBelowQualityValue = false},
                new EmotionalTraitRule { Quality = "Willpower ", Emotion = "Fear", IsComparedBelowQualityValue = false}
            };
        }
    }
}
