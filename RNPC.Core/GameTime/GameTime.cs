using System;

namespace RNPC.Core.GameTime
{
    [Serializable]
    public abstract class GameTime
    {
        //date
        protected int Year;
        protected int? Month;
        protected int? Day;
        //Time
        protected int? Hour;
        protected int? Minute;

        #region Constructors

        /// <summary>
        /// Constructor for a GameTime object
        /// </summary>
        /// <param name="year">Year of the date. Stored as an int, so it can be any value</param>
        /// <param name="month">Month of the date. Stored as an int, so it can be any value</param>
        /// <param name="day">Day of the date. Stored as an int, so it can be any value</param>
        /// <param name="hour">Hour of the date. Has to follow the 24h format</param>
        /// <param name="minute">Minute of the date. Has to follow the 60 minutes format</param>
        protected GameTime(int year, int? month, int? day, int? hour, int? minute)
        {
            Year = year;

            // ReSharper disable VirtualMemberCallInConstructor
            SetMonth(month);
            SetDay(day);
            // ReSharper restore VirtualMemberCallInConstructor

            Hour = hour;
            Minute = minute;
        }

        protected GameTime() { }

        #endregion

        #region Public methods

        public virtual void SetYear(int year)
        {
            Year = year;
        }

        public virtual void SetMonth(int? month)
        {
            if (month.HasValue && month < 1)
                Month = 1;
            else if (month.HasValue && month > 12)
                Month = 12;
            else
                Month = month;
        }

        public virtual long TimeElapsedInDaysSince(GameTime date)
        {
            return 0;
        }

        public virtual long TimeElapsedInYearsSince(GameTime date)
        {
            return 0;
        }

        public virtual int GetYear()
        {
            return Year;
        }

        /// <summary>
        /// Set day, defaulted to a max of 28 days. Override to make it smarter.
        /// </summary>
        /// <param name="day"></param>
        public virtual void SetDay(int? day)
        {
            if (day.HasValue && day < 1)
                Day = 1;
            else if (day.HasValue && day > DateTime.DaysInMonth(Year, Month ?? 1))
                Day = DateTime.DaysInMonth(Year, Month ?? 1);
            else
                Day = day;
        }

        /// <summary>
        /// Set the time. This one is not overridable to keep things simple.
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        public void SetTime(int? hour, int? minute)
        {
            if (hour.HasValue && hour.Value < 0)
                Hour = 0;
            else if (hour.HasValue && hour.Value > 23)
                Hour = 23;
            else
                Hour = hour;

            if (minute.HasValue && minute.Value < 0)
                Minute = 0;
            else if (minute.HasValue && minute.Value > 59)
                Hour = 59;
            else
                Minute = minute;
        }

        #endregion
    }
}