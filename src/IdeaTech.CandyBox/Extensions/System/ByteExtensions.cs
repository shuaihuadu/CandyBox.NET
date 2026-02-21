// Copyright (c) IdeaTech. All rights reserved.

namespace System;

/// <summary>
/// Common extensions of <see cref="byte"/>.
/// </summary>
public static class ByteExtensions
{
    /// <summary>
    /// Convert specified byte to an <see cref="Enum"/> value.
    /// </summary>
    /// <typeparam name="T">The type of enum.</typeparam>
    /// <param name="value">The byte value.</param>
    /// <returns>The converted <see cref="Enum"/> value.</returns>
    public static T ToEnum<T>(this byte value) where T : struct, Enum
    {
        return (T)Enum.ToObject(typeof(T), value);
    }
}
