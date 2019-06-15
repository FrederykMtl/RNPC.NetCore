using System;
using System.Collections.Generic;
using System.Linq;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.InitializationStrategies
{
    internal class RandomInitializationMethod : InitializationTemplateMethod
    {
        internal RandomInitializationMethod()
        {
            StrongPoints = new List<string>();
            WeakPoints = new List<string>();    
        }

        ///<inheritdoc/>
        protected override void AdjustQualitiesAccordingToArchetype(CharacterTraits traits)
        {
            while (StrongPoints.Count < 4)
            {
                int randomTraitIndex = RandomValueGenerator.GenerateIntWithMaxValue(CharacterTraits.GetPersonalQualitiesCount() - 1);

                var property = traits.GetQualityPropertyByIndex(randomTraitIndex);

                if (property == null || StrongPoints.Contains(property.Name) || WeakPoints.Contains(property.Name))
                    continue;

                property.SetValue(traits, RandomValueGenerator.GenerateStrongAttributeValue());

                StrongPoints.Add(property.Name);
            }

            var weakPoints = traits.GetPersonalQualitiesValues().Where(quality => quality.Value <= 20).ToList();

            WeakPoints.AddRange(weakPoints.Select(x => x.Key).Take(2));

            while (WeakPoints.Count < 2)
            {
                int randomTraitIndex = RandomValueGenerator.GenerateIntWithMaxValue(CharacterTraits.GetPersonalQualitiesCount() - 1);

                var property = traits.GetQualityPropertyByIndex(randomTraitIndex);

                if (property == null || StrongPoints.Contains(property.Name) || WeakPoints.Contains(property.Name))
                    continue;

                property.SetValue(traits, RandomValueGenerator.GenerateWeakAttributeValue());

                WeakPoints.Add(property.Name);
            }
        }

        ///<inheritdoc/>
        protected override void SetPersonalValuesAccordingToArchetype(CharacterTraits traits)
        {
            int numberOfValues = Enum.GetValues(typeof(PersonalValues)).Length;

            while (traits.PersonalValues.Count < 3)
            {
                int valueIndex = RandomValueGenerator.GenerateIntWithMaxValue(numberOfValues);

                var personalValue = (PersonalValues)valueIndex;

                if (!traits.PersonalValues.Contains(personalValue))
                    traits.PersonalValues.Add(personalValue);
            }
        }
    }
}