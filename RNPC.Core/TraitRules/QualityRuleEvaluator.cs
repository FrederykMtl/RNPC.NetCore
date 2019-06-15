using System;
using System.Collections.Generic;
using System.Reflection;

namespace RNPC.Core.TraitRules
{
    internal class QualityRuleEvaluator : IRuleEvaluator
    {
        /// <summary>
        /// Evaluate and applies all rules, no exception
        /// </summary>
        /// <param name="traits">character traits</param>
        public void EvaluateAndApplyAllRules(CharacterTraits traits)
        {
            var rules = GetAllPersonalQualitiesRules();
            foreach (var rule in rules)
            {
                if (rule.IsBidirectional)
                    EvaluateBidirectionalRule(traits, rule);
                else
                    EvaluateSimpleRule(traits, rule);
            }
        }

        /// <summary>
        /// Evaluates rule that can work both ways
        /// </summary>
        /// <param name="traits">character traits to modify</param>
        /// <param name="rule">rules to evaluate</param>
        private void EvaluateBidirectionalRule(CharacterTraits traits, QualityTraitRule rule)
        {
            var leadingProperty = traits.GetType().GetProperty(rule.LeadingQuality);
            var attachedProperty = traits.GetType().GetProperty(rule.AttachedQuality);

            if (leadingProperty == null || attachedProperty == null)
                return;

            int leadingValue;
            int attachedValue;

            if (ValuesAreWithinRange(traits, rule, leadingProperty, attachedProperty, out leadingValue, out attachedValue))
                return;

            if (leadingValue > attachedValue)
            {
                //If the attached is a weak point AND the leading is a NOT a strong point, 
                //then we just lower the leading property. Otherwise we raise the attached quality.
                if (traits.WeakPoints.Contains(rule.AttachedQuality))
                {
                    if (!traits.StrongPoints.Contains(rule.LeadingQuality))
                        leadingProperty.SetValue(traits, attachedValue + rule.RangeLimit + 1);
                }
                else
                    attachedProperty.SetValue(traits, leadingValue - rule.RangeLimit + 1);
            }
            else
            {
                //If the leading value is a weakness AND the attached is not a strong point, 
                //we lower the attached property. Otherwise we raise the leading quality
                if (traits.WeakPoints.Contains(rule.LeadingQuality))
                {
                    if(!traits.StrongPoints.Contains(rule.AttachedQuality))
                        attachedProperty.SetValue(traits, leadingValue + rule.RangeLimit - 1);
                }
                else
                    leadingProperty.SetValue(traits, attachedValue - rule.RangeLimit + 1);
            }
        }

        /// <summary>
        /// Evaluates rule that can work one way
        /// </summary>
        /// <param name="traits">character traits to modify</param>
        /// <param name="rule">rules to evaluate</param>
        private static void EvaluateSimpleRule(CharacterTraits traits, QualityTraitRule rule)
        {
            var leadingProperty = traits.GetType().GetProperty(rule.LeadingQuality);
            var attachedProperty = traits.GetType().GetProperty(rule.AttachedQuality);

            if(leadingProperty == null || attachedProperty == null)
                return;

            int leadingValue;
            int attachedValue;

            if (ValuesAreWithinRange(traits, rule, leadingProperty, attachedProperty, out leadingValue, out attachedValue))
                return;

            if (leadingValue > attachedValue)
            {
                //We won't raise if it's a weakness
                if(!traits.WeakPoints.Contains(rule.AttachedQuality))
                    attachedProperty.SetValue(traits, leadingValue - rule.RangeLimit + 1);
            }
            else
            {
                //We won't lower it if it's a strong point
                if(!traits.StrongPoints.Contains(rule.AttachedQuality))
                    attachedProperty.SetValue(traits, leadingValue + rule.RangeLimit - 1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="traits"></param>
        /// <param name="rule"></param>
        /// <param name="leadingProperty"></param>
        /// <param name="attachedProperty"></param>
        /// <param name="leadingValue"></param>
        /// <param name="attachedValue"></param>
        /// <returns></returns>
        private static bool ValuesAreWithinRange(CharacterTraits traits, QualityTraitRule rule, PropertyInfo leadingProperty,
            PropertyInfo attachedProperty, out int leadingValue, out int attachedValue)
        {
            leadingValue = (int) leadingProperty.GetValue(traits);
            attachedValue = (int) attachedProperty.GetValue(traits);

            return Math.Abs(leadingValue - attachedValue) <= rule.RangeLimit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static IEnumerable<QualityTraitRule> GetAllPersonalQualitiesRules()
        {
            return new List<QualityTraitRule>
            {
                new QualityTraitRule { LeadingQuality = "Adaptiveness", AttachedQuality = "Changing", IsBidirectional = true, RangeLimit = 25 },
                new QualityTraitRule { LeadingQuality = "Inventiveness", AttachedQuality = "Imagination", IsBidirectional = false, RangeLimit = 30 },
                new QualityTraitRule { LeadingQuality = "Introspection", AttachedQuality = "Modesty", IsBidirectional = true, RangeLimit = 25 },
                new QualityTraitRule { LeadingQuality = "Selflessness", AttachedQuality = "Charitable", IsBidirectional = true, RangeLimit = 25 },
                new QualityTraitRule { LeadingQuality = "Charitable", AttachedQuality = "Compassion", IsBidirectional = true, RangeLimit = 25 },
                new QualityTraitRule { LeadingQuality = "Willpower", AttachedQuality = "Determination", IsBidirectional = true, RangeLimit = 25 },
                new QualityTraitRule { LeadingQuality = "Expressiveness", AttachedQuality = "Gregariousness", IsBidirectional = true, RangeLimit = 25 },
                new QualityTraitRule { LeadingQuality = "Acuity", AttachedQuality = "CriticalSense", IsBidirectional = true, RangeLimit = 25 },
                new QualityTraitRule { LeadingQuality = "Tact", AttachedQuality = "Sociability", IsBidirectional = true, RangeLimit = 30 },
                new QualityTraitRule { LeadingQuality = "SelfEsteem", AttachedQuality = "Confidence", IsBidirectional = true, RangeLimit = 25 },
                new QualityTraitRule { LeadingQuality = "Quietude", AttachedQuality = "Modesty", IsBidirectional = true, RangeLimit = 25 }
            };
        }
    }
}