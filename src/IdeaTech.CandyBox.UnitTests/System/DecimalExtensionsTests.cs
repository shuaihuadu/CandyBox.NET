// Copyright (c) IdeaTech. All rights reserved.

namespace System.Extensions.UnitTests.System;

[TestClass]
public class DecimalExtensionsTests
{
    #region ToFileSizeString

    [TestMethod]
    public void ToFileSizeStringWithNegativeShouldThrow()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => (-1m).ToFileSizeString());
    }

    [TestMethod]
    public void ToFileSizeStringWithZeroShouldReturnBytes()
    {
        Assert.AreEqual("0 bytes", 0m.ToFileSizeString());
    }

    [TestMethod]
    public void ToFileSizeStringWithBytesShouldReturnBytesString()
    {
        Assert.AreEqual("512 bytes", 512m.ToFileSizeString());
    }

    [TestMethod]
    public void ToFileSizeStringWithKilobytesShouldReturnKBString()
    {
        Assert.AreEqual("1KB", 1024m.ToFileSizeString());
    }

    [TestMethod]
    public void ToFileSizeStringWithMegabytesShouldReturnMBString()
    {
        Assert.AreEqual("1MB", (1024m * 1024m).ToFileSizeString());
    }

    [TestMethod]
    public void ToFileSizeStringWithGigabytesShouldReturnGBString()
    {
        Assert.AreEqual("1GB", (1024m * 1024m * 1024m).ToFileSizeString());
    }

    [TestMethod]
    public void ToFileSizeStringWithTerabytesShouldReturnTBString()
    {
        Assert.AreEqual("1TB", (1024m * 1024m * 1024m * 1024m).ToFileSizeString());
    }

    [TestMethod]
    public void ToFileSizeStringWithPetabytesShouldReturnPBString()
    {
        Assert.AreEqual("1PB", (1024m * 1024m * 1024m * 1024m * 1024m).ToFileSizeString());
    }

    [TestMethod]
    public void ToFileSizeStringWithExabytesShouldReturnEBString()
    {
        Assert.AreEqual("1EB", (1024m * 1024m * 1024m * 1024m * 1024m * 1024m).ToFileSizeString());
    }

    #endregion
}
