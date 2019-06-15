using System.Collections.Generic;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.InitializationStrategies
{
    internal class TheJesterInitializationMethod : InitializationTemplateMethod
    {
        internal TheJesterInitializationMethod()
        {
            StrongPoints = new List<string>
            {
                "Acuity",
                "Expressiveness",
                "Inventiveness"
            };
            WeakPoints = new List<string>
            {
                "Conscience",
                "Selflessness"
            };
        }

        ///<inheritdoc/>
        protected override void AdjustQualitiesAccordingToArchetype(CharacterTraits traits)
        {
            //Strong points
            traits.Acuity = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Expressiveness = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Inventiveness = RandomValueGenerator.GenerateStrongAttributeValue();

            //Weak points
            traits.Conscience = RandomValueGenerator.GenerateWeakAttributeValue();
            traits.Selflessness = RandomValueGenerator.GenerateWeakAttributeValue();
        }

        ///<inheritdoc/>
        protected override void SetPersonalValuesAccordingToArchetype(CharacterTraits traits)
        {
            traits.PersonalValues.Add(PersonalValues.Curiosity);
            traits.PersonalValues.Add(PersonalValues.Humour);
            traits.PersonalValues.Add(PersonalValues.Pleasure);
        }
    }
}