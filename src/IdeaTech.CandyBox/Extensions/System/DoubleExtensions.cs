// Copyright (c) IdeaTech. All rights reserved.

namespace System;

/// <summary>
/// Common extensions of <see cref="double"/>.
/// </summary>
public static class DoubleExtensions
{
    /// <summary>
    /// Convert specified double value to a file size string.
    /// <para>Supported:KB MB GB TB PB EB</para>
    /// </summary>
    /// <param name="size">The file size value.</param>
    /// <returns>The file size string. eg:MB,GB...</returns>
    public static string ToFileSizeString(this double size)
    {
        return size.ToDecimal().ToFileSizeString();
    }

    /// <summary>
    /// Convert specified double to an <see cref="decimal"/> value.
    /// </summary>
    /// <param name="value">The double value.</param>
    /// <returns>The converted <see cref="decimal"/> value.</returns>
    public static decimal ToDecimal(this double value)
    {
        return Convert.ToDecimal(value);
    }
}
