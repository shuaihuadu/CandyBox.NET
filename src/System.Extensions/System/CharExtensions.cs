﻿namespace System;

/// <summary>
/// Common extensions of <see cref="char"/>.
/// </summary>
public static class CharExtensions
{
    /// <summary>
    /// Indicates whether the specified <see cref="char"/> is a digit.
    /// </summary>
    /// <param name="c">The character for test.</param>
    /// <returns>true if the character is a digit;otherwise, false.</returns>
    public static bool IsDigit(this char c) => char.IsDigit(c);
    /// <summary>
    /// Indicates whether the specified <see cref="char"/> is a  lower case ASCII letter.
    /// </summary>
    /// <param name="c">The character for test.</param>
    /// <returns>true if the character is a lower case ASCII letter;otherwise, false.</returns>
    public static bool IsLower(this char c) => char.IsLower(c);
    /// <summary>
    /// Indicates whether the specified <see cref="char"/> is an upper case ASCII letter.
    /// </summary>
    /// <param name="c">The character for test.</param>
    /// <returns>true if the character is an upper case ASCII letter;otherwise, false.</returns>
    public static bool IsUpper(this char c) => char.IsUpper(c);
    /// <summary>
    /// Indicates whether the specified character is an ASCII letter or digit.
    /// </summary>
    /// <param name="c">The character for test.</param>
    /// <returns>true if the character is an ASCII letter or digit;otherwise, false.</returns>
    public static bool IsLetterOrDigit(this char c) => char.IsLetterOrDigit(c);
}