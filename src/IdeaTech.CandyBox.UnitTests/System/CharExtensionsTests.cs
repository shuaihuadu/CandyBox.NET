// Copyright (c) IdeaTech. All rights reserved.

namespace System.Extensions.UnitTests.System;

[TestClass]
public class CharExtensionsTests
{
    #region IsDigit

    [DataTestMethod]
    [DataRow('0')]
    [DataRow('5')]
    [DataRow('9')]
    public void IsDigitWithDigitCharShouldReturnTrue(char c)
    {
        Assert.IsTrue(c.IsDigit());
    }

    [DataTestMethod]
    [DataRow('a')]
    [DataRow('Z')]
    [DataRow('!')]
    [DataRow(' ')]
    public void IsDigitWithNonDigitCharShouldReturnFalse(char c)
    {
        Assert.IsFalse(c.IsDigit());
    }

    #endregion

    #region IsLower

    [DataTestMethod]
    [DataRow('a')]
    [DataRow('m')]
    [DataRow('z')]
    public void IsLowerWithLowerCaseCharShouldReturnTrue(char c)
    {
        Assert.IsTrue(c.IsLower());
    }

    [DataTestMethod]
    [DataRow('A')]
    [DataRow('Z')]
    [DataRow('0')]
    [DataRow('!')]
    public void IsLowerWithNonLowerCaseCharShouldReturnFalse(char c)
    {
        Assert.IsFalse(c.IsLower());
    }

    #endregion

    #region IsUpper

    [DataTestMethod]
    [DataRow('A')]
    [DataRow('M')]
    [DataRow('Z')]
    public void IsUpperWithUpperCaseCharShouldReturnTrue(char c)
    {
        Assert.IsTrue(c.IsUpper());
    }

    [DataTestMethod]
    [DataRow('a')]
    [DataRow('z')]
    [DataRow('0')]
    [DataRow('!')]
    public void IsUpperWithNonUpperCaseCharShouldReturnFalse(char c)
    {
        Assert.IsFalse(c.IsUpper());
    }

    #endregion

    #region IsLetterOrDigit

    [DataTestMethod]
    [DataRow('a')]
    [DataRow('Z')]
    [DataRow('0')]
    [DataRow('9')]
    public void IsLetterOrDigitWithLetterOrDigitCharShouldReturnTrue(char c)
    {
        Assert.IsTrue(c.IsLetterOrDigit());
    }

    [DataTestMethod]
    [DataRow('!')]
    [DataRow('@')]
    [DataRow(' ')]
    [DataRow('-')]
    public void IsLetterOrDigitWithSpecialCharShouldReturnFalse(char c)
    {
        Assert.IsFalse(c.IsLetterOrDigit());
    }

    #endregion
}
