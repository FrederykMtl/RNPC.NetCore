using System.Collections.Generic;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.InitializationStrategies
{
    internal class TheInnocentInitializationMethod : InitializationTemplateMethod
    {
        internal TheInnocentInitializationMethod()
        {
            StrongPoints = new List<string>
            {
                "Outlook",
                "Tolerance",
                "Gregariousness"
            };
            WeakPoints = new List<string>
            {
                "CriticalSense",
                "Willpower"
            };
        }

        ///<inheritdoc/>
        protected override void AdjustQualitiesAccordingToArchetype(CharacterTraits traits)
        {
            //Strong points
            traits.Outlook = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Tolerance = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Gregariousness = RandomValueGenerator.GenerateStrongAttributeValue();

            //Weak points
            traits.CriticalSense = RandomValueGenerator.GenerateWeakAttributeValue();
            traits.Willpower = RandomValueGenerator.GenerateWeakAttributeValue();
        }

        ///<inheritdoc/>
        protected override void SetPersonalValuesAccordingToArchetype(CharacterTraits traits)
        {
            traits.PersonalValues.Add(PersonalValues.Honesty);
            traits.PersonalValues.Add(PersonalValues.Stability);
            traits.PersonalValues.Add(PersonalValues.Security);
        }
    }
}