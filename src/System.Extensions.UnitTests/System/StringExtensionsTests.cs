// Copyright (c) IdeaTech. All rights reserved.

namespace System.Extensions.UnitTests.System;

[TestClass]
public class StringExtensionsTests
{
    [DataTestMethod]
    [DataRow(null, "1")]
    [DataRow("", "1kdfdsd")]
    [DataRow("   ", "你好呀")]
    public void IsNullOrBlankShouldWorksCorrectly(string value1, string value2)
    {
        Assert.IsTrue(value1.IsNullOrBlank());
        Assert.IsTrue(value2.IsNotNullOrBlank());

        string inVisibleString = new(StringExtensions.invisiableCharacters);
        Assert.IsTrue(inVisibleString.IsNullOrBlank());
    }

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

    /*
    

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IsEmailWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.IsEmail());
    }

    [TestMethod]
    public void IsChineseMobileShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1339652435";

        // Act
        var result = value.IsChineseMobile();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IsChineseMobileWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.IsChineseMobile());
    }

    [TestMethod]
    public void IsUrlShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1017031849";

        // Act
        var result = value.IsUrl();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IsUrlWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.IsUrl());
    }

    [TestMethod]
    public void IsHansShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1952453125";

        // Act
        var result = value.IsHans();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IsHansWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.IsHans());
    }

    [TestMethod]
    public void IsValidLengthShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue78780607";
        var minLength = 1230941462;
        var maxLength = 85227440;

        // Act
        var result = value.IsValidLength(minLength, maxLength);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IsValidLengthWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.IsValidLength(1010392417, 25070101));
    }

    [TestMethod]
    public void IsValidByteCountShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue287522979";
        var min = 1939001173;
        var max = 1263008725;

        // Act
        var result = value.IsValidByteCount(min, max);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IsValidByteCountWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.IsValidByteCount(779130403, 2004555957));
    }

    [TestMethod]
    public void IsGuidShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1903575819";
        var format = "TestValue1747099940";

        // Act
        var result = value.IsGuid(format);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IsGuidWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.IsGuid("TestValue1292782901"));
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IsGuidWithInvalidFormatShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => "TestValue2099133876".IsGuid(value));
    }

    [TestMethod]
    public void IsValidCultureIdentifierShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue472208589";

        // Act
        var result = value.IsValidCultureIdentifier();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IsValidCultureIdentifierWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.IsValidCultureIdentifier());
    }

    [TestMethod]
    public void IsByteShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1835024523";
        var style = NumberStyles.Float;

        // Act
        var result = value.IsByte(style);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IsByteWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.IsByte(NumberStyles.AllowTrailingSign));
    }

    [TestMethod]
    public void IsShortShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue246808033";
        var style = NumberStyles.AllowLeadingSign;

        // Act
        var result = value.IsShort(style);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IsShortWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.IsShort(NumberStyles.AllowDecimalPoint));
    }

    [TestMethod]
    public void IsInt32ShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue494914988";
        var style = NumberStyles.AllowDecimalPoint;

        // Act
        var result = value.IsInt32(style);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IsInt32WithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.IsInt32(NumberStyles.AllowParentheses));
    }

    [TestMethod]
    public void IsInt64ShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1932499122";
        var style = NumberStyles.AllowCurrencySymbol;

        // Act
        var result = value.IsInt64(style);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IsInt64WithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.IsInt64(NumberStyles.Currency));
    }

    [TestMethod]
    public void IsDecimalShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue637636788";
        var style = NumberStyles.Number;

        // Act
        var result = value.IsDecimal(style);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IsDecimalWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.IsDecimal(NumberStyles.Any));
    }

    [TestMethod]
    public void IsFloatShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1365593276";
        var style = NumberStyles.Number;

        // Act
        var result = value.IsFloat(style);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IsFloatWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.IsFloat(NumberStyles.AllowParentheses));
    }

    [TestMethod]
    public void IsDateTimeWithValueAndProviderAndStylesShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue284454831";
        var provider = Substitute.For<IFormatProvider>();
        var styles = DateTimeStyles.AllowInnerWhite;

        // Act
        var result = value.IsDateTime(provider, styles);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IsDateTimeWithValueAndProviderAndStylesWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.IsDateTime(Substitute.For<IFormatProvider>(), DateTimeStyles.AllowWhiteSpaces));
    }

    [TestMethod]
    public void IsDateTimeWithStringShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1288987164";

        // Act
        var result = value.IsDateTime();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IsDateTimeWithStringWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.IsDateTime());
    }

    [TestMethod]
    public void IsLetterShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue858583084";

        // Act
        var result = value.IsLetter();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IsLetterWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.IsLetter());
    }

    [TestMethod]
    public void IsBase64StringShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1114705545";

        // Act
        var result = StringExtensions.IsBase64String(value);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IsBase64StringWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => StringExtensions.IsBase64String(value));
    }

    [TestMethod]
    public void TrimAllShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue2082330401";

        // Act
        var result = value.TrimAll();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void TrimAllWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.TrimAll());
    }

    [TestMethod]
    public void TrimBlankShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue238141658";

        // Act
        var result = value.TrimBlank();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void TrimBlankWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.TrimBlank());
    }

    [TestMethod]
    public void ReverseShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1522111554";

        // Act
        var result = value.Reverse();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ReverseWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.Reverse());
    }

    [TestMethod]
    public void TruncateShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue871879989";
        var length = 1591248756;
        var cutOffReplacement = "TestValue413443505";

        // Act
        var result = value.Truncate(length, cutOffReplacement);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void TruncateWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.Truncate(2034556507, "TestValue1999096353"));
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void TruncateWithInvalidCutOffReplacementShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => "TestValue220363102".Truncate(313807107, value));
    }

    [TestMethod]
    public void ToByteWithStringAndByteShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1539183263";
        var @default = (byte)220;

        // Act
        var result = value.ToByte(@default);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToByteWithStringAndByteWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.ToByte((byte)223));
    }

    [TestMethod]
    public void ToInt16WithStringAndShortShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1092844402";
        var @default = (short)6430;

        // Act
        var result = value.ToInt16(@default);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToInt16WithStringAndShortWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.ToInt16((short)26005));
    }

    [TestMethod]
    public void ToInt32WithStringAndIntShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1731349266";
        var @default = 1044531917;

        // Act
        var result = value.ToInt32(@default);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToInt32WithStringAndIntWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.ToInt32(1372667223));
    }

    [TestMethod]
    public void ToInt64WithStringAndLongShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1225376942";
        var @default = 2049865442L;

        // Act
        var result = value.ToInt64(@default);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToInt64WithStringAndLongWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.ToInt64(1026527079L));
    }

    [TestMethod]
    public void ToByteWithStringShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1301364385";

        // Act
        var result = value.ToByte();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToByteWithStringWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.ToByte());
    }

    [TestMethod]
    public void ToInt16WithStringShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1333982102";

        // Act
        var result = value.ToInt16();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToInt16WithStringWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.ToInt16());
    }

    [TestMethod]
    public void ToInt32WithStringShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1633218336";

        // Act
        var result = value.ToInt32();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToInt32WithStringWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.ToInt32());
    }

    [TestMethod]
    public void ToInt64WithStringShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue638962681";

        // Act
        var result = value.ToInt64();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToInt64WithStringWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.ToInt64());
    }

    [TestMethod]
    public void ToEnumShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1962603018";
        var ignoreCase = false;

        // Act
        var result = value.ToEnum<T>(ignoreCase);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToEnumWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.ToEnum<T>(false));
    }

    [TestMethod]
    public void ToDateTimeWithStringShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1727733378";

        // Act
        var result = value.ToDateTime();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToDateTimeWithStringWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.ToDateTime());
    }

    [TestMethod]
    public void ToGuidShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1732880276";
        var format = "TestValue546037359";

        // Act
        var result = value.ToGuid(format);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToGuidWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.ToGuid("TestValue712134596"));
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToGuidWithInvalidFormatShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => "TestValue2062186659".ToGuid(value));
    }

    [TestMethod]
    public void ToDateTimeWithParttenShouldWorksCorrectly()
    {
        // Arrange
        var date = "TestValue311961279";
        var pattern = "TestValue357451564";
        var defaultValue = DateTime.UtcNow;

        // Act
        var result = date.ToDateTimeWithPartten(pattern, defaultValue);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToDateTimeWithParttenWithInvalidDateShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.ToDateTimeWithPartten("TestValue873459128", DateTime.UtcNow));
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToDateTimeWithParttenWithInvalidPatternShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => "TestValue1770518646".ToDateTimeWithPartten(value, DateTime.UtcNow));
    }

    [TestMethod]
    public void ToDateTimeWithDateAndDefaultValueShouldWorksCorrectly()
    {
        // Arrange
        var date = "TestValue742644436";
        var defaultValue = DateTime.UtcNow;

        // Act
        var result = date.ToDateTime(defaultValue);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToDateTimeWithDateAndDefaultValueWithInvalidDateShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.ToDateTime(DateTime.UtcNow));
    }

    [TestMethod]
    public void ToSafeFileNameShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue446600359";
        var replacement = 'D';

        // Act
        var result = value.ToSafeFileName(replacement);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToSafeFileNameWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.ToSafeFileName('d'));
    }

    [TestMethod]
    public void ToSafeFilePathShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue537327007";
        var replacement = 'T';

        // Act
        var result = value.ToSafeFilePath(replacement);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToSafeFilePathWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.ToSafeFilePath('F'));
    }

    [TestMethod]
    public void ToDecimalShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue727572388";
        var defaultValue = 779659530.21M;

        // Act
        var result = value.ToDecimal(defaultValue);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToDecimalWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.ToDecimal(1969866053.1M));
    }

    [TestMethod]
    public void ToDecimalPerformsMapping()
    {
        // Arrange
        var value = "TestValue2063803588";
        var defaultValue = 1762882891.11M;

        // Act
        var result = value.ToDecimal(defaultValue);

        // Assert
        Assert.AreEqual(value, result.Value);
    }

    [TestMethod]
    public void ToUTF8ShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1212323750";

        // Act
        var result = value.ToUTF8();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToUTF8WithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.ToUTF8());
    }

    [TestMethod]
    public void FirstCharToUpperShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1055803322";

        // Act
        var result = value.FirstCharToUpper();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void FirstCharToUpperWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.FirstCharToUpper());
    }

    [TestMethod]
    public void ReplaceSpecialSharactersShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1084260920";
        var replacement = 'Q';

        // Act
        var result = value.ReplaceSpecialSharacters(replacement);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ReplaceSpecialSharactersWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.ReplaceSpecialSharacters('h'));
    }

    [TestMethod]
    public void GetFullChinesePhoneticAlphabetShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1137580393";

        // Act
        var result = value.GetFullChinesePhoneticAlphabet();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void GetFullChinesePhoneticAlphabetWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.GetFullChinesePhoneticAlphabet());
    }

    [TestMethod]
    public void HtmlDecodeShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue990768283";

        // Act
        var result = value.HtmlDecode();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void HtmlDecodeWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.HtmlDecode());
    }

    [TestMethod]
    public void HtmlEncodeShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue640113371";

        // Act
        var result = value.HtmlEncode();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void HtmlEncodeWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.HtmlEncode());
    }

    [TestMethod]
    public void UrlDecodeShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue728410478";

        // Act
        var result = value.UrlDecode();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void UrlDecodeWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.UrlDecode());
    }

    [TestMethod]
    public void UrlEncodeShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue993145904";

        // Act
        var result = value.UrlEncode();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void UrlEncodeWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.UrlEncode());
    }

    [TestMethod]
    public void RemoveNewLinesShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue1145162865";

        // Act
        var result = value.RemoveNewLines();

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void RemoveNewLinesWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.RemoveNewLines());
    }

    [TestMethod]
    public void Base64DecodeShouldWorksCorrectly()
    {
        // Arrange
        var base64String = "TestValue1967676768";
        var encoding = Encoding.GetEncoding(780883123);

        // Act
        var result = base64String.Base64Decode(encoding);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void Base64DecodeWithInvalidBase64StringShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => value.Base64Decode(Encoding.GetEncoding(63987063)));
    }

    [TestMethod]
    public void Base64EncodeShouldWorksCorrectly()
    {
        // Arrange
        var value = "TestValue830906962";
        var encoding = Encoding.GetEncoding(2108314267);

        // Act
        var result = StringExtensions.Base64Encode(value, encoding);

        // Assert
        Assert.Fail("Create or modify test");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void Base64EncodeWithInvalidValueShouldThrow(string value)
    {
        Assert.ThrowsException<ArgumentNullException>(() => StringExtensions.Base64Encode(value, Encoding.GetEncoding(301887481)));
    }
    */
}
