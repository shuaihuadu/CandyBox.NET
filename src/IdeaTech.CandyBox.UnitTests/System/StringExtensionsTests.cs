// Copyright (c) IdeaTech. All rights reserved.

namespace System.Extensions.UnitTests.System;

[TestClass]
public class StringExtensionsTests
{
    #region IsEmail

    [DataTestMethod]
    [DataRow(null, "34candybox@net.com")]
    [DataRow("", "candy.box@net.com")]
    [DataRow("  ", "1candy-box@net.com")]
    [DataRow("candy@box@net.com", "candy133@boxnet.com")]
    public void IsEmailShouldWorksCorrectly(string value1, string value2)
    {
        Assert.IsFalse(value1.IsEmail());
        Assert.IsTrue(value2.IsEmail());
    }

    #endregion

    #region IsChineseMobile

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [DataRow("123")]
    [DataRow("11234567890")]
    [DataRow("12345678901")]
    [DataRow("A1234567890")]
    public void IsChineseMobileWithInvalidValueShouldReturnFalse(string value)
    {
        Assert.IsFalse(value.IsChineseMobile());
    }

    [DataTestMethod]
    [DataRow("13566788990")]
    [DataRow("15890099000")]
    [DataRow("17777788999")]
    public void IsChineseMobileWithValidValueShouldReturnTrue(string value)
    {
        Assert.IsTrue(value.IsChineseMobile());
    }

    #endregion

    #region IsUrl

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [DataRow("not-a-url")]
    [DataRow("ftp://")]
    public void IsUrlWithInvalidValueShouldReturnFalse(string value)
    {
        Assert.IsFalse(value.IsUrl());
    }

    [DataTestMethod]
    [DataRow("https://www.example.com")]
    [DataRow("http://example.com/path")]
    public void IsUrlWithValidValueShouldReturnTrue(string value)
    {
        Assert.IsTrue(value.IsUrl());
    }

    [TestMethod]
    public void IsUrlWithQueryStringShouldReturnFalseWhenNotAllowed()
    {
        Assert.IsFalse("https://example.com/path?key=value".IsUrl(allowQuery: false));
    }

    [TestMethod]
    public void IsUrlWithQueryStringShouldReturnTrueWhenAllowed()
    {
        Assert.IsTrue("https://example.com/path?key=value".IsUrl(allowQuery: true));
    }

    #endregion

    #region IsHans

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [DataRow("hello")]
    [DataRow("123")]
    public void IsHansWithNonHansValueShouldReturnFalse(string value)
    {
        Assert.IsFalse(value.IsHans());
    }

    [DataTestMethod]
    [DataRow("你好")]
    [DataRow("hello世界")]
    public void IsHansWithHansValueShouldReturnTrue(string value)
    {
        Assert.IsTrue(value.IsHans());
    }

    #endregion

    #region IsValidLength

    [TestMethod]
    public void IsValidLengthWithNullShouldThrow()
    {
        Assert.ThrowsException<ArgumentNullException>(() => ((string?)null).IsValidLength(0, 10));
    }

    [TestMethod]
    public void IsValidLengthWithNegativeMinLengthShouldThrow()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => "hello".IsValidLength(-1, 10));
    }

    [TestMethod]
    public void IsValidLengthWithMaxLessThanMinShouldThrow()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => "hello".IsValidLength(5, 3));
    }

    [DataTestMethod]
    [DataRow("hi", 3, 10)]
    [DataRow("toolongstring", 1, 5)]
    public void IsValidLengthOutOfRangeShouldReturnFalse(string value, int min, int max)
    {
        Assert.IsFalse(value.IsValidLength(min, max));
    }

    [DataTestMethod]
    [DataRow("hello", 1, 10)]
    [DataRow("hello", 5, 5)]
    public void IsValidLengthInRangeShouldReturnTrue(string value, int min, int max)
    {
        Assert.IsTrue(value.IsValidLength(min, max));
    }

    #endregion

    #region IsValidByteCount

    [TestMethod]
    public void IsValidByteCountWithNullShouldThrow()
    {
        Assert.ThrowsException<ArgumentNullException>(() => ((string?)null).IsValidByteCount(0, 10));
    }

    [TestMethod]
    public void IsValidByteCountWithNegativeMinShouldThrow()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => "hello".IsValidByteCount(-1, 10));
    }

    [TestMethod]
    public void IsValidByteCountWithMaxLessThanMinShouldThrow()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => "hello".IsValidByteCount(5, 3));
    }

    [TestMethod]
    public void IsValidByteCountWithValueInRangeShouldReturnTrue()
    {
        Assert.IsTrue("hello".IsValidByteCount(1, 20));
    }

    [TestMethod]
    public void IsValidByteCountWithValueOutOfRangeShouldReturnFalse()
    {
        Assert.IsFalse("hello world".IsValidByteCount(1, 5));
    }

    #endregion

    #region IsGuid

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [DataRow("not-a-guid")]
    public void IsGuidWithInvalidValueShouldReturnFalse(string value)
    {
        Assert.IsFalse(value.IsGuid());
    }

    [TestMethod]
    public void IsGuidWithValidGuidShouldReturnTrue()
    {
        string guid = Guid.NewGuid().ToString("D");
        Assert.IsTrue(guid.IsGuid("D"));
    }

    [TestMethod]
    public void IsGuidWithUnknownFormatFallsBackToD()
    {
        string guid = Guid.NewGuid().ToString("D");
        Assert.IsTrue(guid.IsGuid("INVALID_FORMAT"));
    }

    #endregion

    #region IsValidCultureIdentifier

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [DataRow("xx-INVALID")]
    public void IsValidCultureIdentifierWithInvalidValueShouldReturnFalse(string value)
    {
        Assert.IsFalse(value.IsValidCultureIdentifier());
    }

    [DataTestMethod]
    [DataRow("en-US")]
    [DataRow("zh-CN")]
    [DataRow("fr-FR")]
    public void IsValidCultureIdentifierWithValidValueShouldReturnTrue(string value)
    {
        Assert.IsTrue(value.IsValidCultureIdentifier());
    }

    #endregion

    #region IsByte / IsShort / IsInt32 / IsInt64 / IsDecimal / IsFloat

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [DataRow("abc")]
    [DataRow("999")]
    public void IsByteWithInvalidValueShouldReturnFalse(string value)
    {
        Assert.IsFalse(value.IsByte());
    }

    [DataTestMethod]
    [DataRow("0")]
    [DataRow("128")]
    [DataRow("255")]
    public void IsByteWithValidValueShouldReturnTrue(string value)
    {
        Assert.IsTrue(value.IsByte());
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [DataRow("abc")]
    public void IsShortWithInvalidValueShouldReturnFalse(string value)
    {
        Assert.IsFalse(value.IsShort());
    }

    [DataTestMethod]
    [DataRow("0")]
    [DataRow("1000")]
    [DataRow("-32768")]
    public void IsShortWithValidValueShouldReturnTrue(string value)
    {
        Assert.IsTrue(value.IsShort(Globalization.NumberStyles.AllowLeadingSign));
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [DataRow("abc")]
    public void IsInt32WithInvalidValueShouldReturnFalse(string value)
    {
        Assert.IsFalse(value.IsInt32());
    }

    [DataTestMethod]
    [DataRow("0")]
    [DataRow("2147483647")]
    public void IsInt32WithValidValueShouldReturnTrue(string value)
    {
        Assert.IsTrue(value.IsInt32());
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [DataRow("abc")]
    public void IsInt64WithInvalidValueShouldReturnFalse(string value)
    {
        Assert.IsFalse(value.IsInt64());
    }

    [DataTestMethod]
    [DataRow("0")]
    [DataRow("9223372036854775807")]
    public void IsInt64WithValidValueShouldReturnTrue(string value)
    {
        Assert.IsTrue(value.IsInt64());
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [DataRow("abc")]
    public void IsDecimalWithInvalidValueShouldReturnFalse(string value)
    {
        Assert.IsFalse(value.IsDecimal());
    }

    [DataTestMethod]
    [DataRow("0")]
    [DataRow("123.45")]
    public void IsDecimalWithValidValueShouldReturnTrue(string value)
    {
        Assert.IsTrue(value.IsDecimal(Globalization.NumberStyles.AllowDecimalPoint));
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [DataRow("abc")]
    public void IsFloatWithInvalidValueShouldReturnFalse(string value)
    {
        Assert.IsFalse(value.IsFloat());
    }

    [DataTestMethod]
    [DataRow("0")]
    [DataRow("3.14")]
    public void IsFloatWithValidValueShouldReturnTrue(string value)
    {
        Assert.IsTrue(value.IsFloat(Globalization.NumberStyles.AllowDecimalPoint));
    }

    #endregion

    #region IsDateTime

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [DataRow("not-a-date")]
    public void IsDateTimeWithInvalidValueShouldReturnFalse(string value)
    {
        Assert.IsFalse(value.IsDateTime());
    }

    [DataTestMethod]
    [DataRow("2024-01-01")]
    [DataRow("2024-06-15 12:30:00")]
    public void IsDateTimeWithValidValueShouldReturnTrue(string value)
    {
        Assert.IsTrue(value.IsDateTime());
    }

    #endregion

    #region IsLetter

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [DataRow("abc123")]
    [DataRow("hello world")]
    public void IsLetterWithNonLetterValueShouldReturnFalse(string value)
    {
        Assert.IsFalse(value.IsLetter());
    }

    [DataTestMethod]
    [DataRow("hello")]
    [DataRow("ABC")]
    [DataRow("CandyBox")]
    public void IsLetterWithAllLetterValueShouldReturnTrue(string value)
    {
        Assert.IsTrue(value.IsLetter());
    }

    #endregion

    #region IsBase64String

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [DataRow("not-base64!!")]
    [DataRow("abc")]
    public void IsBase64StringWithInvalidValueShouldReturnFalse(string value)
    {
        Assert.IsFalse(value.IsBase64String());
    }

    [TestMethod]
    public void IsBase64StringWithValidValueShouldReturnTrue()
    {
        string encoded = Convert.ToBase64String(Text.Encoding.UTF8.GetBytes("CandyBox"));
        Assert.IsTrue(encoded.IsBase64String());
    }

    #endregion

    #region TrimAll

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void TrimAllWithNullOrWhiteSpaceShouldReturnEmpty(string value)
    {
        Assert.AreEqual(string.Empty, value.TrimAll());
    }

    [TestMethod]
    public void TrimAllShouldRemoveInvisibleCharacters()
    {
        string value = "hello\x00world\x01";
        Assert.AreEqual("helloworld", value.TrimAll());
    }

    [TestMethod]
    public void TrimAllWithNormalStringShouldReturnSame()
    {
        Assert.AreEqual("hello", "hello".TrimAll());
    }

    #endregion

    #region TrimBlank

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void TrimBlankWithNullOrWhiteSpaceShouldReturnEmpty(string value)
    {
        Assert.AreEqual(string.Empty, value.TrimBlank());
    }

    [TestMethod]
    public void TrimBlankShouldRemoveLeadingAndTrailingInvisibleCharacters()
    {
        string value = "\x00hello\x00";
        Assert.AreEqual("hello", value.TrimBlank());
    }

    [TestMethod]
    public void TrimBlankShouldNotRemoveMiddleInvisibleCharacters()
    {
        string value = "hel\x00lo";
        Assert.IsTrue(value.TrimBlank().Contains('\x00'));
    }

    #endregion

    #region Reverse

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ReverseWithNullOrWhiteSpaceShouldReturnEmpty(string value)
    {
        Assert.AreEqual(string.Empty, value.Reverse());
    }

    [DataTestMethod]
    [DataRow("abc", "cba")]
    [DataRow("hello", "olleh")]
    [DataRow("a", "a")]
    public void ReverseShouldReturnReversedString(string value, string expected)
    {
        Assert.AreEqual(expected, value.Reverse());
    }

    #endregion

    #region Truncate

    [TestMethod]
    public void TruncateWithNegativeLengthShouldThrow()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => "hello".Truncate(-1));
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void TruncateWithNullOrWhiteSpaceShouldReturnInput(string value)
    {
        Assert.AreEqual(value, value.Truncate(5));
    }

    [TestMethod]
    public void TruncateWhenStringShorterThanLengthShouldReturnOriginal()
    {
        Assert.AreEqual("hi", "hi".Truncate(10));
    }

    [TestMethod]
    public void TruncateWhenStringLongerThanLengthShouldTruncateWithReplacement()
    {
        string result = "hello world".Truncate(5);
        Assert.AreEqual("hello ...", result);
    }

    [TestMethod]
    public void TruncateWithCustomReplacementShouldUseIt()
    {
        string result = "hello world".Truncate(5, "---");
        Assert.AreEqual("hello---", result);
    }

    #endregion

    #region ToByte / ToInt16 / ToInt32 / ToInt64

    [TestMethod]
    public void ToByteWithValidStringShouldReturnByte()
    {
        Assert.AreEqual((byte)200, "200".ToByte());
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("abc")]
    [DataRow("999")]
    public void ToByteWithInvalidStringShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentException>(() => value.ToByte());
    }

    [TestMethod]
    public void ToInt16WithValidStringShouldReturnShort()
    {
        Assert.AreEqual((short)1000, "1000".ToInt16());
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("abc")]
    public void ToInt16WithInvalidStringShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentException>(() => value.ToInt16());
    }

    [TestMethod]
    public void ToInt32WithValidStringShouldReturnInt()
    {
        Assert.AreEqual(42, "42".ToInt32());
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("abc")]
    public void ToInt32WithInvalidStringShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentException>(() => value.ToInt32());
    }

    [TestMethod]
    public void ToInt64WithValidStringShouldReturnLong()
    {
        Assert.AreEqual(9999999999L, "9999999999".ToInt64());
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("abc")]
    public void ToInt64WithInvalidStringShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentException>(() => value.ToInt64());
    }

    #endregion

    #region ToEnum

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToEnumWithNullOrWhiteSpaceShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.ToEnum<DayOfWeek>());
    }

    [TestMethod]
    public void ToEnumWithInvalidValueShouldThrow()
    {
        Assert.ThrowsException<ArgumentException>(() => "NotADay".ToEnum<DayOfWeek>());
    }

    [DataTestMethod]
    [DataRow("Monday", DayOfWeek.Monday)]
    [DataRow("FRIDAY", DayOfWeek.Friday)]
    [DataRow("sunday", DayOfWeek.Sunday)]
    public void ToEnumWithValidValueShouldReturnEnum(string value, DayOfWeek expected)
    {
        Assert.AreEqual(expected, value.ToEnum<DayOfWeek>());
    }

    #endregion

    #region ToGuid

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToGuidWithNullOrWhiteSpaceShouldReturnNull(string value)
    {
        Assert.IsNull(value.ToGuid());
    }

    [TestMethod]
    public void ToGuidWithInvalidStringShouldThrow()
    {
        Assert.ThrowsException<ArgumentException>(() => "not-a-guid".ToGuid());
    }

    [TestMethod]
    public void ToGuidWithValidStringShouldReturnGuid()
    {
        Guid expected = Guid.NewGuid();
        Assert.AreEqual(expected, expected.ToString("D").ToGuid("D"));
    }

    #endregion

    #region ToDateTimeWithPartten

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToDateTimeWithParttenWithNullOrWhiteSpaceShouldReturnDefault(string value)
    {
        DateTime? defaultValue = new DateTime(2000, 1, 1);
        Assert.AreEqual(defaultValue, value.ToDateTimeWithPartten(defaultValue: defaultValue));
    }

    [TestMethod]
    public void ToDateTimeWithParttenWithValidStringShouldReturnDateTime()
    {
        DateTime? result = "2024-06-15".ToDateTimeWithPartten("yyyy-MM-dd");
        Assert.IsNotNull(result);
        Assert.AreEqual(new DateTime(2024, 6, 15), result.Value);
    }

    [TestMethod]
    public void ToDateTimeWithParttenWithMismatchedPatternShouldReturnDefault()
    {
        DateTime? defaultValue = new DateTime(2000, 1, 1);
        DateTime? result = "2024/06/15".ToDateTimeWithPartten("yyyy-MM-dd", defaultValue);
        Assert.AreEqual(defaultValue, result);
    }

    #endregion

    #region ToSafeFileName

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToSafeFileNameWithNullOrWhiteSpaceShouldReturnNull(string value)
    {
        Assert.IsNull(value.ToSafeFileName());
    }

    [TestMethod]
    public void ToSafeFileNameShouldReplaceInvalidChars()
    {
        string result = "file<name>.txt".ToSafeFileName('_')!;
        Assert.IsFalse(result.Any(c => IO.Path.GetInvalidFileNameChars().Contains(c)));
        Assert.IsTrue(result.Contains('_'));
    }

    [TestMethod]
    public void ToSafeFileNameWithValidNameShouldReturnSame()
    {
        Assert.AreEqual("validname.txt", "validname.txt".ToSafeFileName());
    }

    #endregion

    #region ToSafeFilePath

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToSafeFilePathWithNullOrWhiteSpaceShouldReturnNull(string value)
    {
        Assert.IsNull(value.ToSafeFilePath());
    }

    [TestMethod]
    public void ToSafeFilePathWithValidPathShouldReturnSame()
    {
        Assert.AreEqual(@"C:\folder\file.txt", @"C:\folder\file.txt".ToSafeFilePath());
    }

    #endregion

    #region ToDecimal

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [DataRow("abc")]
    public void ToDecimalWithInvalidValueShouldReturnDefault(string value)
    {
        decimal? defaultValue = 99.9m;
        Assert.AreEqual(defaultValue, value.ToDecimal(defaultValue));
    }

    [TestMethod]
    public void ToDecimalWithValidValueShouldReturnDecimal()
    {
        Assert.AreEqual(123.45m, "123.45".ToDecimal());
    }

    [TestMethod]
    public void ToDecimalWithNullAndNoDefaultShouldReturnNull()
    {
        Assert.IsNull(((string?)null).ToDecimal());
    }

    #endregion

    #region ToUTF8

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToUTF8WithNullOrWhiteSpaceShouldReturnEmpty(string value)
    {
        Assert.AreEqual(string.Empty, value.ToUTF8());
    }

    [TestMethod]
    public void ToUTF8WithNormalStringShouldReturnSame()
    {
        Assert.AreEqual("CandyBox", "CandyBox".ToUTF8());
    }

    #endregion

    #region FirstCharToUpper

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void FirstCharToUpperWithNullOrWhiteSpaceShouldReturnEmpty(string value)
    {
        Assert.AreEqual(string.Empty, value.FirstCharToUpper());
    }

    [DataTestMethod]
    [DataRow("hello", "Hello")]
    [DataRow("world", "World")]
    [DataRow("a", "A")]
    public void FirstCharToUpperShouldUppercaseFirstChar(string value, string expected)
    {
        Assert.AreEqual(expected, value.FirstCharToUpper());
    }

    [TestMethod]
    public void FirstCharToUpperWithAlreadyUpperShouldReturnSame()
    {
        Assert.AreEqual("Hello", "Hello".FirstCharToUpper());
    }

    #endregion

    #region ReplaceSpecialSharacters

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ReplaceSpecialSharacterWithNullOrWhiteSpaceShouldReturnEmpty(string value)
    {
        Assert.AreEqual(string.Empty, value.ReplaceSpecialSharacters());
    }

    [TestMethod]
    public void ReplaceSpecialSharactersShouldRemoveSpecialCharsWhenNoReplacement()
    {
        Assert.AreEqual("hello123", "hello@123!".ReplaceSpecialSharacters());
    }

    [TestMethod]
    public void ReplaceSpecialSharactersShouldReplaceSpecialCharsWithReplacement()
    {
        Assert.AreEqual("hello_123_", "hello@123!".ReplaceSpecialSharacters('_'));
    }

    #endregion

    #region HtmlEncode / HtmlDecode

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void HtmlEncodeWithNullOrWhiteSpaceShouldReturnEmpty(string value)
    {
        Assert.AreEqual(string.Empty, value.HtmlEncode());
    }

    [TestMethod]
    public void HtmlEncodeShouldEncodeSpecialChars()
    {
        Assert.AreEqual("&lt;b&gt;bold&lt;/b&gt;", "<b>bold</b>".HtmlEncode());
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void HtmlDecodeWithNullOrWhiteSpaceShouldReturnEmpty(string value)
    {
        Assert.AreEqual(string.Empty, value.HtmlDecode());
    }

    [TestMethod]
    public void HtmlDecodeShouldDecodeEncodedString()
    {
        Assert.AreEqual("<b>bold</b>", "&lt;b&gt;bold&lt;/b&gt;".HtmlDecode());
    }

    [TestMethod]
    public void HtmlEncodeAndDecodeShouldRoundTrip()
    {
        string original = "<script>alert('xss')</script>";
        Assert.AreEqual(original, original.HtmlEncode().HtmlDecode());
    }

    #endregion

    #region UrlEncode / UrlDecode

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void UrlEncodeWithNullOrWhiteSpaceShouldReturnEmpty(string value)
    {
        Assert.AreEqual(string.Empty, value.UrlEncode());
    }

    [TestMethod]
    public void UrlEncodeShouldEncodeSpecialChars()
    {
        string encoded = "hello world&foo=bar".UrlEncode();
        Assert.IsFalse(encoded.Contains(' '));
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void UrlDecodeWithNullOrWhiteSpaceShouldReturnEmpty(string value)
    {
        Assert.AreEqual(string.Empty, value.UrlDecode());
    }

    [TestMethod]
    public void UrlEncodeAndDecodeShouldRoundTrip()
    {
        string original = "hello world&foo=bar";
        Assert.AreEqual(original, original.UrlEncode().UrlDecode());
    }

    #endregion

    #region RemoveNewLines

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void RemoveNewLinesWithNullOrWhiteSpaceShouldReturnEmpty(string value)
    {
        Assert.AreEqual(string.Empty, value.RemoveNewLines());
    }

    [DataTestMethod]
    [DataRow("hello\r\nworld", "helloworld")]
    [DataRow("line1\nline2", "line1line2")]
    [DataRow("line1\rline2", "line1line2")]
    public void RemoveNewLinesShouldRemoveAllNewLines(string value, string expected)
    {
        Assert.AreEqual(expected, value.RemoveNewLines());
    }

    #endregion

    #region ToBase64String / FromBase64String

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToBase64StringWithNullOrWhiteSpaceShouldReturnEmpty(string value)
    {
        Assert.AreEqual(string.Empty, value.ToBase64String());
    }

    [TestMethod]
    public void ToBase64StringWithValidValueShouldReturnBase64()
    {
        string result = "CandyBox".ToBase64String();
        Assert.IsFalse(string.IsNullOrEmpty(result));
        Assert.IsTrue(result.IsBase64String());
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void FromBase64StringWithNullOrWhiteSpaceShouldReturnEmpty(string value)
    {
        Assert.AreEqual(string.Empty, value.FromBase64String());
    }

    [TestMethod]
    public void FromBase64StringWithInvalidBase64ShouldThrow()
    {
        Assert.ThrowsException<ArgumentException>(() => "not-valid-base64!!!".FromBase64String());
    }

    [TestMethod]
    public void ToBase64StringAndFromBase64StringShouldRoundTrip()
    {
        string original = "Hello, CandyBox!";
        Assert.AreEqual(original, original.ToBase64String().FromBase64String());
    }

    #endregion
}
