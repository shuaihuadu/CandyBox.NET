// Copyright (c) IdeaTech. All rights reserved.

namespace System.Extensions.UnitTests.System;

[TestClass]
public class Int32ExtensionsTests
{
    #region ToEnum

    [DataTestMethod]
    [DataRow(0, DayOfWeek.Sunday)]
    [DataRow(1, DayOfWeek.Monday)]
    [DataRow(6, DayOfWeek.Saturday)]
    public void ToEnumShouldConvertIntToEnum(int value, DayOfWeek expected)
    {
        Assert.AreEqual(expected, value.ToEnum<DayOfWeek>());
    }

    #endregion

    #region ToFileSizeString

    [TestMethod]
    public void ToFileSizeStringWithBytesShouldReturnBytesString()
    {
        Assert.AreEqual("512 bytes", 512.ToFileSizeString());
    }

    [TestMethod]
    public void ToFileSizeStringWithKilobytesShouldReturnKBString()
    {
        Assert.AreEqual("1KB", 1024.ToFileSizeString());
    }

    [TestMethod]
    public void ToFileSizeStringWithMegabytesShouldReturnMBString()
    {
        Assert.AreEqual("1MB", (1024 * 1024).ToFileSizeString());
    }

    [TestMethod]
    public void ToFileSizeStringWithNegativeShouldThrow()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => (-1).ToFileSizeString());
    }

    #endregion
}
