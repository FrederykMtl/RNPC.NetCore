using System.Collections.Generic;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.InitializationStrategies
{
    internal class TheRulerInitializationMethod : InitializationTemplateMethod
    {
        internal TheRulerInitializationMethod()
        {
            StrongPoints = new List<string>
            {
                "Assertiveness",
                "Determination",
                "Expressiveness"
            };
            WeakPoints = new List<string>
            {
                "Inventiveness",
                "Imagination"
            };
        }

        ///<inheritdoc/>
        protected override void AdjustQualitiesAccordingToArchetype(CharacterTraits traits)
        {
            //Strong points
            traits.Assertiveness = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Determination = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Expressiveness = RandomValueGenerator.GenerateStrongAttributeValue();

            //Weak points
            traits.Inventiveness = RandomValueGenerator.GenerateWeakAttributeValue();
            traits.Imagination = RandomValueGenerator.GenerateWeakAttributeValue();
        }

        ///<inheritdoc/>
        protected override void SetPersonalValuesAccordingToArchetype(CharacterTraits traits)
        {
            traits.PersonalValues.Add(PersonalValues.Authority);
            traits.PersonalValues.Add(PersonalValues.Competency);
            traits.PersonalValues.Add(PersonalValues.Responsibility);
        }
    }
}