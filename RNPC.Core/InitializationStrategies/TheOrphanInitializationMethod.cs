using System.Collections.Generic;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.InitializationStrategies
{
    internal class TheOrphanInitializationMethod : InitializationTemplateMethod
    {
        internal TheOrphanInitializationMethod()
        {
            StrongPoints = new List<string>
            {
                "CriticalSense",
                "Compassion",
                "Determination"
            };
            WeakPoints = new List<string>
            {
                "Outlook",
                "Gregariousness"
            };
        }

        ///<inheritdoc/>
        protected override void AdjustQualitiesAccordingToArchetype(CharacterTraits traits)
        {
            //Strong points
            traits.CriticalSense = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Compassion = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Determination = RandomValueGenerator.GenerateStrongAttributeValue();

            //Weak points
            traits.Outlook = RandomValueGenerator.GenerateWeakAttributeValue();
            traits.Gregariousness = RandomValueGenerator.GenerateWeakAttributeValue();
        }

        ///<inheritdoc/>
        protected override void SetPersonalValuesAccordingToArchetype(CharacterTraits traits)
        {
            traits.PersonalValues.Add(PersonalValues.Autonomy);
            traits.PersonalValues.Add(PersonalValues.Independance);
            traits.PersonalValues.Add(PersonalValues.Freedom);
        }
    }
}