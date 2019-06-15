using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.Core;
using RNPC.Core.Enums;
using RNPC.Core.InitializationStrategies;
using RNPC.Core.TraitRules;

namespace RNPC.Tests.Unit.DTO.Initializations
{
    [TestClass]
    public class RandomInitializationMethodTest
    {
        [TestMethod]
        public void Initialize_NewCharacterTraits_ValuesInitializedAndAdjusted()
        {
            CharacterTraits traits = new CharacterTraits("Ronald McDonald", Sex.Male, Orientation.Straight, Gender.Male);

            new RandomInitializationMethod().Initialize(ref traits, new QualityRuleEvaluator(), new EmotionRuleEvaluator());

            Assert.IsFalse(traits.GetPersonalQualitiesValues().Any(x => x.Value == 0));
            Assert.IsTrue(traits.PersonalValues.Count == 3);
            Assert.IsFalse(traits.GetEmotionalStateValues().Any(x => x.Value > 12));
        }
    }
}
