using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.Core.GameTime;

namespace RNPC.Tests.Unit.DTO.DateTime
{
    [TestClass]
    public class CustomDateTimeTest
    {
        [TestMethod]
        public void TimeElapsedInDaysSince_ValidGameDate_ReturnCorrectNumberOfDays()
        {
            //ARRANGE
            CustomDateTime time = new CustomDateTime(5196, 7, 11);
            time.SetNumberOfDaysPerMonth(20);
            time.SetNumberOfMonthsInYear(18);
            //ACT
            long days = time.TimeElapsedInDaysSince(new CustomDateTime(5196, 11, 1, 1, 2));
            //ASSERT
            Assert.AreEqual(360, time.GetNumberOfDaysInYear());
            Assert.AreEqual(70, days);
        }

        [TestMethod]
        public void TimeElapsedInDaysSince_InvalidGameDate_ReturnZero()
        {
            //ARRANGE
            CustomDateTime time = new CustomDateTime(5196, 7, 11); ;
            //ACT
            long days = time.TimeElapsedInDaysSince(new StandardDateTime(new System.DateTime(5196, 11, 1)));
            //ASSERT
            Assert.AreEqual(0, days);
        }

        [TestMethod]
        public void TimeElapsedInYearsSince_InvalidGameDate_ReturnZero()
        {
            //ARRANGE
            CustomDateTime time = new CustomDateTime(5196, 7, 11);
            //ACT
            long days = time.TimeElapsedInYearsSince(new StandardDateTime(new System.DateTime(5196, 11, 1)));
            //ASSERT
            Assert.AreEqual(0, days);
        }
    }
}
