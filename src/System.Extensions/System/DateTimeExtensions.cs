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
    /// <param name="yearAgo">Singular form of year string</param>
    /// <param name="yearsAgo">Plural form of year string</param>
    /// <param name="monthAgo">Singular form of month string</param>
    /// <param name="monthsAgo">Plural form of month string</param>
    /// <param name="weekAgo">Singular form of week string</param>
    /// <param name="weeksAgo">Plural form of week string</param>
    /// <param name="dayAgo">Singular form of day string</param>
    /// <param name="daysAgo">Plural form of day string</param>
    /// <param name="hourAgo">Singular form of hour string</param>
    /// <param name="hoursAgo">Plural form of hour string</param>
    /// <param name="minuteAgo">Singular form of minute string</param>
    /// <param name="minutesAgo">Plural form of minute string</param>
    /// <param name="secondAgo">Singular form of second string</param>
    /// <param name="secondsAgo">Plural form of second string</param>
    /// <param name="justNow"></param>
    /// <returns>The string value of relative time.</returns>
    public static string ToRelativeTime(
        this DateTime dateTime,
        string yearAgo = "year ago", string yearsAgo = "years ago",
        string monthAgo = "month ago", string monthsAgo = "months ago",
        string weekAgo = "week ago", string weeksAgo = "weeks ago",
        string dayAgo = "day ago", string daysAgo = "days ago",
        string hourAgo = "hour ago", string hoursAgo = "hours ago",
        string minuteAgo = "minute ago", string minutesAgo = "minutes ago",
        string secondAgo = "second ago", string secondsAgo = "seconds ago",
        string justNow = "Just now")
    {
        TimeSpan diff = DateTime.Now - dateTime;

        string suffix = string.Empty;

        int numeral = 0;

        if (diff.TotalDays >= 365)
        {
            numeral = (int)Math.Floor(diff.TotalDays / 365);
            suffix = numeral == 1 ? yearAgo : yearsAgo;
        }
        else if (diff.TotalDays >= 31)
        {
            numeral = (int)Math.Floor(diff.TotalDays / 31);
            suffix = numeral == 1 ? monthAgo : monthsAgo;
        }
        else if (diff.TotalDays >= 7)
        {
            numeral = (int)Math.Floor(diff.TotalDays / 7);
            suffix = numeral == 1 ? weekAgo : weeksAgo;
        }
        else if (diff.TotalDays >= 1)
        {
            numeral = (int)Math.Floor(diff.TotalDays);
            suffix = numeral == 1 ? dayAgo : daysAgo;
        }
        else if (diff.TotalHours >= 1)
        {
            numeral = (int)Math.Floor(diff.TotalHours);
            suffix = numeral == 1 ? hourAgo : hoursAgo;
        }
        else if (diff.TotalMinutes >= 1)
        {
            numeral = (int)Math.Floor(diff.TotalMinutes);
            suffix = numeral == 1 ? minuteAgo : minutesAgo;
        }
        else if (diff.TotalSeconds >= 1)
        {
            numeral = (int)Math.Floor(diff.TotalSeconds);
            suffix = numeral == 1 ? secondAgo : secondsAgo;
        }
        else
        {
            suffix = justNow;
        }

        string output = numeral == 0 ? suffix : string.Format(CultureInfo.InvariantCulture, "{0} {1}", numeral, suffix);
        return output;
    }
    /// <summary>
    /// Returns the <see cref="DateTime"/> with max time value of the specified date.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/></param>
    /// <returns>The datetime with max time of the <paramref name="dateTime"/></returns>
    public static DateTime ToDateWithMaxTime(this DateTime dateTime)
    {
        return dateTime.Date.AddDays(1).AddMilliseconds(-1);
    }
    /// <summary>
    /// Returns the <see cref="DateTime"/> with min time value of the specified date.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/></param>
    /// <returns>The datetime with min time of the <paramref name="dateTime"/></returns>
    public static DateTime ToDateWithMinTime(this DateTime dateTime)
    {
        return dateTime.Date;
    }
    /// <summary>
    /// Returns the min value of sql server datetime.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/></param>
    /// <returns>1753/01/01 00:00:00 000</returns>
    public static DateTime SqlServerMinValue(this DateTime dateTime)
    {
        return new DateTime(1753, 1, 1, 0, 0, 0, 0);
    }
    /// <summary>
    /// Returns the min value of sql server datetime.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/></param>
    /// <returns>1753/01/01 00:00:00 000</returns>
    public static DateTime SqlServerMinValue(this DateTime? dateTime)
    {
        return new DateTime(1753, 1, 1, 0, 0, 0, 0);
    }
    /// <summary>
    /// Returns the max value of sql server datetime.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/></param>
    /// <returns>9999/12/31 23:59:59 997</returns>
    public static DateTime SqlServerMaxValue(this DateTime dateTime)
    {
        return new DateTime(9999, 12, 31, 23, 59, 59, 997);
    }
    /// <summary>
    /// Returns the max value of sql server datetime.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/></param>
    /// <returns>9999/12/31 23:59:59 997</returns>
    public static DateTime SqlServerMaxValue(this DateTime? dateTime)
    {
        return new DateTime(9999, 12, 31, 23, 59, 59, 997);
    }
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
        else
        {
            return dateTime.SqlServerMinValue();
        }
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
        var age = DateTime.Now.Year - dateTime.Year;
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
    public static bool IsWorkingDay(this DateTime dateTime)
    {
        return dateTime.DayOfWeek != DayOfWeek.Saturday && dateTime.DayOfWeek != DayOfWeek.Sunday;
    }
    /// <summary>
    /// Indicates whether the specified date time is weekend.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/></param>
    /// <returns>true if the value is Saturday or Sunday;otherwise, false.</returns>
    public static bool IsWeekend(this DateTime dateTime)
    {
        return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
    }
    /// <summary>
    /// Determines whether [is null or empty].
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns>
    ///   <c>true</c> if [is null or empty] [the specified date time]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsNullOrEmpty(this DateTime dateTime)
    {
        return dateTime == DateTime.MinValue || dateTime <= DateTimeExtensionConstants.DB_NULL_DATETIME;
    }
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
    /// <returns></returns>
    public static string ToMonthString(this DateTime? dateTime, string emptyContent = "", string partten = DateTimeExtensionConstants.DEFAULT_DATE_FORMAT_PARTTEN)
    {
        if (!dateTime.HasValue)
        {
            return string.Empty;
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
    public static DateTime? ToDateWithMinTime(this DateTime? dateTime)
    {
        return dateTime.HasValue ? dateTime.Value.ToDateWithMinTime() : dateTime;
    }
    /// <summary>
    /// To the date with maximum time.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    public static DateTime? ToDateWithMaxTime(this DateTime? dateTime)
    {
        return dateTime.HasValue ? dateTime.Value.ToDateWithMaxTime() : dateTime;
    }
    /// <summary>
    /// To the date with maximum time.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    public static DateTime? ToSystemDateWithMaxTime(this DateTime? dateTime)
    {
        return dateTime.HasValue ? dateTime.Value.ToSystemDateWithMaxTime() : dateTime;
    }
    /// <summary>
    /// To the date with maximum time.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    public static DateTime ToSystemDateWithMaxTime(this DateTime dateTime)
    {
        return dateTime.AddDays(1).AddSeconds(-1);
    }
    /// <summary>
    /// Returns the first day of month of specified <paramref name="dateTime"/>.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    public static DateTime FirstDayOfMonth(this DateTime dateTime)
    {
        return dateTime.AddDays(1 - dateTime.Day).Date;
    }
    /// <summary>
    /// Returns the last day of the month of specified <paramref name="dateTime"/>.
    /// </summary>
    /// <param name="dateTime">A dateTime</param>
    /// <param name="subtracteMilliseconds">The milliseconds to be subtracted.</param>
    /// <returns>The last day of the month of the <paramref name="dateTime"/></returns>
    public static DateTime LastDayOfMonth(this DateTime dateTime, int subtracteMilliseconds = 1)
    {
        return dateTime.AddDays(1 - dateTime.Day).Date.AddMonths(1).AddMilliseconds(subtracteMilliseconds * -1);
    }
}