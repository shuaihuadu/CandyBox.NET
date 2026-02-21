// Copyright (c) IdeaTech. All rights reserved.

namespace System;

/// <summary>
/// Common extensions of <see cref="string"/>.
/// </summary>
public static partial class StringExtensions
{
    /// <summary>
    /// The <see cref="string.Trim()"/> method only trims 0x0009, 0x000a, 0x000b, 0x000c, 0x000d, 0x0085, 0x2028, and 0x2029.
    /// This array adds in control characters.
    /// </summary>
    internal static readonly char[] invisiableCharacters =
    [
        (char)0x00, (char)0x01, (char)0x02, (char)0x03, (char)0x04, (char)0x05,
        (char)0x06, (char)0x07, (char)0x08, (char)0x09, (char)0x0a, (char)0x0b,
        (char)0x0c, (char)0x0d, (char)0x0e, (char)0x0f, (char)0x10, (char)0x11,
        (char)0x12, (char)0x13, (char)0x14, (char)0x15, (char)0x16, (char)0x17,
        (char)0x18, (char)0x19, (char)0x20, (char)0x1a, (char)0x1b, (char)0x1c,
        (char)0x1d, (char)0x1e, (char)0x1f, (char)0x7f, (char)0x85, (char)0x2028, (char)0x2029
    ];

    /// <summary>
    /// A <see cref="HashSet{T}"/> of <see cref="invisiableCharacters"/> for O(1) lookup performance.
    /// </summary>
    private static readonly HashSet<char> invisiableCharactersSet = [.. invisiableCharacters];

    /// <summary>
    /// All valid GUID format specifiers accepted by <see cref="Guid.TryParseExact"/>.
    /// </summary>
    private static readonly string[] validGuidFormats = ["D", "d", "N", "n", "P", "p", "B", "b", "X", "x"];

    /// <summary>
    /// A <see cref="HashSet{T}"/> of <see cref="validGuidFormats"/> for O(1) lookup performance.
    /// </summary>
    private static readonly HashSet<string> validGuidFormatsSet = new(validGuidFormats, StringComparer.Ordinal);

    /// <summary>
    /// A <see cref="HashSet{T}"/> of characters that are invalid in file names, for O(1) lookup performance.
    /// </summary>
    private static readonly HashSet<char> invalidFileNameCharsSet = new(Path.GetInvalidFileNameChars());

    /// <summary>
    /// A <see cref="HashSet{T}"/> of characters that are invalid in file paths, for O(1) lookup performance.
    /// </summary>
    private static readonly HashSet<char> invalidPathCharsSet = new(Path.GetInvalidPathChars());

    /// <summary>
    /// Compiled regular expression for matching Chinese mobile phone numbers (all current 13x-19x prefixes).
    /// </summary>
    [GeneratedRegex(@"^1[3-9]\d{9}$")]
    private static partial Regex ChineseMobileRegex();

    /// <summary>
    /// Compiled regular expression for matching Chinese characters (CJK Unified Ideographs U+4E00-U+9FA5).
    /// </summary>
    [GeneratedRegex(@"[\u4e00-\u9fa5]")]
    private static partial Regex HansRegex();

    /// <summary>
    /// Compiled regular expression for matching strings that contain only ASCII letters.
    /// </summary>
    [GeneratedRegex(@"^[a-zA-Z]+$")]
    private static partial Regex LetterRegex();

    #region IsXXXXXX

    /// <summary>
    /// Indicates whether the specified string is a correct email address.
    /// </summary>
    /// <remarks>
    /// Uses <see cref="System.Net.Mail.MailAddress"/> for validation, which complies with RFC 5321
    /// and supports modern TLDs of any length (e.g., .photography, .museum).
    /// </remarks>
    /// <param name="value">The string.</param>
    /// <returns>true if the value parameter is a correct email address; otherwise, false.</returns>
    public static bool IsEmail(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        try
        {
            System.Net.Mail.MailAddress addr = new(value!);

            return addr.Address == value!.Trim();
        }
        catch (FormatException)
        {
            return false;
        }
    }

    /// <summary>
    /// Indicates whether the specified string is a correct chinese mobile phone number.
    /// </summary>
    /// <remarks>
    /// Matches all current Chinese mobile number prefixes (13x-19x) as assigned by MIIT.
    /// Uses pattern <c>^1[3-9]\d{9}$</c> to remain compatible with future prefix assignments.
    /// </remarks>
    /// <param name="value">The string.</param>
    /// <returns>true if the value parameter is a correct chinese mobile phone number; otherwise, false.</returns>
    public static bool IsChineseMobile(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return ChineseMobileRegex().IsMatch(value!);
    }

    /// <summary>
    /// Determines whether the specified check query is URL.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="allowQuery">if set to <c>true</c> check the query string.</param>
    /// <returns>
    ///   <c>true</c> if the specified check query is URL; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsUrl(this string? value, bool allowQuery = false)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        if (!Uri.TryCreate(value, UriKind.Absolute, out Uri? uri) || string.IsNullOrEmpty(uri.Host))
        {
            return false;
        }

        if (!allowQuery && !string.IsNullOrEmpty(uri.Query))
        {
            return false;
        }

        if (!string.IsNullOrEmpty(uri.Fragment))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Indicates whether the specified string contains at least one Chinese character (CJK Unified Ideographs U+4E00-U+9FA5).
    /// </summary>
    /// <param name="value">The string.</param>
    /// <returns>true if the value parameter contains at least one Chinese character; otherwise, false.</returns>
    public static bool IsHans(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return HansRegex().IsMatch(value!);
    }

    /// <summary>
    /// Indicates whether the specified string is between <paramref name="minLength"/> and <paramref name="maxLength"/>.
    /// </summary>
    /// <param name="value">The string.</param>
    /// <param name="minLength">The min length for validate.</param>
    /// <param name="maxLength">The max length for validate.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="value"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">When <paramref name="minLength"/> is less than 0 or <paramref name="maxLength"/> is less than <paramref name="minLength"/>.</exception>
    /// <returns>true if the value is valid length; otherwise, false.</returns>
    public static bool IsValidLength(this string? value, int minLength, int maxLength)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (minLength < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(minLength), minLength, "The minLength must be greater than or equal to 0.");
        }

        if (maxLength < minLength)
        {
            throw new ArgumentOutOfRangeException(nameof(maxLength), maxLength, "The maxLength must be greater than or equal to minLength.");
        }

        return value.Length >= minLength && value.Length <= maxLength;
    }

    /// <summary>
    /// Indicates whether the specified string bytes is between <paramref name="min"/> and <paramref name="max"/>.
    /// </summary>
    /// <param name="value">The string.</param>
    /// <param name="min">The min bytes count.</param>
    /// <param name="max">The max bytes count.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="value"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">When <paramref name="min"/> is less than 0 or <paramref name="max"/> is less than <paramref name="min"/>.</exception>
    /// <returns>true if the value is valid byte count; otherwise, false.</returns>
    public static bool IsValidByteCount(this string? value, int min, int max)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (min < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(min), min, "The min must be greater than or equal to 0.");
        }

        if (max < min)
        {
            throw new ArgumentOutOfRangeException(nameof(max), max, "The max must be greater than or equal to min.");
        }

        int count = Text.Encoding.Default.GetByteCount(value);

        return count >= min && count <= max;
    }

    /// <summary>
    /// Indicates whether the specified string is a correct <see cref="Guid"/>.
    /// </summary>
    /// <param name="value">The string.</param>
    /// <param name="format">GUID format type.</param>
    /// <returns>true if the value parameter is a correct <see cref="Guid"/>; otherwise, false.</returns>
    public static bool IsGuid(this string? value, string format = "D")
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        if (!validGuidFormatsSet.Contains(format))
        {
            format = validGuidFormats[0];
        }

        return Guid.TryParseExact(value!, format, out _);
    }

    /// <summary>
    /// Determines whether the <paramref name="value"/> is a valid culture identifier.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    ///   <c>true</c> if the <paramref name="value"/> is valid culture identifier; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsValidCultureIdentifier(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        try
        {
            CultureInfo.GetCultureInfo(value!);

            return true;
        }
        catch (CultureNotFoundException)
        {
            return false;
        }
    }

    /// <summary>
    /// Determines whether this instance is byte.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="style">The number style.</param>
    /// <returns>
    ///   <c>true</c> if the specified value is byte; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsByte(this string? value, NumberStyles style = NumberStyles.None)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return byte.TryParse(value!, style, provider: null, out _);
    }

    /// <summary>
    /// Determines whether this instance is short.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="style">The number style.</param>
    /// <returns>
    ///   <c>true</c> if the specified value is short; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsShort(this string? value, NumberStyles style = NumberStyles.None)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return short.TryParse(value!, style, provider: null, out _);
    }

    /// <summary>
    /// Determines whether this instance is int32.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="style">The number style.</param>
    /// <returns>
    ///   <c>true</c> if the specified value is int32; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsInt32(this string? value, NumberStyles style = NumberStyles.None)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return int.TryParse(value!, style, provider: null, out _);
    }

    /// <summary>
    /// Determines whether this instance is int64.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="style">The number style.</param>
    /// <returns>
    ///   <c>true</c> if the specified value is int64; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsInt64(this string? value, NumberStyles style = NumberStyles.None)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return long.TryParse(value!, style, provider: null, out _);
    }

    /// <summary>
    /// Determines whether this instance is decimal.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="style">The number style.</param>
    /// <returns>
    ///   <c>true</c> if the specified value is decimal; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsDecimal(this string? value, NumberStyles style = NumberStyles.None)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return decimal.TryParse(value!, style, provider: null, out _);
    }

    /// <summary>
    /// Determines whether this instance is float.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="style">The number style.</param>
    /// <returns>
    ///   <c>true</c> if the specified value is float; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsFloat(this string? value, NumberStyles style = NumberStyles.None)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return float.TryParse(value!, style, provider: null, out _);
    }

    /// <summary>
    /// Determines whether the <paramref name="value"/> is a valid date time string.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="value"/>.</param>
    /// <param name="styles">A bitwise combination of the enumeration values that indicates the style elements that can be present in s for the parse operation to succeed, and that defines how to interpret the parsed date in relation to the current time zone or the current date. A typical value to specify is System.Globalization.DateTimeStyles.None.</param>
    /// <returns>
    ///   <c>true</c> if <paramref name="value"/> is date time; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsDateTime(this string? value, IFormatProvider? provider = null, DateTimeStyles styles = DateTimeStyles.None)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return DateTime.TryParse(value!, provider ?? CultureInfo.InvariantCulture, styles, out _);
    }

    /// <summary>
    /// Determines whether this instance is letter.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    ///   <c>true</c> if the specified value is letter; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsLetter(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return LetterRegex().IsMatch(value!);
    }

    /// <summary>
    /// Determines whether a given string is a valid Base64-encoded string.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is Base64-encoded; otherwise, false.</returns>
    public static bool IsBase64String(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        // Valid Base64-encoded strings must have a length that is a multiple of 4.
        if (value!.Length % 4 != 0)
        {
            return false;
        }

        try
        {
            Convert.FromBase64String(value!);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    #endregion

    #region ToXXXXXX

    /// <summary>
    /// Removes all invisible characters from the current System.String object.
    /// </summary>
    /// <param name="value">The string.</param>
    /// <returns>
    /// The string that remains after all invisible characters are removed from every position in the
    /// current string. If no characters can be trimmed from the current instance, the method returns
    /// the current instance unchanged.
    ///</returns>
    public static string TrimAll(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return new string(value!.Where(c => !invisiableCharactersSet.Contains(c)).ToArray());
    }

    /// <summary>
    /// Removes all leading and trailing invisible characters from the current System.String object.
    /// </summary>
    /// <param name="value">The string.</param>
    /// <returns>
    /// The string that remains after all invisible  characters are removed from the
    /// start and end of the current string. If no characters can be trimmed from the
    /// current instance, the method returns the current instance unchanged.
    ///</returns>
    public static string TrimBlank(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return value!.Trim(invisiableCharacters);
    }

    /// <summary>
    /// Reverse the specified string.
    /// </summary>
    /// <param name="value">The string.</param>
    /// <returns>The reversed string</returns>
    public static string Reverse(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return string.Create(value!.Length, value!, static (span, source) =>
        {
            source.AsSpan().CopyTo(span);
            span.Reverse();
        });
    }

    /// <summary>
    /// Truncate the specified string to the specified length.
    /// </summary>
    /// <param name="value">The string.</param>
    /// <param name="length">The length of remained.</param>
    /// <param name="cutOffReplacement">The replacement of the removed part of the <see cref="string"/>.</param>
    /// <exception cref="ArgumentOutOfRangeException">When <paramref name="length"/> is negative.</exception>
    /// <returns>The string truncated.</returns>
    public static string? Truncate(this string? value, int length, string cutOffReplacement = " ...")
    {
        ArgumentOutOfRangeException.ThrowIfNegative(length);

        if (string.IsNullOrWhiteSpace(value) || value!.Length <= length)
        {
            return value;
        }

        return $"{value[..length]}{cutOffReplacement}";
    }

    /// <summary>
    /// Convert specified string to a <see cref="byte"/> value.
    /// </summary>
    /// <param name="value">The string.</param>
    /// <returns>The converted <see cref="byte"/> value.</returns>
    /// <exception cref="ArgumentException">When convert failed.</exception>
    public static byte ToByte(this string? value)
    {
        if (!string.IsNullOrWhiteSpace(value) && byte.TryParse(value, out byte result))
        {
            return result;
        }

        throw new ArgumentException("The specified string is not a byte value.", nameof(value));
    }

    /// <summary>
    /// Convert specified string to a <see cref="short"/> value.
    /// </summary>
    /// <param name="value">The string.</param>
    /// <returns>The converted <see cref="short"/> value.</returns>
    /// <exception cref="ArgumentException">When convert failed.</exception>
    public static short ToInt16(this string? value)
    {
        if (!string.IsNullOrWhiteSpace(value) && short.TryParse(value, out short result))
        {
            return result;
        }

        throw new ArgumentException("The specified string is not a short value.", nameof(value));
    }

    /// <summary>
    /// Convert specified string to an <see cref="int"/> value.
    /// </summary>
    /// <param name="value">The string.</param>
    /// <returns>The converted <see cref="int"/> value.</returns>
    /// <exception cref="ArgumentException">When convert failed.</exception>
    public static int ToInt32(this string? value)
    {
        if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, out int result))
        {
            return result;
        }

        throw new ArgumentException("The specified string is not an int value.", nameof(value));
    }

    /// <summary>
    /// Convert specified string to a <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The string.</param>
    /// <returns>The converted <see cref="long"/> value.</returns>
    /// <exception cref="ArgumentException">When convert failed.</exception>
    public static long ToInt64(this string? value)
    {
        if (!string.IsNullOrWhiteSpace(value) && long.TryParse(value, out long result))
        {
            return result;
        }

        throw new ArgumentException("The specified string is not a long value.", nameof(value));
    }

    /// <summary>
    /// Convert specified string to an <see cref="Enum"/> value.
    /// </summary>
    /// <typeparam name="T">The type of enum.</typeparam>
    /// <param name="value">The string.</param>
    /// <param name="ignoreCase">true to ignore case during the comparison; otherwise, false.</param>
    /// <returns>The converted <see cref="Enum"/> value.</returns>
    /// <exception cref="ArgumentException">When convert failed.</exception>
    public static T ToEnum<T>(this string? value, bool ignoreCase = true) where T : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value), "Must specify valid information for parsing in the string.");
        }

        if (Enum.TryParse<T>(value!, ignoreCase, out T result))
        {
            return result;
        }

        throw new ArgumentException($"The value '{value}' is not a valid member of enum type {typeof(T).Name}.", nameof(value));
    }

    /// <summary>
    /// Convert specified string to a <see cref="Guid"/> value.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <param name="format">GUID format type.</param>
    /// <returns>The converted <see cref="Guid"/> value.</returns>
    /// <exception cref="ArgumentException">When convert failed.</exception>
    public static Guid? ToGuid(this string? value, string format = "D")
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (!validGuidFormatsSet.Contains(format))
        {
            format = validGuidFormats[0];
        }

        if (Guid.TryParseExact(value!, format, out Guid result))
        {
            return result;
        }

        throw new ArgumentException("Input string is not a valid GUID format.", nameof(value));
    }

    /// <summary>
    /// To the date time with partten.
    /// </summary>
    /// <param name="dateTime">The date time string.</param>
    /// <param name="pattern">The pattern.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="formatProvider">An object that supplies culture-specific format information abouts.</param>
    /// <returns>The converted <see cref="DateTime"/> value.</returns>
    /// <exception cref="ArgumentException">When convert faild.</exception>
    public static DateTime? ToDateTimeWithPartten(this string? dateTime, string pattern = DateTimeExtensionConstants.DEFAULT_DATE_FORMAT_PARTTEN, DateTime? defaultValue = null, IFormatProvider? formatProvider = null)
    {
        if (string.IsNullOrWhiteSpace(dateTime))
        {
            return defaultValue;
        }

        if (DateTime.TryParseExact(dateTime!, pattern, formatProvider ?? CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
        {
            return result;
        }

        return defaultValue;
    }

    /// <summary>
    /// Converts the <paramref name="value"/> to a safe file name.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="replacement">The replacement character for invalid file name characters.</param>
    /// <returns>A safe file name string, or <see langword="null"/> if <paramref name="value"/> is null or whitespace.</returns>
    public static string? ToSafeFileName(this string? value, char replacement = '_')
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (invalidFileNameCharsSet.Contains(replacement))
        {
            throw new ArgumentException("Invalid replacement.", nameof(replacement));
        }

        return string.Create(value!.Length, (Source: value!, Replacement: replacement), static (span, state) =>
        {
            for (int i = 0; i < state.Source.Length; i++)
            {
                span[i] = invalidFileNameCharsSet.Contains(state.Source[i]) ? state.Replacement : state.Source[i];
            }
        });
    }

    /// <summary>
    /// Converts the <paramref name="value"/> to a safe file path.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="replacement">The replacement character for invalid path characters.</param>
    /// <returns>A safe file path string, or <see langword="null"/> if <paramref name="value"/> is null or whitespace.</returns>
    public static string? ToSafeFilePath(this string? value, char replacement = '_')
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (invalidPathCharsSet.Contains(replacement))
        {
            throw new ArgumentException("Invalid replacement.", nameof(replacement));
        }

        return string.Create(value!.Length, (Source: value!, Replacement: replacement), static (span, state) =>
        {
            for (int i = 0; i < state.Source.Length; i++)
            {
                span[i] = invalidPathCharsSet.Contains(state.Source[i]) ? state.Replacement : state.Source[i];
            }
        });
    }

    /// <summary>
    /// Converts the <paramref name="value"/> to decimal.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="defaultValue">If specified string is not decimal,then return the default value.</param>
    /// <returns></returns>
    public static decimal? ToDecimal(this string? value, decimal? defaultValue = null)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return defaultValue;
        }

        if (decimal.TryParse(value, out decimal result))
        {
            return result;
        }

        return defaultValue;
    }

    /// <summary>
    /// Re-encodes the <paramref name="value"/> through a UTF-8 round-trip (string → UTF-8 bytes → string).
    /// </summary>
    /// <remarks>
    /// Because .NET strings are always valid Unicode, this operation is a no-op for well-formed input.
    /// It can be used to strip characters that cannot be represented in UTF-8, such as lone surrogates,
    /// by relying on the encoder's substitution fallback behaviour.
    /// </remarks>
    /// <param name="value">The value.</param>
    /// <returns>The UTF-8 round-tripped string, or <see cref="string.Empty"/> if <paramref name="value"/> is null or whitespace.</returns>
    public static string ToUTF8(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(value!));
    }

    #endregion

    /// <summary>
    /// Return the specified string with first character upper status.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string FirstCharToUpper(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        if (value!.Length == 1)
        {
            return value.ToUpper(CultureInfo.CurrentCulture);
        }

        return string.Create(value.Length, (value, CultureInfo.CurrentCulture), static (span, state) =>
        {
            span[0] = char.ToUpper(state.value[0], state.CurrentCulture);
            state.value.AsSpan(1).CopyTo(span[1..]);
        });
    }

    /// <summary>
    /// Replaces the all special sharacters in <paramref name="value"/> with <paramref name="replacement"/>.
    /// Only remain the letter and digital.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="replacement">The replacement.</param>
    /// <returns></returns>
    public static string ReplaceSpecialSharacters(this string? value, char replacement = char.MinValue)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        if (replacement == char.MinValue)
        {
            return new string(value!.Where(c => c.IsLetterOrDigit()).ToArray());
        }

        return string.Create(value!.Length, (Source: value!, Replacement: replacement), static (span, state) =>
        {
            for (int i = 0; i < state.Source.Length; i++)
            {
                span[i] = state.Source[i].IsLetterOrDigit() ? state.Source[i] : state.Replacement;
            }
        });
    }

    /// <summary>
    /// Gets the full chinese phonetic alphabet.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string GetFullChinesePhoneticAlphabet(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return PinYinHelper.Get(value!);
    }

    /// <summary>
    /// Gets the html decode string of <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string HtmlDecode(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return HttpUtility.HtmlDecode(value!);
    }

    /// <summary>
    /// Gets the html encode string of <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string HtmlEncode(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return HttpUtility.HtmlEncode(value!);
    }

    /// <summary>
    /// Decodes a  URL string.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string UrlDecode(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return HttpUtility.UrlDecode(value!);
    }

    /// <summary>
    /// Encodes a  URL string.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string UrlEncode(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return HttpUtility.UrlEncode(value!);
    }

    /// <summary>
    /// Remove all new line chars in the <paramref name="value"/>, include \r \n.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <returns></returns>
    public static string RemoveNewLines(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return value!.Replace("\r\n", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
    }

    /// <summary>
    /// Decodes a Base64 encoded string to its original string representation using a specified encoding.
    /// </summary>
    /// <param name="base64String">The Base64 encoded string to decode.</param>
    /// <param name="encoding">The encoding to use for the decoded string. If null, use UTF8.</param>
    /// <returns>The decoded string.</returns>
    public static string FromBase64String(this string? base64String, Encoding? encoding = null)
    {
        if (string.IsNullOrWhiteSpace(base64String))
        {
            return string.Empty;
        }

        encoding ??= Encoding.UTF8;

        try
        {
            byte[] decodedBytes = Convert.FromBase64String(base64String!);

            return encoding.GetString(decodedBytes);
        }
        catch (FormatException)
        {
            throw new ArgumentException("The specified string is not a valid Base64-encoded string.", nameof(base64String));
        }
    }

    /// <summary>
    /// Encodes a given string to a Base64 string.
    /// </summary>
    /// <param name="value">The input string to be encoded.</param>
    /// <param name="encoding">The encoding to use for the decoded string. If null, use UTF8.</param>
    /// <returns>The Base64 encoded string.</returns>
    public static string ToBase64String(this string? value, Encoding? encoding = null)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        encoding ??= Encoding.UTF8;

        byte[] bytes = encoding.GetBytes(value!);

        return Convert.ToBase64String(bytes);
    }
}
