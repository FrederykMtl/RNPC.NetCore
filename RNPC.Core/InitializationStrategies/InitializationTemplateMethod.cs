using System.Collections.Generic;
using RNPC.Core.Resources;
using RNPC.Core.TraitGeneration;
using RNPC.Core.TraitRules;

namespace RNPC.Core.InitializationStrategies
{
    internal abstract class InitializationTemplateMethod
    {
        internal List<string> StrongPoints;
        internal List<string> WeakPoints;

        /// <summary>
        /// Template Method
        /// </summary>
        /// <param name="traits">Character traits to initialize</param>
        /// <param name="qualityRuleEvaluator"></param>
        /// <param name="emotionRuleEvaluator"></param>
        public void Initialize(ref CharacterTraits traits, IRuleEvaluator qualityRuleEvaluator,
            IRuleEvaluator emotionRuleEvaluator)
        {
            RandomlyInitializeAllQualities(traits);

            AdjustQualitiesAccordingToArchetype(traits);

            SetPersonalValuesAccordingToArchetype(traits);

            AddRandomStrongPoint(traits);
            AddRandomWeakPoint(traits);

            traits.StrongPoints = StrongPoints;
            traits.WeakPoints = WeakPoints;

            traits.SetMyEnergy(traits.Energetic <= Constants.MaxWeakPoint ? 25 : 50);

            ApplyRulesToQualityTraits(traits, qualityRuleEvaluator);

            InitializeEmotionalStates(traits, emotionRuleEvaluator);
        }

        /// <summary>
        /// Initializes all qualities with a random value, to be adjusted later
        /// </summary>
        /// <param name="traits">character traits</param>
        private static void RandomlyInitializeAllQualities(CharacterTraits traits)
        {
            traits.Acuity = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Adaptiveness = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Ambition = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Assertiveness = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Awareness = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Changing = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Charisma = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Charitable = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Compassion = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Confidence = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Conscience = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.CriticalSense = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Determination = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Energetic = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Expressiveness = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Gregariousness = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Imagination = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Introspection = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Inventiveness = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Memory = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Modesty = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Quietude = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Outlook = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.SelfEsteem = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Selflessness = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Sociability = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Tolerance = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Tact = RandomValueGenerator.GenerateMidRangeAttributeValue();
            traits.Willpower = RandomValueGenerator.GenerateMidRangeAttributeValue();
        }

        /// <summary>
        /// Adjust specific values according to archetype
        /// </summary>
        /// <param name="traits">Character traits</param>
        protected virtual void AdjustQualitiesAccordingToArchetype(CharacterTraits traits)
        {
        }

        /// <summary>
        /// Applies the rules attached to qualities
        /// </summary>
        /// <param name="traits"></param>
        /// <param name="qualityRuleEvaluator"></param>
        private void ApplyRulesToQualityTraits(CharacterTraits traits, IRuleEvaluator qualityRuleEvaluator)
        {
            qualityRuleEvaluator.EvaluateAndApplyAllRules(traits);
        }

        /// <summary>
        /// Initializes a number of personal values according to archetype.
        /// </summary>
        /// <param name="traits">Character traits</param>
        protected virtual void SetPersonalValuesAccordingToArchetype(CharacterTraits traits)
        {
        }

        /// <summary>
        /// Calculates intial emotional states based on archetype and qualities
        /// </summary>
        /// <param name="traits">Character traits</param>
        /// <param name="emotionRuleEvaluator">rule evaluator</param>
        protected void InitializeEmotionalStates(CharacterTraits traits, IRuleEvaluator emotionRuleEvaluator)
        {
            emotionRuleEvaluator.EvaluateAndApplyAllRules(traits);
        }

        /// <summary>
        /// Adds 1 to 4 additional strong points
        /// </summary>
        /// <param name="traits"></param>
        protected void AddRandomStrongPoint(CharacterTraits traits)
        {
            int numberOfStrongPoints = 4;
            int demographicPercentage = RandomValueGenerator.GeneratePercentileIntegerValue();

            if (demographicPercentage > int.Parse(TraitDistribution.StrongPointTier1) &&
                demographicPercentage <= int.Parse(TraitDistribution.StrongPointTier2))
            {
                numberOfStrongPoints = 5;
            }

            if (demographicPercentage > int.Parse(TraitDistribution.StrongPointTier2) &&
                demographicPercentage <= int.Parse(TraitDistribution.StrongPointTier3))
            {
                numberOfStrongPoints = 6;
            }
            if (demographicPercentage > int.Parse(TraitDistribution.StrongPointTier3))
            {
                numberOfStrongPoints = 7;
            }

            while (StrongPoints.Count < numberOfStrongPoints)
            {
                int randomTraitIndex = RandomValueGenerator.GenerateIntWithMaxValue(CharacterTraits.GetPersonalQualitiesCount() - 1);

                var property = traits.GetQualityPropertyByIndex(randomTraitIndex);

                if (property == null || StrongPoints.Contains(property.Name) || WeakPoints.Contains(property.Name))
                    continue;

                property.SetValue(traits, RandomValueGenerator.GenerateStrongAttributeValue());

                StrongPoints.Add(property.Name);
            }
        }

        /// <summary>
        /// Adds 1 to 4 additional weak points
        /// </summary>
        /// <param name="traits"></param>
        protected void AddRandomWeakPoint(CharacterTraits traits)
        {
            int numberOfWeakPoints = 3;
            int demographicPercentage = RandomValueGenerator.GeneratePercentileIntegerValue();

            if (demographicPercentage > int.Parse(TraitDistribution.WeakPointTier1) &&
                demographicPercentage <= int.Parse(TraitDistribution.WeakPointTier2))
            {
                numberOfWeakPoints = 4;
            }
            if (demographicPercentage > int.Parse(TraitDistribution.WeakPointTier2) &&
                demographicPercentage <= int.Parse(TraitDistribution.WeakPointTier3))
            {
                numberOfWeakPoints = 5;
            }
            if (demographicPercentage > int.Parse(TraitDistribution.WeakPointTier3))
            {
                numberOfWeakPoints = 6;
            }

            while (WeakPoints.Count < numberOfWeakPoints)
            {
                int randomTraitIndex = RandomValueGenerator.GenerateIntWithMaxValue(CharacterTraits.GetPersonalQualitiesCount() - 1);

                var property = traits.GetQualityPropertyByIndex(randomTraitIndex);

                if (property == null || StrongPoints.Contains(property.Name) || WeakPoints.Contains(property.Name))
                    continue;

                property.SetValue(traits, RandomValueGenerator.GenerateWeakAttributeValue());

                WeakPoints.Add(property.Name);
            }
        }
    }
}