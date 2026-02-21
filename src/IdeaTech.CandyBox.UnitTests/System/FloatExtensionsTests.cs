// Copyright (c) IdeaTech. All rights reserved.

namespace System.Extensions.UnitTests.System;

[TestClass]
public class FloatExtensionsTests
{
    #region ToDecimal

    [TestMethod]
    public void ToDecimalShouldConvertFloatToDecimal()
    {
        Assert.AreEqual((decimal)(float)3.14, ((float)3.14).ToDecimal());
    }

    [TestMethod]
    public void ToDecimalWithZeroShouldReturnZero()
    {
        Assert.AreEqual(0m, ((float)0).ToDecimal());
    }

    #endregion

    #region ToFileSizeString

    [TestMethod]
    public void ToFileSizeStringWithBytesShouldReturnBytesString()
    {
        Assert.AreEqual("512 bytes", ((float)512).ToFileSizeString());
    }

    [TestMethod]
    public void ToFileSizeStringWithKilobytesShouldReturnKBString()
    {
        Assert.AreEqual("1KB", ((float)1024).ToFileSizeString());
    }

    [TestMethod]
    public void ToFileSizeStringWithMegabytesShouldReturnMBString()
    {
        Assert.AreEqual("1MB", ((float)(1024 * 1024)).ToFileSizeString());
    }

    [TestMethod]
    public void ToFileSizeStringWithNegativeShouldThrow()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => ((float)-1).ToFileSizeString());
    }

    #endregion
}
