using System.Collections.Generic;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.InitializationStrategies
{
    internal class TheMagicianInitializationMethod : InitializationTemplateMethod
    {
        internal TheMagicianInitializationMethod()
        {
            StrongPoints = new List<string>
            {
                "Acuity",
                "Changing",
                "Inventiveness"
            };
            WeakPoints = new List<string>
            {
                "Selflessness",
                "Charitable"
            };
        }

        ///<inheritdoc/>
        protected override void AdjustQualitiesAccordingToArchetype(CharacterTraits traits)
        {
            //Strong points
            traits.Acuity = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Changing = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Inventiveness = RandomValueGenerator.GenerateStrongAttributeValue();

            //Weak points
            traits.Selflessness = RandomValueGenerator.GenerateWeakAttributeValue();
            traits.Charitable = RandomValueGenerator.GenerateWeakAttributeValue();
        }

        ///<inheritdoc/>
        protected override void SetPersonalValuesAccordingToArchetype(CharacterTraits traits)
        {
            traits.PersonalValues.Add(PersonalValues.Curiosity);
            traits.PersonalValues.Add(PersonalValues.Learning);
            traits.PersonalValues.Add(PersonalValues.Knowledge);
        }
    }
}