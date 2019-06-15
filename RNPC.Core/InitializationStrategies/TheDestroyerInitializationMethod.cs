using System.Collections.Generic;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.InitializationStrategies
{
    internal class TheDestroyerInitializationMethod : InitializationTemplateMethod
    {
        internal TheDestroyerInitializationMethod()
        {
            StrongPoints = new List<string>
            {
                "Modesty",
                "Tolerance",
                "Changing"
            };
            WeakPoints = new List<string>
            {
                "Compassion",
                "Conscience"
            };
        }

        ///<inheritdoc/>
        protected override void AdjustQualitiesAccordingToArchetype(CharacterTraits traits)
        {
            //Strong points
            traits.Modesty = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Tolerance = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Changing = RandomValueGenerator.GenerateStrongAttributeValue();

            //Weak points
            traits.Compassion = RandomValueGenerator.GenerateWeakAttributeValue();
            traits.Conscience = RandomValueGenerator.GenerateWeakAttributeValue();
        }

        ///<inheritdoc/>
        protected override void SetPersonalValuesAccordingToArchetype(CharacterTraits traits)
        {
            traits.PersonalValues.Add(PersonalValues.Justice);
            traits.PersonalValues.Add(PersonalValues.Growth);
            traits.PersonalValues.Add(PersonalValues.Community);
        }
    }
}