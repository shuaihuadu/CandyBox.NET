// Copyright (c) IdeaTech. All rights reserved.

namespace System;

/// <summary>
/// Common extensions of <see cref="DateTime"/>.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    ///  Returns a string representing of a <see cref="DateTime"/> relative value.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/></param>
    /// <param name="format">A custom format string can contain two placeholders {0} and {1}, which represent the amount of time and the time unit, respectively.
    /// <br />For example, '{0} {1} ago' will output something like '1 year ago' or '3 months ago'.</param>
    /// <returns>The string value of relative time.</returns>
    public static string ToRelativeTime(this DateTime dateTime, string format)
    {
        TimeSpan difference = DateTime.Now - dateTime;

        if (difference.TotalSeconds < 50)
        {
            return "Just now";
        }
        else if (difference.TotalMinutes < 1)
        {
            int seconds = (int)difference.TotalSeconds;
            string unit = seconds == 1 ? "second" : "seconds";

            return string.Format(CultureInfo.InvariantCulture, format, seconds, unit);
        }
        else if (difference.TotalHours < 1)
        {
            int minutes = (int)difference.TotalMinutes;
            string unit = minutes == 1 ? "minute" : "minutes";

            return string.Format(CultureInfo.InvariantCulture, format, minutes, unit);
        }
        else if (difference.TotalDays < 1)
        {
            int hours = (int)difference.TotalHours;
            string unit = hours == 1 ? "hour" : "hours";

            return string.Format(CultureInfo.InvariantCulture, format, hours, unit);
        }
        else if (difference.TotalDays < 7)
        {
            int days = (int)difference.TotalDays;
            string unit = days == 1 ? "day" : "days";

            return string.Format(CultureInfo.InvariantCulture, format, days, unit);
        }
        else if (difference.TotalDays < 30)
        {
            int weeks = (int)(difference.TotalDays / 7);
            string unit = weeks == 1 ? "week" : "weeks";

            return string.Format(CultureInfo.InvariantCulture, format, weeks, unit);
        }
        else if (difference.TotalDays < 365)
        {
            int months = (int)(difference.TotalDays / 30);
            string unit = months == 1 ? "month" : "months";

            return string.Format(CultureInfo.InvariantCulture, format, months, unit);
        }
        else
        {
            int years = (int)(difference.TotalDays / 365);
            string unit = years == 1 ? "year" : "years";

            return string.Format(CultureInfo.InvariantCulture, format, years, unit);
        }
    }

    /// <summary>
    /// Returns the <see cref="DateTime"/> with max time value of the specified date.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/></param>
    /// <returns>The datetime with max time of the <paramref name="dateTime"/></returns>
    public static DateTime ToDateWithMaxTime(this DateTime dateTime) => dateTime.Date.AddDays(1).AddMilliseconds(-1);

    /// <summary>
    /// Returns the <see cref="DateTime"/> with min time value of the specified date.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/></param>
    /// <returns>The datetime with min time of the <paramref name="dateTime"/></returns>
    public static DateTime ToDateWithMinTime(this DateTime dateTime) => dateTime.Date;

    /// <summary>
    /// Returns the min value of sql server datetime.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/></param>
    /// <returns>1753/01/01 00:00:00 000</returns>
    public static DateTime SqlServerMinValue(this DateTime dateTime) => new(1753, 1, 1, 0, 0, 0, 0);

    /// <summary>
    /// Returns the min value of sql server datetime.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/></param>
    /// <returns>1753/01/01 00:00:00 000</returns>
    public static DateTime SqlServerMinValue(this DateTime? dateTime) => new(1753, 1, 1, 0, 0, 0, 0);

    /// <summary>
    /// Returns the max value of sql server datetime.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/></param>
    /// <returns>9999/12/31 23:59:59 997</returns>
    public static DateTime SqlServerMaxValue(this DateTime dateTime) => new(9999, 12, 31, 23, 59, 59, 997);

    /// <summary>
    /// Returns the max value of sql server datetime.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/></param>
    /// <returns>9999/12/31 23:59:59 997</returns>
    public static DateTime SqlServerMaxValue(this DateTime? dateTime) => new(9999, 12, 31, 23, 59, 59, 997);

    /// <summary>
    /// Returns the safe value of sql server datetime.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/></param>
    /// <returns>A safe value of sql server dateTime</returns>
    public static DateTime ToSqlServerSafeDateTime(this DateTime dateTime)
    {
        DateTime sqlServerMaxDateTime = new(9999, 12, 31, 23, 59, 59, 997);
        DateTime sqlServerMinDateTime = new(1753, 1, 1, 0, 0, 0, 0);

        if (dateTime > sqlServerMaxDateTime)
        {
            dateTime = sqlServerMaxDateTime;
        }

        if (dateTime < sqlServerMinDateTime)
        {
            dateTime = sqlServerMinDateTime;
        }

        return dateTime!;
    }

    /// <summary>
    /// Returns the safe value of sql server datetime.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/></param>
    /// <returns>A safe value of sql server dateTime</returns>
    public static DateTime ToSqlServerSafeDateTime(this DateTime? dateTime)
    {
        if (dateTime.HasValue)
        {
            return dateTime.Value.ToSqlServerSafeDateTime();
        }

        return dateTime.SqlServerMinValue();
    }

    /// <summary>
    /// Returns the ages of the specified date time until now.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/></param>
    /// <returns>The age.</returns>
    public static int Age(this DateTime dateTime)
    {
        if (dateTime > DateTime.Now)
        {
            return 0;
        }

        int age = DateTime.Now.Year - dateTime.Year;

        if (DateTime.Now < dateTime.AddYears(age))
        {
            age--;
        }

        return age;
    }

    /// <summary>
    /// Indicates whether the specified date time is a working day.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/></param>
    /// <returns>true if the value is Monday,Tuesday,Wednesday,Thursday or Friday;otherwise, false.</returns>
    public static bool IsWorkingDay(this DateTime dateTime) => dateTime.DayOfWeek != DayOfWeek.Saturday && dateTime.DayOfWeek != DayOfWeek.Sunday;

    /// <summary>
    /// Indicates whether the specified date time is weekend.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/></param>
    /// <returns>true if the value is Saturday or Sunday;otherwise, false.</returns>
    public static bool IsWeekend(this DateTime dateTime) => dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;

    /// <summary>
    /// Determines whether [is null or empty].
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns>
    ///   <c>true</c> if [is null or empty] [the specified date time]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsNullOrEmpty(this DateTime dateTime) => dateTime == DateTime.MinValue || dateTime <= DateTimeExtensionConstants.DB_NULL_DATETIME;

    /// <summary>
    /// To the month string.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <param name="partten">The partten.</param>
    /// <returns></returns>
    public static string ToMonthString(this DateTime dateTime, string partten = DateTimeExtensionConstants.DEFAULT_DATE_MONTH_FORMAT_PARTTEN)
    {
        if (dateTime.IsNullOrEmpty())
        {
            return string.Empty;
        }

        return dateTime.ToString(partten, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// To the application month string.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <param name="emptyContent">The empty content.</param>
    /// <param name="partten">The partten.</param>
    /// <returns></returns>
    public static string ToMonthString(this DateTime? dateTime, string emptyContent = "", string partten = DateTimeExtensionConstants.DEFAULT_DATE_FORMAT_PARTTEN)
    {
        if (!dateTime.HasValue)
        {
            return emptyContent;
        }

        return dateTime.Value.ToMonthString(partten);
    }

    /// <summary>
    /// To the application date partten string.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <param name="partten">The partten.</param>
    /// <returns></returns>
    public static string ToDateParttenString(this DateTime dateTime, string partten = DateTimeExtensionConstants.DEFAULT_DATE_FORMAT_PARTTEN)
    {
        if (dateTime.IsNullOrEmpty())
        {
            return string.Empty;
        }

        return dateTime.ToString(partten, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// To the application date partten string.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <param name="emptyContent">The empty content.</param>
    /// <param name="partten">The partten.</param>
    /// <returns></returns>
    public static string ToDateParttenString(this DateTime? dateTime, string emptyContent = "", string partten = DateTimeExtensionConstants.DEFAULT_DATE_FORMAT_PARTTEN)
    {
        if (!dateTime.HasValue)
        {
            return emptyContent;
        }

        return dateTime.Value.ToDateParttenString(partten);
    }

    /// <summary>
    /// To the application date time string.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <param name="partten">The partten.</param>
    /// <returns></returns>
    public static string ToDateTimeString(this DateTime dateTime, string partten = DateTimeExtensionConstants.DEFAULT_DATETIME_FORMAT_PARTTEN)
    {
        if (dateTime.IsNullOrEmpty())
        {
            return string.Empty;
        }

        return dateTime.ToString(partten, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// To the application date time string.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <param name="emptyContent">The empty content.</param>
    /// <param name="partten">The partten.</param>
    /// <returns></returns>
    public static string ToDateTimeString(this DateTime? dateTime, string emptyContent = "", string partten = DateTimeExtensionConstants.DEFAULT_DATETIME_FORMAT_PARTTEN)
    {
        if (!dateTime.HasValue)
        {
            return emptyContent;
        }

        return dateTime.Value.ToDateTimeString(partten);
    }

    /// <summary>
    /// To the date with minimum time.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    public static DateTime? ToDateWithMinTime(this DateTime? dateTime) => dateTime.HasValue ? dateTime.Value.ToDateWithMinTime() : dateTime;

    /// <summary>
    /// To the date with maximum time.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    public static DateTime? ToDateWithMaxTime(this DateTime? dateTime) => dateTime.HasValue ? dateTime.Value.ToDateWithMaxTime() : dateTime;

    /// <summary>
    /// To the date with maximum time.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    public static DateTime? ToSystemDateWithMaxTime(this DateTime? dateTime) => dateTime.HasValue ? dateTime.Value.ToSystemDateWithMaxTime() : dateTime;

    /// <summary>
    /// To the date with maximum time.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    public static DateTime ToSystemDateWithMaxTime(this DateTime dateTime) => dateTime.AddDays(1).AddSeconds(-1);

    /// <summary>
    /// Returns the first day of month of specified <paramref name="dateTime"/>.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    public static DateTime FirstDayOfMonth(this DateTime dateTime) => dateTime.AddDays(1 - dateTime.Day).Date;

    /// <summary>
    /// Returns the last day of the month of specified <paramref name="dateTime"/>.
    /// </summary>
    /// <param name="dateTime">A dateTime</param>
    /// <param name="subtracteMilliseconds">The milliseconds to be subtracted.</param>
    /// <returns>The last day of the month of the <paramref name="dateTime"/></returns>
    public static DateTime LastDayOfMonth(this DateTime dateTime, int subtracteMilliseconds = 1) => dateTime.AddDays(1 - dateTime.Day).Date.AddMonths(1).AddMilliseconds(subtracteMilliseconds * -1);
}
