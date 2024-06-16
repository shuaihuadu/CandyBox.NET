namespace System;

/// <summary>
/// The <see cref="bool"/> extensions.
/// </summary>
public static class BooleanExtensions
{
    /// <summary>
    /// To the true or false string.
    /// </summary>
    /// <param name="value">if set to <c>true</c> return <paramref name="trueString"/> ;otherwise <paramref name="falseString"/> .</param>
    /// <param name="trueString">The true string.</param>
    /// <param name="falseString">The false string.</param>
    /// <returns><paramref name="trueString"/> or <paramref name="falseString"/></returns>
    public static string ToTrueOrFalseString(this bool value, string trueString = "Yes", string falseString = "No")
    {
        return value ? trueString : falseString;
    }
}
