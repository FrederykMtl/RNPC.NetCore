using System;

namespace RNPC.Core.GameTime
{
    [Serializable]
    public class CustomDateTime : GameTime
    {
        public CustomDateTime(int year, int month, int day, int hour, int? minute) : base(year, month, day, hour, minute)
        {
            //default values
            _numberOfMonths = 12;
            _numberOfDaysByMonth = 30;
        }

        public CustomDateTime(int year, int month, int? day) : base(year, month, day, null, null)
        {
            //default values
            _numberOfMonths = 12;
            _numberOfDaysByMonth = 30;
        }

        public CustomDateTime(int year) : base(year, null, null, null, null)
        {
            //default values
            _numberOfMonths = 12;
            _numberOfDaysByMonth = 30;
        }

        public CustomDateTime()
        {
        }

        private int _numberOfMonths;
        private int _numberOfDaysByMonth;

        public void SetNumberOfDaysPerMonth(int days)
        {
            _numberOfDaysByMonth = days;
        }

        public void SetNumberOfMonthsInYear(int months)
        {
            _numberOfMonths = months;
        }

        public long GetNumberOfDaysInYear()
        {
            return _numberOfMonths * _numberOfDaysByMonth;
        }

        public override void SetMonth(int? month)
        {
            Month = month;
        }

        public override void SetDay(int? day)
        {
            Day = day;
        }

        public override long TimeElapsedInDaysSince(GameTime date)
        {
            if (!(date is CustomDateTime))
                return 0;

            var endDate = (CustomDateTime)date;

            var numberOfDaysInFirstDate = (endDate.Year * _numberOfMonths + endDate.Month ?? 1) * _numberOfDaysByMonth + endDate.Day ?? 1;
            var numberOfDaysInSecondDate = (Year * _numberOfMonths + Month ?? 1) * _numberOfDaysByMonth + Day ?? 1;

            return Math.Abs(numberOfDaysInFirstDate - numberOfDaysInSecondDate);
        }

        public override long TimeElapsedInYearsSince(GameTime date)
        {
            if (!(date is CustomDateTime))
                return 0;

            var endDate = (CustomDateTime) date;

            if ((!Month.HasValue && !Day.HasValue) ||
                (!endDate.Month.HasValue && !endDate.Day.HasValue))
            {
                return endDate.Year - Year;
            }

            long totalDays = TimeElapsedInDaysSince(endDate);

            long extraDays = totalDays % GetNumberOfDaysInYear();

            return (totalDays - extraDays) / GetNumberOfDaysInYear();

        }
    }
}