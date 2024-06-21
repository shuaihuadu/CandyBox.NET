// Copyright (c) IdeaTech. All rights reserved.

namespace System;

/// <summary>
/// Common extensions of <see cref="string"/>.
/// </summary>
public static class StringExtensions
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

    #region IsXXXXXX

    /// <summary>
    /// Indicates whether the specified string is null or an System.String.Empty string(Include all invisible  characters).
    /// <param name="value">The string to test.</param>
    /// <returns>true if the value parameter is null or an empty string ("") or whitespace; otherwise, false.</returns>
    /// </summary>
    public static bool IsNullOrBlank(this string value)
    {
        if (value == null || string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
        {
            return true;
        }

        if (value.Trim(invisiableCharacters).Length == 0)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Indicates whether the specified string is null or an System.String.Empty string(Include all invisible  characters).
    /// <param name="value">The string to test.</param>
    /// <returns>true if the value parameter is not null or not an empty string (""); otherwise, false.</returns>
    /// </summary>
    public static bool IsNotNullOrBlank(this string value)
    {
        return !IsNullOrBlank(value);
    }

    /// <summary>
    /// Indicates whether the specified string is a correct email address.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns>true if the value parameter is a correct email address; otherwise, false.</returns>
    public static bool IsEmail(this string value)
    {
        if (value.IsNullOrBlank())
        {
            return false;
        }

        return new Regex(@"^[a-zA-Z0-9_+.-]+\@([a-zA-Z0-9-]+\.)+[a-zA-Z0-9]{2,4}$").Match(value).Success;
    }

    /// <summary>
    /// Indicates whether the specified string is a correct chinese mobile phone number.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns>true if the value parameter is a correct chinese mobile phone number; otherwise, false.</returns>
    public static bool IsChineseMobile(this string value)
    {
        if (value.IsNullOrBlank())
        {
            return false;
        }

        return new Regex(@"^1(3[0-9]|4[57]|5[0-35-9]|7[0678]|8[0-9])\d{8}$").Match(value).Success;
    }

    /// <summary>
    /// Indicates whether the specified string is a correct uri.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns>true if the value parameter is a correct uri; otherwise ,false.</returns>
    public static bool IsUrl(this string value)
    {
        return Uri.TryCreate(value, UriKind.Absolute, out Uri? uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
    }

    /// <summary>
    /// Indicates whether the specified string is Chinese character.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns>true if the value parameter is Chinese character; otherwise, false.</returns>
    public static bool IsHans(this string value)
    {
        if (value.IsNullOrBlank())
        {
            return false;
        }

        return Regex.Match(value, @"[\u4e00-\u9fa5]").Success;
    }

    /// <summary>
    /// Indicates whether the specified string is between <paramref name="minLength"/> and <paramref name="maxLength"/>.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <param name="minLength">The min length for validate.</param>
    /// <param name="maxLength">The max length for validate.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns>true if the value is valid length;otherwise, false.</returns>
    public static bool IsValidLength(this string value, int minLength, int maxLength)
    {
        if (minLength < 0)
        {
            throw new ArgumentException("The minLength must be greater than 0.");
        }

        if (maxLength < minLength)
        {
            throw new ArgumentException("The maxLength must be greater than the minLength.");
        }

        if (maxLength > int.MaxValue)
        {
            throw new ArgumentException("The minLength must be less than Int32.MaxValue.");
        }

        if (value == null)
        {
            throw new ArgumentException("value can not be null.");
        }

        return value.Length >= minLength && value.Length <= maxLength;
    }

    /// <summary>
    /// Indicates whether the specified string bytes is between <paramref name="min"/> and <paramref name="max"/>.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <param name="min">The min bytes count.</param>
    /// <param name="max">The max bytes count.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns>true if the value is valid byte count;otherwise, false.</returns>
    public static bool IsValidByteCount(this string value, int min, int max)
    {
        if (min < 0)
        {
            throw new ArgumentException("min must be greater than 0.");
        }

        if (max < min)
        {
            throw new ArgumentException("The max must be greater than the min.");
        }

        if (max > int.MaxValue)
        {
            throw new ArgumentException("The max must be less than Int32.MaxValue.");
        }

        if (value == null)
        {
            throw new ArgumentException("value can not be null.");
        }

        int count = Text.Encoding.Default.GetByteCount(value);

        return count >= min && count <= max;
    }

    /// <summary>
    /// Indicates whether the specified string is a correct <see cref="Guid"/>.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <param name="format">GUID format type.</param>
    /// <returns>true if the value parameter is a correct <see cref="Guid"/>; otherwise, false.</returns>
    public static bool IsGuid(this string value, string format = "D")
    {
        string[] formats = ["D", "d", "N", "n", "P", "p", "B", "b", "X", "x"];

        if (!formats.Contains(format))
        {
            format = formats[0];
        }

        return Guid.TryParse(value, out Guid guid);
    }

    /// <summary>
    /// Determines whether the <paramref name="value"/> is a valid culture identifier.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    ///   <c>true</c> if the <paramref name="value"/> is valid culture identifier; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsValidCultureIdentifier(this string value)
    {
        try
        {
            CultureInfo c = CultureInfo.GetCultureInfo(value);
            return true;
        }
        catch
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
    public static bool IsByte(this string value, NumberStyles style = NumberStyles.None)
    {
        try
        {
            byte.Parse(value, style);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Determines whether this instance is short.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="style">The number style.</param>
    /// <returns>
    ///   <c>true</c> if the specified value is short; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsShort(this string value, NumberStyles style = NumberStyles.None)
    {
        try
        {
            short.Parse(value, style);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Determines whether this instance is int32.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="style">The number style.</param>
    /// <returns>
    ///   <c>true</c> if the specified value is int32; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsInt32(this string value, NumberStyles style = NumberStyles.None)
    {
        try
        {
            int.Parse(value, style);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Determines whether this instance is int64.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="style">The number style.</param>
    /// <returns>
    ///   <c>true</c> if the specified value is int64; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsInt64(this string value, NumberStyles style = NumberStyles.None)
    {
        try
        {
            int.Parse(value, style);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Determines whether this instance is decimal.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="style">The number style.</param>
    /// <returns>
    ///   <c>true</c> if the specified value is decimal; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsDecimal(this string value, NumberStyles style = NumberStyles.None)
    {
        try
        {
            decimal.Parse(value, style);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Determines whether this instance is float.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="style">The number style.</param>
    /// <returns>
    ///   <c>true</c> if the specified value is float; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsFloat(this string value, NumberStyles style = NumberStyles.None)
    {
        try
        {
            float.Parse(value, style);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
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
    public static bool IsDateTime(this string value, IFormatProvider? provider = null, DateTimeStyles styles = DateTimeStyles.None)
    {
        try
        {
            if (provider.IsNull())
            {
                DateTime.Parse(value);
            }
            else
            {
                DateTime.Parse(value, provider, styles);
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Determines whether the <paramref name="value"/> is a valid date time.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsDateTime(this string value)
    {
        if (value.IsNullOrBlank())
        {
            return false;
        }

        var dateTime = DateTimeExtensionConstants.DB_NULL_DATETIME;

        if (DateTime.TryParse(value, out dateTime))
        {
            return dateTime > DateTimeExtensionConstants.DB_NULL_DATETIME;
        }

        return false;
    }

    /// <summary>
    /// Determines whether this instance is letter.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    ///   <c>true</c> if the specified value is letter; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsLetter(this string value)
    {
        return Regex.IsMatch(value, "^[a-zA-Z]+$");
    }

    /// <summary>
    /// Determines whether a given string is a valid Base64-encoded string.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is Base64-encoded; otherwise, false.</returns>
    public static bool IsBase64String(string value)
    {
        try
        {
            Convert.FromBase64String(value);

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
    /// <param name="value">The string to test.</param>
    /// <returns>
    /// The string that remains after all invisible  characters are removed from the
    /// start and end of the current string. If no characters can be trimmed from the
    /// current instance, the method returns the current instance unchanged.
    ///</returns>
    public static string TrimAll(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        string result = TrimBlank(value);

        foreach (char item in invisiableCharacters)
        {
            result = result.Replace(new string([item]), string.Empty);
        }

        return result;
    }

    /// <summary>
    /// Removes all leading and trailing invisible  characters from the current System.String object.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns>
    /// The string that remains after all invisible  characters are removed from the
    /// start and end of the current string. If no characters can be trimmed from the
    /// current instance, the method returns the current instance unchanged.
    ///</returns>
    public static string TrimBlank(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return value.Trim(invisiableCharacters);
    }

    /// <summary>
    /// Reverse the specified string.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns>The reversed string</returns>
    public static string Reverse(this string value)
    {
        if (value.IsNullOrBlank())
        {
            return string.Empty;
        }

        char[] array = value.ToCharArray();
        Array.Reverse(array);

        return new string(array);
    }

    /// <summary>
    /// Truncate the specified string to the specified length.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <param name="length">The length of remained.</param>
    /// <param name="cutOffReplacement">The replacement of the removed part of the <see cref="string"/>.</param>
    /// <returns>The string truncated.</returns>
    public static string Truncate(this string value, int length, string cutOffReplacement = " ...")
    {
        if (string.IsNullOrEmpty(value) || value.Length <= length)
        {
            return value;
        }

        return $"{value.Remove(length)}{cutOffReplacement}";
    }

    /// <summary>
    /// Convert specified string to a <see cref="byte"/> value.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <param name="default">The default value while the convert faild.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns>The converted <see cref="byte"/> value.If the specified string is not a byte value,returns the <paramref name="default"/>.</returns>
    public static byte ToByte(this string value, byte @default = 0)
    {
        try
        {
            return value.ToByte();
        }
        catch (ArgumentException)
        {
            return @default;
        }
    }

    /// <summary>
    /// Convert specified string to a <see cref="short"/> value.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <param name="default">The default value while the convert faild.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns>The converted <see cref="short"/> value.If the specified string is not a <see cref="short"/> value,returns the <paramref name="default"/>.</returns>
    public static short ToInt16(this string value, short @default)
    {
        try
        {
            return value.ToInt16();
        }
        catch (ArgumentException)
        {
            return @default;
        }
    }

    /// <summary>
    /// Convert specified string to an <see cref="int"/> value.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <param name="default">The default value while the convert faild.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns>The converted <see cref="int"/> value.If the specified string is not a <see cref="int"/> value,returns the <paramref name="default"/>.</returns>
    public static int ToInt32(this string value, int @default = 0)
    {
        try
        {
            return value.ToInt32();
        }
        catch (ArgumentException)
        {
            return @default;
        }
    }

    /// <summary>
    /// Convert specified string to a <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <param name="default">The default value while the convert faild.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns>The converted <see cref="long"/> value.If the specified string is not a <see cref="long"/> value,returns the <paramref name="default"/>.</returns>
    public static long ToInt64(this string value, long @default = 0)
    {
        try
        {
            return value.ToInt64();
        }
        catch (ArgumentException)
        {
            return @default;
        }
    }

    /// <summary>
    /// Convert specified string to a <see cref="byte"/> value.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns>The converted <see cref="byte"/> value.</returns>
    public static byte ToByte(this string value)
    {
        if (!value.IsNullOrBlank() && byte.TryParse(value, out byte result))
        {
            return result;
        }

        throw new ArgumentException("The specified string is not a byte value.");
    }

    /// <summary>
    /// Convert specified string to a <see cref="short"/> value.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns>The converted <see cref="short"/> value.</returns>
    public static short ToInt16(this string value)
    {
        if (!value.IsNullOrBlank() && short.TryParse(value, out short result))
        {
            return result;
        }

        throw new ArgumentException("The specified string is not a short value.");
    }

    /// <summary>
    /// Convert specified string to an <see cref="int"/> value.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns>The converted <see cref="int"/> value.</returns>
    public static int ToInt32(this string value)
    {
        if (!value.IsNullOrBlank() && int.TryParse(value, out int result))
        {
            return result;
        }

        throw new ArgumentException("The specified string is not an int value.");
    }

    /// <summary>
    /// Convert specified string to a <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns>The converted <see cref="long"/> value.</returns>
    public static long ToInt64(this string value)
    {
        if (!value.IsNullOrBlank() && long.TryParse(value, out long result))
        {
            return result;
        }

        throw new ArgumentException("The specified string is not a long value.");
    }

    /// <summary>
    /// Convert specified string to an <see cref="Enum"/> value.
    /// </summary>
    /// <typeparam name="T">The type of enum.</typeparam>
    /// <param name="value">The string to test.</param>
    /// <param name="ignoreCase">true to ignore case during the comparison; otherwise, false.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns>The converted <see cref="Enum"/> value.</returns>
    public static T ToEnum<T>(this string value, bool ignoreCase = true)
    {
        if (value.IsNullOrBlank())
        {
            throw new ArgumentException("Must specify valid information for parsing in the string.", nameof(value));
        }

        Type t = typeof(T);

        if (!t.IsEnum)
        {
            throw new ArgumentException("Type provided must be an Enum.", "T");
        }

        return (T)Enum.Parse(t, value, ignoreCase);
    }

    /// <summary>
    /// Convert specified string to a <see cref="DateTime"/> value.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns>The converted <see cref="DateTime"/> value.</returns>
    public static DateTime ToDateTime(this string value)
    {
        if (!value.IsNullOrBlank() && DateTime.TryParse(value, out DateTime result))
        {
            return result;
        }

        throw new ArgumentException("The specified string is not an DateTime value.");
    }

    /// <summary>
    /// Convert specified string to a <see cref="Guid"/> value.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <param name="format">GUID format type.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns>The converted <see cref="Guid"/> value.</returns>
    public static Guid ToGuid(this string value, string format = "D")
    {
        string[] formats = ["D", "d", "N", "n", "P", "p", "B", "b", "X", "x"];

        if (!formats.Contains(format))
        {
            format = formats[0];
        }

        if (IsGuid(value, format))
        {
            return Guid.ParseExact(value, format);
        }

        throw new ArgumentException("Input string is not a valid GUID format.");
    }

    /// <summary>
    /// To the date time with partten.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <param name="pattern">The pattern.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static DateTime? ToDateTimeWithPartten(this string date, string pattern = DateTimeExtensionConstants.DEFAULT_DATE_FORMAT_PARTTEN, DateTime? defaultValue = null)
    {
        try
        {
            return DateTime.ParseExact(date, pattern, null);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// To the date time.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static DateTime? ToDateTime(this string date, DateTime? defaultValue = null)
    {
        if (date.IsNullOrBlank())
        {
            return null;
        }

        try
        {
            return date.ToDateTime();
        }
        catch (ArgumentException)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// Convert  to the <paramref name="value"/> to a safe file name.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="replacement">The replacement</param>
    /// <returns></returns>
    public static string ToSafeFileName(this string value, char replacement = '_')
    {
        var invalidChars = Path.GetInvalidFileNameChars();

        if (invalidChars.Contains(replacement))
        {
            throw new ArgumentException(null, nameof(replacement));
        }

        return invalidChars.Aggregate(value, (accmulate, result) => (accmulate.Replace(result, '_')));
    }

    /// <summary>
    /// Convert  to the <paramref name="value"/> to a safe file path.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="replacement">The replacement</param>
    /// <returns></returns>
    public static string ToSafeFilePath(this string value, char replacement = '_')
    {
        var invalidChars = Path.GetInvalidPathChars();

        if (invalidChars.Contains(replacement))
        {
            throw new ArgumentException(null, nameof(replacement));
        }

        return invalidChars.Aggregate(value, (accmulate, result) => (accmulate.Replace(result, '_')));
    }

    /// <summary>
    /// Conver the <paramref name="value"/> to decimal.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="defaultValue">If specified string is not decimal,then return the default value.</param>
    /// <returns></returns>
    public static decimal? ToDecimal(this string value, decimal? defaultValue = null)
    {
        if (value.IsNullOrBlank())
        {
            return null;
        }

        if (decimal.TryParse(value, out decimal result))
        {
            return result;
        }

        return defaultValue;
    }

    /// <summary>
    /// Set the <paramref name="value"/> to the utf-8 encoding.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string ToUTF8(this string value)
    {
        if (value.IsNullOrBlank())
        {
            return string.Empty;
        }

        return Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(value));
    }

    #endregion

    /// <summary>
    /// Return the specified string with first character upper status.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string FirstCharToUpper(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        if (value.Length > 1)
        {
            return char.ToUpper(value[0], CultureInfo.CurrentCulture) + value.Substring(1);
        }

        return value.ToUpper(CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Replaces the all special sharacters in <paramref name="value"/> with <paramref name="replacement"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="replacement">The replacement.</param>
    /// <returns></returns>
    public static string ReplaceSpecialSharacters(this string value, char replacement = char.MinValue)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        char[] chars = new char[value.Length];

        for (int i = 0; i < value.Length; i++)
        {
            if (!value[i].IsLetterOrDigit())
            {
                chars[i] = replacement;
            }
            else
            {
                chars[i] = value[i];
            }
        }

        if (replacement == char.MinValue)
        {
            return new string(chars.Where(x => x != replacement).ToArray());
        }

        return new string(chars);
    }

    /// <summary>
    /// Gets the full chinese phonetic alphabet.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string GetFullChinesePhoneticAlphabet(this string value) => PinYinHelper.Get(value);

    /// <summary>
    /// Gets the html decode string of <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string HtmlDecode(this string value)
    {
        if (value.IsNullOrBlank())
        {
            return string.Empty;
        }

        return HttpUtility.HtmlDecode(value);
    }

    /// <summary>
    /// Gets the html encode string of <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string HtmlEncode(this string value)
    {
        if (value.IsNullOrBlank())
        {
            return string.Empty;
        }

        return HttpUtility.HtmlEncode(value);
    }

    /// <summary>
    /// Decodes a  URL string.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string UrlDecode(this string value)
    {
        if (value.IsNullOrBlank())
        {
            return string.Empty;
        }

        return HttpUtility.UrlDecode(value);
    }

    /// <summary>
    /// Encodes a  URL string.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string UrlEncode(this string value)
    {
        if (value.IsNullOrBlank())
        {
            return string.Empty;
        }

        return HttpUtility.UrlEncode(value);
    }

    /// <summary>
    /// Remove all new line chars in the <paramref name="value"/>, include \r \n.
    /// </summary>
    /// <param name="value">需要转换的字符串</param>
    /// <returns></returns>
    public static string RemoveNewLines(this string value)
    {
        if (value.IsNullOrBlank())
        {
            return string.Empty;
        }

        return value.Replace("\r\n", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty).Replace(Environment.NewLine, string.Empty);
    }

    /// <summary>
    /// Decodes a Base64 encoded string to its original string representation using a specified encoding.
    /// </summary>
    /// <param name="base64String">The Base64 encoded string to decode.</param>
    /// <param name="encoding">The encoding to use for the decoded string. If null, use UTF8.</param>
    /// <returns>The decoded string.</returns>
    public static string Base64Decode(this string base64String, Encoding? encoding = null)
    {
        base64String.IsNullOrBlank().ThrowArgumentNullExceptionIfTrue(nameof(base64String));

        encoding ??= Encoding.UTF8;

        byte[] decodedBytes = Convert.FromBase64String(base64String);

        string decodedString = encoding.GetString(decodedBytes);

        return decodedString;
    }

    /// <summary>
    /// Encodes a given string to a Base64 string.
    /// </summary>
    /// <param name="value">The input string to be encoded.</param>
    /// <param name="encoding">The encoding to use for the decoded string. If null, use UTF8.</param>
    /// <returns>The Base64 encoded string.</returns>
    public static string Base64Encode(string value, Encoding? encoding = null)
    {
        value.IsNullOrBlank().ThrowArgumentNullExceptionIfTrue(nameof(value));

        encoding ??= Encoding.UTF8;

        byte[] bytes = Encoding.UTF8.GetBytes(value);

        return Convert.ToBase64String(bytes);
    }
}
