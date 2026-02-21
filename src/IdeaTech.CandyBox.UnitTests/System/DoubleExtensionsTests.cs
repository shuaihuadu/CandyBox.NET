// Copyright (c) IdeaTech. All rights reserved.

namespace System.Extensions.UnitTests.System;

[TestClass]
public class DoubleExtensionsTests
{
    #region ToDecimal

    [TestMethod]
    public void ToDecimalShouldConvertDoubleToDecimal()
    {
        Assert.AreEqual(3.14m, ((double)3.14).ToDecimal());
    }

    [TestMethod]
    public void ToDecimalWithZeroShouldReturnZero()
    {
        Assert.AreEqual(0m, ((double)0).ToDecimal());
    }

    #endregion

    #region ToFileSizeString

    [TestMethod]
    public void ToFileSizeStringWithBytesShouldReturnBytesString()
    {
        Assert.AreEqual("512 bytes", ((double)512).ToFileSizeString());
    }

    [TestMethod]
    public void ToFileSizeStringWithKilobytesShouldReturnKBString()
    {
        Assert.AreEqual("1KB", ((double)1024).ToFileSizeString());
    }

    [TestMethod]
    public void ToFileSizeStringWithMegabytesShouldReturnMBString()
    {
        Assert.AreEqual("1MB", ((double)(1024 * 1024)).ToFileSizeString());
    }

    [TestMethod]
    public void ToFileSizeStringWithNegativeShouldThrow()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => ((double)-1).ToFileSizeString());
    }

    #endregion
}
