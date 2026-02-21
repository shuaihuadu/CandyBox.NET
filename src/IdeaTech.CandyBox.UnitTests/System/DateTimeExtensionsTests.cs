// Copyright (c) IdeaTech. All rights reserved.

namespace System.Extensions.UnitTests.System;

[TestClass]
public class DateTimeExtensionsTests
{
    private static readonly DateTime TestDate = new(2024, 6, 15, 10, 30, 45, DateTimeKind.Utc);

    #region ToRelativeTime

    [TestMethod]
    public void ToRelativeTimeWithJustNowShouldReturnJustNow()
    {
        DateTime recent = DateTime.Now.AddSeconds(-10);
        Assert.AreEqual("Just now", recent.ToRelativeTime("{0} {1} ago"));
    }

    [TestMethod]
    public void ToRelativeTimeWithMinutesShouldReturnMinutesAgo()
    {
        DateTime past = DateTime.Now.AddMinutes(-5);
        string result = past.ToRelativeTime("{0} {1} ago");
        StringAssert.Contains(result, "minute");
    }

    [TestMethod]
    public void ToRelativeTimeWithHoursShouldReturnHoursAgo()
    {
        DateTime past = DateTime.Now.AddHours(-3);
        string result = past.ToRelativeTime("{0} {1} ago");
        StringAssert.Contains(result, "hour");
    }

    [TestMethod]
    public void ToRelativeTimeWithDaysShouldReturnDaysAgo()
    {
        DateTime past = DateTime.Now.AddDays(-3);
        string result = past.ToRelativeTime("{0} {1} ago");
        StringAssert.Contains(result, "day");
    }

    [TestMethod]
    public void ToRelativeTimeWithWeeksShouldReturnWeeksAgo()
    {
        DateTime past = DateTime.Now.AddDays(-14);
        string result = past.ToRelativeTime("{0} {1} ago");
        StringAssert.Contains(result, "week");
    }

    [TestMethod]
    public void ToRelativeTimeWithMonthsShouldReturnMonthsAgo()
    {
        DateTime past = DateTime.Now.AddDays(-60);
        string result = past.ToRelativeTime("{0} {1} ago");
        StringAssert.Contains(result, "month");
    }

    [TestMethod]
    public void ToRelativeTimeWithYearsShouldReturnYearsAgo()
    {
        DateTime past = DateTime.Now.AddYears(-2);
        string result = past.ToRelativeTime("{0} {1} ago");
        StringAssert.Contains(result, "year");
    }

    #endregion

    #region ToDateWithMinTime / ToDateWithMaxTime

    [TestMethod]
    public void ToDateWithMinTimeShouldReturnDateAtMidnight()
    {
        DateTime result = TestDate.ToDateWithMinTime();
        Assert.AreEqual(TestDate.Date, result);
        Assert.AreEqual(TimeSpan.Zero, result.TimeOfDay);
    }

    [TestMethod]
    public void ToDateWithMinTimeNullableShouldReturnDateAtMidnight()
    {
        DateTime? nullable = TestDate;
        DateTime? result = nullable.ToDateWithMinTime();
        Assert.IsNotNull(result);
        Assert.AreEqual(TestDate.Date, result!.Value);
    }

    [TestMethod]
    public void ToDateWithMinTimeNullableShouldReturnNullWhenNull()
    {
        DateTime? nullable = null;
        Assert.IsNull(nullable.ToDateWithMinTime());
    }

    [TestMethod]
    public void ToDateWithMaxTimeShouldReturnEndOfDay()
    {
        DateTime result = TestDate.ToDateWithMaxTime();
        Assert.AreEqual(TestDate.Date, result.Date);
        Assert.AreEqual(23, result.Hour);
        Assert.AreEqual(59, result.Minute);
        Assert.AreEqual(59, result.Second);
    }

    [TestMethod]
    public void ToDateWithMaxTimeNullableShouldReturnNullWhenNull()
    {
        DateTime? nullable = null;
        Assert.IsNull(nullable.ToDateWithMaxTime());
    }

    #endregion

    #region SqlServerMinValue / SqlServerMaxValue

    [TestMethod]
    public void SqlServerMinValueShouldReturn1753()
    {
        DateTime result = TestDate.SqlServerMinValue();
        Assert.AreEqual(1753, result.Year);
        Assert.AreEqual(1, result.Month);
        Assert.AreEqual(1, result.Day);
    }

    [TestMethod]
    public void SqlServerMaxValueShouldReturn9999()
    {
        DateTime result = TestDate.SqlServerMaxValue();
        Assert.AreEqual(9999, result.Year);
        Assert.AreEqual(12, result.Month);
        Assert.AreEqual(31, result.Day);
    }

    #endregion

    #region ToSqlServerSafeDateTime

    [TestMethod]
    public void ToSqlServerSafeDateTimeWithNormalDateShouldReturnSame()
    {
        DateTime result = TestDate.ToSqlServerSafeDateTime();
        Assert.AreEqual(TestDate, result);
    }

    [TestMethod]
    public void ToSqlServerSafeDateTimeWithTooOldDateShouldReturnMinValue()
    {
        DateTime tooOld = new(1000, 1, 1);
        DateTime result = tooOld.ToSqlServerSafeDateTime();
        Assert.AreEqual(1753, result.Year);
    }

    [TestMethod]
    public void ToSqlServerSafeDateTimeNullableShouldReturnMinValueWhenNull()
    {
        DateTime? nullable = null;
        DateTime result = nullable.ToSqlServerSafeDateTime();
        Assert.AreEqual(1753, result.Year);
    }

    #endregion

    #region Age

    [TestMethod]
    public void AgeWithPastDateShouldReturnCorrectAge()
    {
        DateTime birthday = DateTime.Now.AddYears(-30);
        Assert.AreEqual(30, birthday.Age());
    }

    [TestMethod]
    public void AgeWithFutureDateShouldReturnZero()
    {
        DateTime future = DateTime.Now.AddYears(5);
        Assert.AreEqual(0, future.Age());
    }

    #endregion

    #region IsWorkingDay / IsWeekend

    [DataTestMethod]
    [DataRow(DayOfWeek.Monday)]
    [DataRow(DayOfWeek.Tuesday)]
    [DataRow(DayOfWeek.Wednesday)]
    [DataRow(DayOfWeek.Thursday)]
    [DataRow(DayOfWeek.Friday)]
    public void IsWorkingDayWithWeekdayShouldReturnTrue(DayOfWeek day)
    {
        DateTime date = GetNextDayOfWeek(day);
        Assert.IsTrue(date.IsWorkingDay());
    }

    [DataTestMethod]
    [DataRow(DayOfWeek.Saturday)]
    [DataRow(DayOfWeek.Sunday)]
    public void IsWorkingDayWithWeekendShouldReturnFalse(DayOfWeek day)
    {
        DateTime date = GetNextDayOfWeek(day);
        Assert.IsFalse(date.IsWorkingDay());
    }

    [DataTestMethod]
    [DataRow(DayOfWeek.Saturday)]
    [DataRow(DayOfWeek.Sunday)]
    public void IsWeekendWithWeekendShouldReturnTrue(DayOfWeek day)
    {
        DateTime date = GetNextDayOfWeek(day);
        Assert.IsTrue(date.IsWeekend());
    }

    [DataTestMethod]
    [DataRow(DayOfWeek.Monday)]
    [DataRow(DayOfWeek.Friday)]
    public void IsWeekendWithWeekdayShouldReturnFalse(DayOfWeek day)
    {
        DateTime date = GetNextDayOfWeek(day);
        Assert.IsFalse(date.IsWeekend());
    }

    #endregion

    #region IsNullOrEmpty

    [TestMethod]
    public void IsNullOrEmptyWithMinValueShouldReturnTrue()
    {
        Assert.IsTrue(DateTime.MinValue.IsNullOrEmpty());
    }

    [TestMethod]
    public void IsNullOrEmptyWithDbNullDateShouldReturnTrue()
    {
        Assert.IsTrue(new DateTime(1900, 1, 1).IsNullOrEmpty());
    }

    [TestMethod]
    public void IsNullOrEmptyWithNormalDateShouldReturnFalse()
    {
        Assert.IsFalse(TestDate.IsNullOrEmpty());
    }

    #endregion

    #region ToDateParttenString

    [TestMethod]
    public void ToDateParttenStringWithNullOrEmptyDateShouldReturnEmpty()
    {
        Assert.AreEqual(string.Empty, DateTime.MinValue.ToDateParttenString());
    }

    [TestMethod]
    public void ToDateParttenStringWithValidDateShouldReturnFormattedString()
    {
        Assert.AreEqual("2024-06-15", TestDate.ToDateParttenString());
    }

    [TestMethod]
    public void ToDateParttenStringNullableWithNullShouldReturnEmptyContent()
    {
        DateTime? nullable = null;
        Assert.AreEqual("N/A", nullable.ToDateParttenString("N/A"));
    }

    #endregion

    #region ToDateTimeString

    [TestMethod]
    public void ToDateTimeStringWithNullOrEmptyDateShouldReturnEmpty()
    {
        Assert.AreEqual(string.Empty, DateTime.MinValue.ToDateTimeString());
    }

    [TestMethod]
    public void ToDateTimeStringWithValidDateShouldReturnFormattedString()
    {
        Assert.AreEqual("2024-06-15 10:30:45", TestDate.ToDateTimeString());
    }

    [TestMethod]
    public void ToDateTimeStringNullableWithNullShouldReturnEmptyContent()
    {
        DateTime? nullable = null;
        Assert.AreEqual("--", nullable.ToDateTimeString("--"));
    }

    #endregion

    #region ToMonthString

    [TestMethod]
    public void ToMonthStringWithNullOrEmptyDateShouldReturnEmpty()
    {
        Assert.AreEqual(string.Empty, DateTime.MinValue.ToMonthString());
    }

    [TestMethod]
    public void ToMonthStringWithValidDateShouldReturnFormattedString()
    {
        Assert.AreEqual("2024-06", TestDate.ToMonthString());
    }

    [TestMethod]
    public void ToMonthStringNullableWithNullShouldReturnEmptyContent()
    {
        DateTime? nullable = null;
        Assert.AreEqual(string.Empty, nullable.ToMonthString());
    }

    #endregion

    #region ToSystemDateWithMaxTime / FirstDayOfMonth / LastDayOfMonth

    [TestMethod]
    public void ToSystemDateWithMaxTimeShouldReturnEndOfDay()
    {
        DateTime result = TestDate.ToSystemDateWithMaxTime();
        Assert.AreEqual(TestDate.AddDays(1).AddSeconds(-1), result);
    }

    [TestMethod]
    public void FirstDayOfMonthShouldReturnFirstDay()
    {
        DateTime result = TestDate.FirstDayOfMonth();
        Assert.AreEqual(1, result.Day);
        Assert.AreEqual(TestDate.Month, result.Month);
        Assert.AreEqual(TestDate.Year, result.Year);
    }

    [TestMethod]
    public void LastDayOfMonthShouldReturnLastDay()
    {
        DateTime result = TestDate.LastDayOfMonth();
        Assert.AreEqual(30, result.Day);
        Assert.AreEqual(TestDate.Month, result.Month);
    }

    #endregion

    private static DateTime GetNextDayOfWeek(DayOfWeek day)
    {
        DateTime now = DateTime.Now;
        int daysUntil = ((int)day - (int)now.DayOfWeek + 7) % 7;
        return now.AddDays(daysUntil == 0 ? 0 : daysUntil);
    }
}
