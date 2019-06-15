using System.Collections.Generic;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.InitializationStrategies
{
    //Determination, Tolerance, Willpower	Changing, Imagination	Honour, Strength, Service
    internal class TheWarriorInitializationMethod : InitializationTemplateMethod
    {
        internal TheWarriorInitializationMethod()
        {
            StrongPoints = new List<string>
            {
                "Determination",
                "Tolerance",
                "Willpower"
            };
            WeakPoints = new List<string>
            {
                "Changing",
                "Imagination"
            };
        }

        ///<inheritdoc/>
        protected override void AdjustQualitiesAccordingToArchetype(CharacterTraits traits)
        {
            //Strong points
            traits.Determination = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Tolerance = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Willpower = RandomValueGenerator.GenerateStrongAttributeValue();

            //Weak points
            traits.Changing = RandomValueGenerator.GenerateWeakAttributeValue();
            traits.Imagination = RandomValueGenerator.GenerateWeakAttributeValue();
        }

        ///<inheritdoc/>
        protected override void SetPersonalValuesAccordingToArchetype(CharacterTraits traits)
        {
            traits.PersonalValues.Add(PersonalValues.Honour);
            traits.PersonalValues.Add(PersonalValues.Strength);
            traits.PersonalValues.Add(PersonalValues.Service);
        }
    }
}