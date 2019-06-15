using System.Collections.Generic;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.InitializationStrategies
{
    internal class TheCaregiverInitializationMethod : InitializationTemplateMethod
    {
        internal TheCaregiverInitializationMethod()
        {
            StrongPoints = new List<string>
            {
                "Compassion",
                "Selflessness",
                "Tolerance"
            };
            WeakPoints = new List<string>
            {
                "Introspection",
                "SelfEsteem"
            };
        }

        ///<inheritdoc/>
        protected override void AdjustQualitiesAccordingToArchetype(CharacterTraits traits)
        {
            //Strong points
            traits.Compassion = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Selflessness = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Tolerance = RandomValueGenerator.GenerateStrongAttributeValue();

            //Weak points
            traits.Introspection = RandomValueGenerator.GenerateWeakAttributeValue();
            traits.SelfEsteem = RandomValueGenerator.GenerateWeakAttributeValue();

            AddRandomWeakPoint(traits);
        }

        ///<inheritdoc/>
        protected override void SetPersonalValuesAccordingToArchetype(CharacterTraits traits)
        {
            traits.PersonalValues.Add(PersonalValues.Service);
            traits.PersonalValues.Add(PersonalValues.Compassion);
            traits.PersonalValues.Add(PersonalValues.Community);
        }
    }
}