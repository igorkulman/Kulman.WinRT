using System;

namespace Kulman.WinRT.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ConvertFromUnixTimestamp(uint timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }


        public static double ConvertToUnixTimestamp(this DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        /// <summary>
        /// Get the first date of the week for a certain date, provided
        /// that the first day of the week is Monday, the first week of
        /// a year is the one that includes the first Thursday of that
        /// year and the last week of a year is the one that immediately
        /// precedes the first calendar week of the next year.
        /// </summary>
        /// <param name="date">ISO 8601 date of interest.</param>
        /// <returns>The first week date.</returns>
        public static DateTime GetFirstDateOfWeek(this DateTime date)
        {
            if (date == DateTime.MinValue)
                return date;

            int week = date.GetWeekNumber();
            while (week == date.GetWeekNumber())
                date = date.AddDays(-1);
            return date.AddDays(1);
        }

        /// <summary>
        /// Get the last date of the week for a certain date, provided
        /// that the first day of the week is Monday, the first week of
        /// a year is the one that includes the first Thursday of that
        /// year and the last week of a year is the one that immediately
        /// precedes the first calendar week of the next year.
        /// </summary>
        /// <param name="date">ISO 8601 date of interest.</param>
        /// <returns>The first week date.</returns>
        public static DateTime GetLastDateOfWeek(this DateTime date)
        {
            if (date == DateTime.MaxValue)
                return date;

            int week = date.GetWeekNumber();
            while (week == date.GetWeekNumber())
                date = date.AddDays(1);
            return date.AddDays(-1);
        }


        /// <summary>Get the week number of a certain date, provided that
        /// the first day of the week is Monday, the first week of a year
        /// is the one that includes the first Thursday of that year and
        /// the last week of a year is the one that immediately precedes
        /// the first calendar week of the next year.
        /// </summary>
        /// <param name="date">Date of interest.</param>
        /// <returns>The week number.</returns>
        public static int GetWeekNumber(this DateTime date)
        {
            //Constants
            const int JAN = 1;
            const int DEC = 12;
            const int LASTDAYOFDEC = 31;
            const int FIRSTDAYOFJAN = 1;
            const int THURSDAY = 4;
            bool thursdayFlag = false;

            //Get the day number since the beginning of the year
            int dayOfYear = date.DayOfYear;

            //Get the first and last weekday of the year
            int startWeekDayOfYear = (int)(new DateTime(date.Year, JAN, FIRSTDAYOFJAN)).DayOfWeek;
            int endWeekDayOfYear = (int)(new DateTime(date.Year, DEC, LASTDAYOFDEC)).DayOfWeek;

            //Compensate for using monday as the first day of the week
            if (startWeekDayOfYear == 0)
                startWeekDayOfYear = 7;
            if (endWeekDayOfYear == 0)
                endWeekDayOfYear = 7;

            //Calculate the number of days in the first week
            int daysInFirstWeek = 8 - (startWeekDayOfYear);

            //Year starting and ending on a thursday will have 53 weeks
            if (startWeekDayOfYear == THURSDAY || endWeekDayOfYear == THURSDAY)
                thursdayFlag = true;

            //We begin by calculating the number of FULL weeks between
            //the year start and our date. The number is rounded up so
            //the smallest possible value is 0.
            int fullWeeks = (int)Math.Ceiling((dayOfYear - (daysInFirstWeek)) / 7.0);
            int resultWeekNumber = fullWeeks;

            //If the first week of the year has at least four days, the
            //actual week number for our date can be incremented by one.
            if (daysInFirstWeek >= THURSDAY)
                resultWeekNumber = resultWeekNumber + 1;

            //If the week number is larger than 52 (and the year doesn't
            //start or end on a thursday), the correct week number is 1.
            if (resultWeekNumber > 52 && !thursdayFlag)
                resultWeekNumber = 1;

            //If the week number is still 0, it means that we are trying
            //to evaluate the week number for a week that belongs to the
            //previous year (since it has 3 days or less in this year).
            //We therefore execute this function recursively, using the
            //last day of the previous year.
            if (resultWeekNumber == 0)
                resultWeekNumber = GetWeekNumber(new DateTime(date.Year - 1, DEC, LASTDAYOFDEC));
            return resultWeekNumber;
        }
    }
}
