using System.Collections.Generic;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.InitializationStrategies
{
    internal class TheCreatorInitializationMethod : InitializationTemplateMethod
    {
        internal TheCreatorInitializationMethod()
        {
            StrongPoints = new List<string>
            {
                "Inventiveness",
                "Imagination",
                "Ambition"
            };
            WeakPoints = new List<string>
            {
                "Modesty",
                "Introspection"
            };
        }

        ///<inheritdoc/>
        protected override void AdjustQualitiesAccordingToArchetype(CharacterTraits traits)
        {
            //Strong points
            traits.Inventiveness = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Imagination = RandomValueGenerator.GenerateStrongAttributeValue();
            traits.Ambition = RandomValueGenerator.GenerateStrongAttributeValue();

            //Weak points
            traits.Modesty = RandomValueGenerator.GenerateWeakAttributeValue();
            traits.Introspection = RandomValueGenerator.GenerateWeakAttributeValue();
        }

        ///<inheritdoc/>
        protected override void SetPersonalValuesAccordingToArchetype(CharacterTraits traits)
        {
            traits.PersonalValues.Add(PersonalValues.Achievement);
            traits.PersonalValues.Add(PersonalValues.Creativity);
            traits.PersonalValues.Add(PersonalValues.Growth);
        }
    }
}