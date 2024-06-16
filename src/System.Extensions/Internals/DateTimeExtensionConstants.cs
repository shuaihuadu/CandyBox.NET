namespace System.Extensions.Internals;

internal class DateTimeExtensionConstants
{
    /// <summary>
    /// The datetime format partten.
    /// </summary>
    public const string DEFAULT_DATETIME_FORMAT_PARTTEN = "yyyy-MM-dd HH:mm:ss";
    /// <summary>
    /// The datet format partten
    /// </summary>
    public const string DEFAULT_DATE_FORMAT_PARTTEN = "yyyy-MM-dd";
    /// <summary>
    /// The date month format partten
    /// </summary>
    public const string DEFAULT_DATE_MONTH_FORMAT_PARTTEN = "yyyy-MM";
    /// <summary>
    /// The database null datetime
    /// </summary>
    public const string DB_NULL_DATETIME_STRING = "1900-01-01 00:00:00.000";
    /// <summary>
    /// The database null datetime
    /// </summary>
    public static readonly DateTime DB_NULL_DATETIME = new DateTime(1900, 1, 1, 0, 0, 0, 0);
}
