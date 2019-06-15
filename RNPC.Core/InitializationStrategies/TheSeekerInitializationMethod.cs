using System.Collections.Generic;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.InitializationStrategies
{
    internal class TheSeekerInitializationMethod : InitializationTemplateMethod
    {
        internal TheSeekerInitializationMethod()
        {
            StrongPoints = new List<string>
            {
                "Ambition",
                "Imagination",
                "Adaptiveness"
            };
            WeakPoints = new List<string>
            {
                "Tolerance",
                "Introspection"
            };
        }

        ///<inheritdoc/>
        protected override void AdjustQualitiesAccordingToArchetype(CharacterTraits traits)
        {
            //Strong points
            traits.Ambition = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Imagination = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Adaptiveness = RandomValueGenerator.GenerateStrongAttributeValue();

            //Weak points
            traits.Tolerance = RandomValueGenerator.GenerateWeakAttributeValue();
            traits.Introspection = RandomValueGenerator.GenerateWeakAttributeValue();
        }

        ///<inheritdoc/>
        protected override void SetPersonalValuesAccordingToArchetype(CharacterTraits traits)
        {
            traits.PersonalValues.Add(PersonalValues.Autonomy);
            traits.PersonalValues.Add(PersonalValues.Adventure);
            traits.PersonalValues.Add(PersonalValues.Learning);
        }
    }
}