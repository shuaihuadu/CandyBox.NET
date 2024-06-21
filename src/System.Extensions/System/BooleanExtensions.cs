// Copyright (c) IdeaTech. All rights reserved.

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

    /// <summary>
    /// Determines if the <paramref name="value"/> is true then throws the <see cref="ArgumentException"/>.
    /// </summary>
    /// <param name="value">The bool value.</param>
    /// <param name="parameterName">Exception to throw.</param>
    /// <param name="message">The exception message.</param>
    /// <returns>The object.</returns>
    public static void ThrowArgumentExceptionIfTrue([ValidatedNotNullAttribute] this bool value, string parameterName, string? message = null)
    {
        ThrowIfTrue(value, new ArgumentException(parameterName));
    }

    /// <summary>
    /// Determines if the <paramref name="value"/> is true then throws the <see cref="ArgumentNullException"/>.
    /// </summary>
    /// <param name="value">The bool value.</param>
    /// <param name="parameterName">Exception to throw</param>
    /// <param name="message">The exception message.</param>
    /// <returns>The object.</returns>
    public static void ThrowArgumentNullExceptionIfTrue([ValidatedNotNullAttribute] this bool value, string parameterName, string? message = null)
    {
        ThrowIfTrue(value, new ArgumentNullException(parameterName));
    }

    /// <summary>
    /// Determines if the <paramref name="value"/> is false then throws the <see cref="ArgumentException"/>.
    /// </summary>
    /// <param name="value">The bool value.</param>
    /// <param name="parameterName">Exception to throw</param>
    /// <param name="message">The exception message.</param>
    /// <returns>The object.</returns>
    public static void ThrowArgumentExceptionIfFalse([ValidatedNotNullAttribute] this bool value, string parameterName, string? message = null)
    {
        ThrowArgumentExceptionIfTrue(!value, parameterName);
    }

    /// <summary>
    /// Determines if the <paramref name="value"/> is false then throws the <see cref="ArgumentNullException"/>.
    /// </summary>
    /// <param name="value">The bool value.</param>
    /// <param name="parameterName">Exception to throw</param>
    /// <param name="message">The exception message.</param>
    /// <returns>The object.</returns>
    public static void ThrowArgumentNullExceptionIfFalse([ValidatedNotNullAttribute] this bool value, string parameterName, string? message = null)
    {
        ThrowArgumentNullExceptionIfTrue(!value, parameterName);
    }

    /// <summary>
    /// Determines if the <paramref name="value"/> is true then throws the <paramref name="exception"/>.
    /// </summary>
    /// <param name="value">The bool value.</param>
    /// <param name="exception">Exception to throw</param>
    /// <returns>The object.</returns>
    public static void ThrowIfTrue([ValidatedNotNullAttribute] this bool value, Exception exception)
    {
        if (value)
        {
            throw exception;
        }
    }

    /// <summary>
    /// Determines if the <paramref name="value"/> is false and throws the <paramref name="exception"/>.
    /// </summary>
    /// <param name="value">The bool value.</param>
    /// <param name="exception">Exception to throw</param>
    /// <returns>The object.</returns>
    public static void ThrowIfFlase([ValidatedNotNullAttribute] this bool value, Exception exception)
    {
        ThrowIfTrue(!value, exception);
    }
}
