// Copyright (c) IdeaTech. All rights reserved.

namespace System.Extensions.UnitTests.System;

[TestClass]
public class ByteExtensionsTests
{
    #region ToEnum

    [TestMethod]
    public void ToEnumShouldConvertByteToEnum()
    {
        byte value = (byte)DayOfWeek.Monday;
        Assert.AreEqual(DayOfWeek.Monday, value.ToEnum<DayOfWeek>());
    }

    [TestMethod]
    public void ToEnumWithZeroShouldReturnFirstEnumValue()
    {
        byte value = 0;
        Assert.AreEqual(DayOfWeek.Sunday, value.ToEnum<DayOfWeek>());
    }

    [DataTestMethod]
    [DataRow((byte)0, DayOfWeek.Sunday)]
    [DataRow((byte)1, DayOfWeek.Monday)]
    [DataRow((byte)6, DayOfWeek.Saturday)]
    public void ToEnumShouldMapAllDaysOfWeek(byte value, DayOfWeek expected)
    {
        Assert.AreEqual(expected, value.ToEnum<DayOfWeek>());
    }

    #endregion
}
