using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.Core;
using RNPC.Core.Enums;
using RNPC.Core.TraitRules;

namespace RNPC.Tests.Unit.DTO.TraitTests
{
    [TestClass]
    public class RuleEvaluatorTest
    {
        [TestMethod]
        public void EvaluateAndApplyAllRules_RandomlyGeneratedCharacter_ValuesNormalizedAfterCall()
        {
            //Arrange
            QualityRuleEvaluator evaluator = new QualityRuleEvaluator();

            CharacterTraits traits = new CharacterTraits("Lara Croft", Sex.Female)
            {
                Adaptiveness = 90,
                Changing = 45,
                Inventiveness = 20,
                Imagination = 66,
                WeakPoints = new List<string> {"Inventiveness"},
                StrongPoints = new List<string> {"Adaptiveness"}
            };

            var currentQualityValues = traits.GetPersonalQualitiesValues();
            //Act
            evaluator.EvaluateAndApplyAllRules(traits);

            var newValues = traits.GetPersonalQualitiesValues();

            int differenceCount = currentQualityValues.Count(qualityValue => qualityValue.Value != newValues[qualityValue.Key]);

            //Assert
            Assert.IsTrue(differenceCount > 0);
        }
    }
}
