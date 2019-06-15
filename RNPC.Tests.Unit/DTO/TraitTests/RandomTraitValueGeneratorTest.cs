using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.Core.TraitGeneration;

namespace RNPC.Tests.Unit.DTO.TraitTests
{
    [TestClass]
    public class RandomTraitValueGeneratorTest
    {
        [TestMethod]
        public void GeneratePercentileValue_1000Calls_1000NumbersBetweenOneAndOneHundred()
        {
            //ARRANGE
            List<int> generatedValues = new List<int>();
            //ACT
            for (int i = 0; i < 1000; i++)
            {
                generatedValues.Add(RandomValueGenerator.GeneratePercentileIntegerValue());
            }

            //ASSERT
            //Checking if values are within the boundaries
            Assert.IsTrue(generatedValues.All(x => x > 0 && x <= 100));
            //checking that some values are at the limits
            Assert.IsTrue(generatedValues.Any(x => x == 1));
            Assert.IsTrue(generatedValues.Any(x => x == 100));
        }
    }
}