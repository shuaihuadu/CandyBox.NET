// Copyright (c) IdeaTech. All rights reserved.

namespace System.Extensions.UnitTests;

[TestClass]
public class BooleanExtensionsTests
{
    [TestMethod]
    public void ToTrueOrFalseStringShouldWorksCorrectly()
    {
        bool value = true;

        string result = value.ToTrueOrFalseString();
        Assert.AreEqual("Yes", result);

        value = false;
        result = value.ToTrueOrFalseString();
        Assert.AreEqual("No", result);

        value = true;
        result = value.ToTrueOrFalseString("Enable", "Disable");
        Assert.AreEqual("Enable", result);

        value = false;
        result = value.ToTrueOrFalseString("Enable", "Disable");
        Assert.AreEqual("Disable", result);
    }

    [TestMethod]
    public void ThrowArgumentExceptionIfTrueShouldWorksCorrectly()
    {
        false.ThrowArgumentExceptionIfTrue("value");

        Assert.ThrowsException<ArgumentException>(() => true.ThrowArgumentExceptionIfTrue("value"));
    }

    [TestMethod]
    public void ThrowArgumentNullExceptionIfTrueShouldWorksCorrectly()
    {
        false.ThrowArgumentNullExceptionIfTrue("value");

        Assert.ThrowsException<ArgumentNullException>(() => true.ThrowArgumentNullExceptionIfTrue("value"));
    }

    [TestMethod]
    public void ThrowArgumentExceptionIfFalseShouldWorksCorrectly()
    {
        true.ThrowArgumentExceptionIfFalse("value");

        Assert.ThrowsException<ArgumentException>(() => false.ThrowArgumentExceptionIfFalse("value"));
    }

    [TestMethod]
    public void ThrowArgumentNullExceptionIfFalseShouldWorksCorrectly()
    {
        true.ThrowArgumentNullExceptionIfFalse("value");

        Assert.ThrowsException<ArgumentNullException>(() => false.ThrowArgumentNullExceptionIfFalse("value"));
    }

    [TestMethod]
    public void ThrowIfTrueShouldWorksCorrectly()
    {
        InvalidOperationException exception = new("Invalid operation");

        false.ThrowIfTrue(exception);

        InvalidOperationException result = Assert.ThrowsException<InvalidOperationException>(() => true.ThrowIfTrue(exception));

        Assert.AreEqual(exception.Message, result.Message);
    }

    [TestMethod]
    public void ThrowIfFlaseShouldWorksCorrectly()
    {
        NotSupportedException exception = new("Not Supported");

        true.ThrowIfFlase(exception);

        NotSupportedException result = Assert.ThrowsException<NotSupportedException>(() => false.ThrowIfFlase(exception));

        Assert.AreEqual(exception.Message, result.Message);
    }
}
