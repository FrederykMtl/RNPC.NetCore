using System.Collections.Generic;
using RNPC.Core.Enums;
using RNPC.Core.Memory;
using RNPC.Core.Resources;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.KnowledgeBuildingStrategies
{
    public class StandardKnowledgeRandomization : IKnowledgeRandomizationStrategy
    {
        private const int WeakPointRetentionValue = 35;
        private const int StrongPointRetentionValue = 80;
        private readonly int _knowledgeRetentionPercentage;
        private readonly int _knowledgeAccuracyPercentage;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="traits"></param>
        public StandardKnowledgeRandomization(CharacterTraits traits)
        {
            _knowledgeRetentionPercentage = CalculateRetentionPercentage(traits);
            _knowledgeAccuracyPercentage = CalculateAccuracyPercentage(traits);
        }

        /// <summary>
        /// Calculates the percentage of information (MemoryItems) that is retained
        /// </summary>
        /// <param name="traits">character traits that establish the percentage</param>
        /// <returns>Calculated percentage</returns>
        private static int CalculateRetentionPercentage(CharacterTraits traits)
        {
            int retentionPercentage = traits.PersonalValues.Contains(PersonalValues.Knowledge) ? 10 : 0;

            if (traits.Memory <= Constants.MaxWeakPoint)
                retentionPercentage += WeakPointRetentionValue;
            else if (traits.Memory >= Constants.MinStrongPoint)
                retentionPercentage += StrongPointRetentionValue;
            else
                retentionPercentage += (int) ((traits.Memory - Constants.MaxWeakPoint) * 0.75);

            return retentionPercentage;
        }

        /// <summary>
        /// Calculates the percentage of information (MemoryItems) that is accurate information
        /// </summary>
        /// <param name="traits">character traits that establish the percentage</param>
        /// <returns>Calculated percentage</returns>
        private int CalculateAccuracyPercentage(CharacterTraits traits)
        {
            int accuracyPercentage;
            //the value will be established by the avergae of memory and critical sense (do they believe everything they're told)
            int averageTraitsValue = (traits.Memory + traits.CriticalSense) / 2;

            if (averageTraitsValue > 20 && averageTraitsValue < StrongPointRetentionValue)
            {
                int lowerBoundValue;
                int upperBoundValue;

                int.TryParse(KnowledgeAccuracyParameters.AccuracyLowerBound, out lowerBoundValue);
                int.TryParse(KnowledgeAccuracyParameters.AccuracyUpperBound, out upperBoundValue);

                if (lowerBoundValue == 0)
                    lowerBoundValue = 50;

                if (upperBoundValue == 0)
                    upperBoundValue = StrongPointRetentionValue;

                //calculate the relative step within the range of 20 to 80
                // ReSharper disable once PossibleLossOfFraction
                decimal factor = (upperBoundValue - lowerBoundValue) / 60;

                accuracyPercentage = (int) ((averageTraitsValue - 20) * factor) + lowerBoundValue;
            }
            else if(averageTraitsValue >= 80)
            {
                int strongPointsValue;

                int.TryParse(KnowledgeAccuracyParameters.StrongPointsAccuracy, out strongPointsValue);

                accuracyPercentage = strongPointsValue==0? 0 : 83;
            }
            else
            {
                int weakPointsValue;

                int.TryParse(KnowledgeAccuracyParameters.WeakPointsAccuracy, out weakPointsValue);

                accuracyPercentage = weakPointsValue == 0 ? 0 : 83;
            }

            //Bonuses
            if (traits.PersonalValues.Contains(PersonalValues.Knowledge))
            {
                accuracyPercentage += int.Parse(KnowledgeAccuracyParameters.PersonalValueKnowledgeBonus);
            }
            if (traits.PersonalValues.Contains(PersonalValues.Curiosity))
            {
                accuracyPercentage += int.Parse(KnowledgeAccuracyParameters.PersonalValueCuriosityBonus);
            }
            if (traits.PersonalValues.Contains(PersonalValues.Truth))
            {
                accuracyPercentage += int.Parse(KnowledgeAccuracyParameters.PersonalValueTruthBonus);
            }

            if (accuracyPercentage > 98)
                accuracyPercentage = 98;

            return accuracyPercentage;
        }

        /// <summary>
        /// Randomizes the information, forgetting some of it and making some of it wrong.
        /// </summary>
        /// <param name="knowledgeBase">The complete bulk of knowledge to be randomized</param>
        /// <returns>The final, randomized knowledge content</returns>
        public List<MemoryItem> BuildRandomizedKnowledgeBase(List<MemoryItem> knowledgeBase)
        {
            List<MemoryItem> randomizedKnowledge = new List<MemoryItem>();
            foreach (var item in knowledgeBase)
            {
                if (DoIRememberThisInformation())
                {
                    randomizedKnowledge.Add(RandomizeKnowledgeItem(item));
                }
            }

            return randomizedKnowledge;
        }

        /// <summary>
        /// Randomly set some information to be wrong
        /// </summary>
        /// <param name="knowledgeItem">an item to randomize</param>
        /// <returns>The memory item with randomly falsified info</returns>
        public MemoryItem RandomizeKnowledgeItem(MemoryItem knowledgeItem)
        {
            int comparisonValue = RandomValueGenerator.GeneratePercentileIntegerValue();

            return _knowledgeAccuracyPercentage >= comparisonValue? knowledgeItem.GetAccurateCopy() : knowledgeItem.GetInaccurateCopy();
        }

        /// <summary>
        /// Am I going to remember this MemoryItem?
        /// </summary>
        /// <returns>true, remember the item, false I won't remember</returns>
        private bool DoIRememberThisInformation()
        {
            return _knowledgeRetentionPercentage >= RandomValueGenerator.GeneratePercentileIntegerValue();
        }
    }
}