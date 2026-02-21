// Copyright (c) IdeaTech. All rights reserved.

namespace IdeaTech.CandyBox;

/// <summary>
/// The verify helper class.
/// </summary>
public static class Verify
{
    /// <summary>
    /// Force the <paramref name="obj"/> not null.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <param name="paramName">Name of the parameter.</param>
    /// <exception cref="System.ArgumentNullException"></exception>
    public static void NotNull([NotNull] object? obj, [CallerArgumentExpression(nameof(obj))] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(obj, paramName);
    }

    /// <summary>
    /// Force the <paramref name="value"/> not null or white space.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="paramName">Name of the parameter.</param>
    public static void NotNullOrWhiteSpace([NotNull] string? value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, paramName);
    }

    /// <summary>
    /// Force the <paramref name="collection"/> not null or empty.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection">The collection.</param>
    /// <param name="paramName">Name of the parameter.</param>
    /// <exception cref="System.ArgumentException">The value cannot be empty.</exception>
    public static void NotNullOrEmpty<T>(IEnumerable<T>? collection, [CallerArgumentExpression(nameof(collection))] string? paramName = null)
    {
        NotNull(collection, paramName);

        if (!collection.Any())
        {
            throw new ArgumentException("The value cannot be empty.", paramName);
        }
    }

    /// <summary>
    /// Force the specified <paramref name="value"/> between the specified value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">The value.</param>
    /// <param name="lower">The lower.</param>
    /// <param name="upper">The upper.</param>
    /// <param name="paramName">Name of the parameter.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">The '{paramName}' must between {lower} and {upper}.</exception>
    public static void Between<T>(T value, T lower, T upper, [CallerArgumentExpression(nameof(value))] string? paramName = null)
         where T : IComparable<T>
    {
        if (!value.Between(lower, upper))
        {
            throw new ArgumentOutOfRangeException(paramName, $"The '{paramName}' must between {lower} and {upper}.");
        }
    }

    /// <summary>
    /// Force the specified <paramref name="condition"/> is true.
    /// </summary>
    /// <param name="condition">The condition.</param>
    /// <param name="message">The message.</param>
    /// <param name="paramName">Name of the parameter.</param>
    /// <exception cref="System.ArgumentException"></exception>
    public static void True(bool condition, string message, [CallerArgumentExpression(nameof(condition))] string? paramName = null)
    {
        if (!condition)
        {
            throw new ArgumentException(message, paramName);
        }
    }

    /// <summary>
    /// Force the specified <paramref name="condition"/> is false.
    /// </summary>
    /// <param name="condition">The condition.</param>
    /// <param name="message">The message.</param>
    /// <param name="paramName">Name of the parameter.</param>
    public static void False(bool condition, string message, [CallerArgumentExpression(nameof(condition))] string? paramName = null)
    {
        True(!condition, message, paramName);
    }

    /// <summary>
    /// Force the <paramref name="url"/> is a valid url.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="allowQueryString">if set to <c>true</c> check query string.</param>
    /// <param name="paramName">Name of the parameter.</param>
    /// <exception cref="System.ArgumentException">
    /// The `{url}` is not valid.
    /// or
    /// The `{url}` is not valid: it cannot contain query parameters.
    /// or
    /// The `{url}` is not valid: it cannot contain URL fragments.
    /// </exception>
    public static void ValidUrl(string url, bool allowQueryString = false, [CallerArgumentExpression(nameof(url))] string? paramName = null)
    {
        NotNullOrWhiteSpace(url, paramName);

        bool isValidUrl = url.IsUrl(allowQueryString);

        True(isValidUrl, $"The `{url}` is not a valid URL.");
    }
}
