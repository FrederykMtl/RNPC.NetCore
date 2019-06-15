using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.Core;
using RNPC.Core.Enums;

namespace RNPC.Tests.Unit.DTO.TraitTests
{
    /// <summary>
    /// Summary description for CharacterTest
    /// </summary>
    [TestClass]
    public class CharacterTraitTest
    {
        [TestMethod]
        public void GetPersonalQualitiesValues_NormalCharacterCreation_29QualitiesExpected()
        {
            //Arrange
            CharacterTraits traits = new CharacterTraits("Fred Flintstone", Sex.Male);
            //Act
            var list= traits.GetPersonalQualitiesValues();
            //Assert
            Assert.AreEqual(list.Count, CharacterTraits.GetPersonalQualitiesCount());
        }

        [TestMethod]
        public void SetQualityAttributeByIndex_IndexOfCriticalSense_CriticalSenseSetTo87()
        {
            //Arrange
            CharacterTraits traits = new CharacterTraits("Deadpool", Sex.Male, Orientation.Undefined, Gender.Genderfluid);
            //Act
            traits.SetQualityAttributeByIndex(11, 87);
            //Assert
            Assert.AreEqual(87,traits.CriticalSense);
        }
    }
}