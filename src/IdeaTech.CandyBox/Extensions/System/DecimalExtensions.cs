// Copyright (c) IdeaTech. All rights reserved.

namespace System;

/// <summary>
/// Common extensions of <see cref="decimal"/>.
/// </summary>
public static class DecimalExtensions
{
    private const decimal KB = 1024m;
    private const decimal MB = KB * 1024m;
    private const decimal GB = MB * 1024m;
    private const decimal TB = GB * 1024m;
    private const decimal PB = TB * 1024m;
    private const decimal EB = PB * 1024m;

    /// <summary>
    /// Convert specified decimal value to a file size string.
    /// <para>Supported:KB MB GB TB PB EB</para>
    /// </summary>
    /// <param name="size">The file size value.</param>
    /// <returns>The file size string. eg:MB,GB...</returns>
    /// <exception cref="ArgumentOutOfRangeException">When <paramref name="size"/> is negative.</exception>
    public static string ToFileSizeString(this decimal size)
    {
        if (size < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(size), size, "Size must be greater than or equal to zero.");
        }

        if (size < KB) return $"{size:F0} bytes";
        if (size < MB) return $"{size / KB:F0}KB";
        if (size < GB) return $"{size / MB:F0}MB";
        if (size < TB) return $"{size / GB:F0}GB";
        if (size < PB) return $"{size / TB:F0}TB";
        if (size < EB) return $"{size / PB:F0}PB";

        return $"{size / EB:F0}EB";
    }
}
