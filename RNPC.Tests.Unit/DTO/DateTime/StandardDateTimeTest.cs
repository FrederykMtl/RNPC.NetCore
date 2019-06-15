using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.Core.GameTime;

namespace RNPC.Tests.Unit.DTO.DateTime
{
    [TestClass]
    public class StandardDateTimeTest
    {
        [TestMethod]
        public void TimeElapsedInDaysSince_ValidGameDate_ReturnCorrectNumberOfDays()
        {
            //ARRANGE
            StandardDateTime time = new StandardDateTime(new System.DateTime(5196, 9, 1));
            //ACT
            long days = time.TimeElapsedInDaysSince(new StandardDateTime(new System.DateTime(5196, 11, 1, 1, 2, 0)));
            //ASSERT
            Assert.AreEqual(61, days);
        }

        [TestMethod]
        public void TimeElapsedInYearsSince_ValidGameDate_ReturnCorrectNumberOfDays()
        {
            //ARRANGE
            StandardDateTime time = new StandardDateTime();
            time.SetDatetime(new System.DateTime(5196, 9, 1));
            //ACT
            long days = time.TimeElapsedInYearsSince(new StandardDateTime(new System.DateTime(5203, 11, 1, 1, 2, 0)));
            //ASSERT
            Assert.AreEqual(7, days);
        }

        [TestMethod]
        public void TimeElapsedInDaysSince_InvalidGameDate_ReturnZero()
        {
            //ARRANGE
            StandardDateTime time = new StandardDateTime(new System.DateTime(5196, 7, 11));
            //ACT
            long days = time.TimeElapsedInDaysSince(new CustomDateTime(5196, 11, 1));
            //ASSERT
            Assert.AreEqual(0, days);
        }

        [TestMethod]
        public void TimeElapsedInYearsSince_InvalidGameDate_ReturnZero()
        {
            //ARRANGE
            StandardDateTime time = new StandardDateTime(new System.DateTime(5196, 7, 11));
            //ACT
            long days = time.TimeElapsedInYearsSince(new CustomDateTime(5196, 11, 1));
            //ASSERT
            Assert.AreEqual(0, days);
        }
    }
}
