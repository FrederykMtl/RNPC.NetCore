using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.Core.Enums;
using RNPC.Core.Learning;

namespace RNPC.Tests.Unit.Learning
{
    [TestClass]
    public class PersonalValueAssociationsTest
    {
        [TestMethod]
        public void GetAssociatedValue_BadValueRadness_NullReturned()
        {
            //ARRANGE
            PersonalValueAssociations associations = new PersonalValueAssociations();
            //ACT
            var result = associations.GetAssociatedValue("Radness");
            //ASSERT
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAssociatedValue_PersonalValueFaith_ReturnAssociatedValue()
        {
            //ARRANGE
            PersonalValueAssociations associations = new PersonalValueAssociations();

            //ACT
            var result = associations.GetAssociatedValue("Faith");

            //ASSERT
            Assert.IsNotNull(result);

            bool adequateAssociatedValue = result == PersonalValues.Loyalty || result == PersonalValues.Tradition || result == PersonalValues.Community;

            Assert.IsTrue(adequateAssociatedValue);
        }

        [TestMethod]
        public void GetAssociatedValue_PersonalValueFaith_CheckDistribution()
        {
            //ARRANGE
            PersonalValueAssociations associations = new PersonalValueAssociations();

            int[] counts = {0, 0, 0};

            //ACT
            for (int i = 0; i < 1000; i++)
            {
                var result = associations.GetAssociatedValue("Faith");

                if (result == PersonalValues.Loyalty)
                {
                    counts[0]++;
                }

                if (result == PersonalValues.Tradition)
                {
                    counts[1]++;
                }

                if (result == PersonalValues.Community)
                {
                    counts[2]++;
                }
            }

            //ASSERT
            Assert.IsNotNull(counts);
            //We assume a variation of about 3.5%s
            Assert.IsTrue(counts[0] > 300 && counts[0] < 365);
            Assert.IsTrue(counts[1] > 300 && counts[1] < 365);
            Assert.IsTrue(counts[2] > 300 && counts[2] < 365);
        }
    }
}
