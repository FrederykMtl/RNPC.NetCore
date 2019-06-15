using System;

namespace RNPC.Core.GameTime
{
    [Serializable]
    public class StandardDateTime : GameTime
    {
        public StandardDateTime(DateTime date) : base(date.Year, date.Month, date.Day, date.Hour, date.Minute)
        {
        }

        public StandardDateTime() : base(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute)
        {}

        public void SetDatetime(DateTime date)
        {
            Year =date.Year;
            Month = date.Month;
            Day = date.Day;
            Hour = date.Hour;
            Minute = date.Minute;
        }

        public override long TimeElapsedInDaysSince(GameTime date)
        {
            if (!(date is StandardDateTime))
                return 0;

            var endDate = (StandardDateTime)date;


            var time = new DateTime(endDate.Year, endDate.Month ?? 1, endDate.Day ?? 1, endDate.Hour ?? 0, endDate.Minute ?? 0, 0) - GetDateTime();

            return (long)time.TotalDays;
        }

        public override long TimeElapsedInYearsSince(GameTime date)
        {
            if (!(date is StandardDateTime))
                return 0;

            var endDate = (StandardDateTime)date;

            if (!Month.HasValue && !Day.HasValue)
            {
                return endDate.Year - Year;
            }

            var totalDays = TimeElapsedInDaysSince(endDate);

            var extraDays = totalDays % 365;

            return (totalDays - extraDays) / 365;
        }

        public DateTime GetDateTime()
        {
            return new DateTime(Year, Month ?? 1, Day ?? 1, Hour ?? 1, Minute ?? 1, 1);
        }
    }
}
