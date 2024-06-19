// Copyright (c) IdeaTech. All rights reserved.

namespace System;

/// <summary>
/// Common extensions of <see cref="float"/>.
/// </summary>
public static class FloatExtensions
{
    /// <summary>
    /// Convert specified float value to a file size string.
    /// <para>Supported:KB MB GB TB PB EB</para>
    /// </summary>
    /// <param name="size">The file size value.</param>
    /// <returns>The file size string. eg:MB,GB...</returns>
    public static string ToFileSizeString(this float size)
    {
        return size.ToDecimal().ToFileSizeString();
    }

    /// <summary>
    /// Convert specified float to an <see cref="decimal"/> value.
    /// </summary>
    /// <param name="value">The float value.</param>
    /// <returns>The converted <see cref="decimal"/> value.</returns>
    public static decimal ToDecimal(this float value)
    {
        return Convert.ToDecimal(value);
    }
}
