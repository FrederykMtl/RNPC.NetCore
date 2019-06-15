using System.Collections.Generic;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.InitializationStrategies
{
    internal class TheSageInitializationMethod : InitializationTemplateMethod
    {
        internal TheSageInitializationMethod()
        {
            StrongPoints = new List<string>
            {
                "Awareness",
                "CriticalSense",
                "Memory"
            };
            WeakPoints = new List<string>
            {
                "Outlook",
                "Imagination"
            };
        }

        ///<inheritdoc/>
        protected override void AdjustQualitiesAccordingToArchetype(CharacterTraits traits)
        {
            //Strong points
            traits.Awareness = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.CriticalSense = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Memory = RandomValueGenerator.GenerateStrongAttributeValue();

            //Weak points
            traits.Outlook = RandomValueGenerator.GenerateWeakAttributeValue();
            traits.Imagination = RandomValueGenerator.GenerateWeakAttributeValue();
        }

        ///<inheritdoc/>
        protected override void SetPersonalValuesAccordingToArchetype(CharacterTraits traits)
        {
            traits.PersonalValues.Add(PersonalValues.Knowledge);
            traits.PersonalValues.Add(PersonalValues.Truth);
            traits.PersonalValues.Add(PersonalValues.Wisdom);
        }

        /////<inheritdoc/>
        //protected override List<string> StrongPoints => new List<string>
        //{
        //    "Awareness",
        //    "CriticalSense",
        //    "Memory"
        //};
        /////<inheritdoc/>
        //protected override List<string> WeakPoints => new List<string>
        //{
        //    "Outlook",
        //    "Imagination"
        //};
    }
}