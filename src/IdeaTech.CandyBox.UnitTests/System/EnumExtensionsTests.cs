// Copyright (c) IdeaTech. All rights reserved.

namespace System.Extensions.UnitTests.System;

[TestClass]
public class EnumExtensionsTests
{
    #region IntValue

    [DataTestMethod]
    [DataRow(DayOfWeek.Sunday, 0)]
    [DataRow(DayOfWeek.Monday, 1)]
    [DataRow(DayOfWeek.Saturday, 6)]
    public void IntValueShouldReturnCorrectInt(DayOfWeek day, int expected)
    {
        Assert.AreEqual(expected, day.IntValue());
    }

    #endregion

    #region ByteValue

    [DataTestMethod]
    [DataRow(DayOfWeek.Sunday, (byte)0)]
    [DataRow(DayOfWeek.Monday, (byte)1)]
    [DataRow(DayOfWeek.Saturday, (byte)6)]
    public void ByteValueShouldReturnCorrectByte(DayOfWeek day, byte expected)
    {
        Assert.AreEqual(expected, day.ByteValue());
    }

    #endregion

    #region Equals(int)

    [TestMethod]
    public void EqualsIntWithMatchingValueShouldReturnTrue()
    {
        Assert.IsTrue(EnumExtensions.Equals(DayOfWeek.Monday, 1));
    }

    [TestMethod]
    public void EqualsIntWithNonMatchingValueShouldReturnFalse()
    {
        Assert.IsFalse(EnumExtensions.Equals(DayOfWeek.Monday, 2));
    }

    #endregion

    #region Equals(byte)

    [TestMethod]
    public void EqualsByteWithMatchingValueShouldReturnTrue()
    {
        Assert.IsTrue(EnumExtensions.Equals(DayOfWeek.Monday, (byte)1));
    }

    [TestMethod]
    public void EqualsByteWithNonMatchingValueShouldReturnFalse()
    {
        Assert.IsFalse(EnumExtensions.Equals(DayOfWeek.Monday, (byte)3));
    }

    #endregion

    #region Equals(string)

    [TestMethod]
    public void EqualsStringWithMatchingNameShouldReturnTrue()
    {
        Assert.IsTrue(EnumExtensions.Equals(DayOfWeek.Monday, "Monday"));
    }

    [TestMethod]
    public void EqualsStringWithMatchingNameIgnoreCaseShouldReturnTrue()
    {
        Assert.IsTrue(EnumExtensions.Equals(DayOfWeek.Monday, "monday"));
    }

    [TestMethod]
    public void EqualsStringWithNonMatchingNameShouldReturnFalse()
    {
        Assert.IsFalse(EnumExtensions.Equals(DayOfWeek.Monday, "Friday"));
    }

    #endregion
}
