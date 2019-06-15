using System.Collections.Generic;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.InitializationStrategies
{
    internal class TheLoverInitializationMethod : InitializationTemplateMethod
    {
        internal TheLoverInitializationMethod()
        {
            StrongPoints = new List<string>
            {
                "Charisma",
                "Expressiveness",
                "Sociability"
            };
            WeakPoints = new List<string>
            {
                "Ambition",
                "Willpower"
            };
        }

        ///<inheritdoc/>
        protected override void AdjustQualitiesAccordingToArchetype(CharacterTraits traits)
        {
            //Strong points
            traits.Charisma = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Expressiveness = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Sociability = RandomValueGenerator.GenerateStrongAttributeValue();

            //Weak points
            traits.Ambition = RandomValueGenerator.GenerateWeakAttributeValue();
            traits.Willpower = RandomValueGenerator.GenerateWeakAttributeValue();
        }

        ///<inheritdoc/>
        protected override void SetPersonalValuesAccordingToArchetype(CharacterTraits traits)
        {
            traits.PersonalValues.Add(PersonalValues.Love);
            traits.PersonalValues.Add(PersonalValues.Happiness);
            traits.PersonalValues.Add(PersonalValues.Pleasure);
        }
    }
}