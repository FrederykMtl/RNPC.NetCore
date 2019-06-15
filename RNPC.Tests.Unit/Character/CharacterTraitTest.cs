using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.Core;
using RNPC.Core.Enums;

namespace RNPC.Tests.Unit.Character
{
    [TestClass]
    public class CharacterTraitTest
    {
        [TestMethod]
        public void New_WithValidArguments_ClassInstantiated()
        {
            //ACT
            CharacterTraits traits = new CharacterTraits("Sterling Archer", Sex.Male, Orientation.Bisexual);

            var emotions = traits.GetEmotionalStateValues();
            var qualities = traits.GetPersonalQualitiesValues();

            traits.SetQualityAttributeByName("Adaptiveness", 99);

            traits.ResetEmotions();

            Assert.AreEqual(qualities.Count, CharacterTraits.GetPersonalQualitiesCount());
            Assert.AreEqual(emotions.Count, CharacterTraits.GetEmotionalStatesCount());
            Assert.AreEqual(traits.Sex, Sex.Male);
            Assert.AreEqual(traits.Orientation, Orientation.Bisexual);
            Assert.AreNotEqual(traits.InternalId, String.Empty);
        }
    }
}
