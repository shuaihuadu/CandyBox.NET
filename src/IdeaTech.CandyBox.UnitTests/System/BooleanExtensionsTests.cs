// Copyright (c) IdeaTech. All rights reserved.

namespace System.Extensions.UnitTests.System;

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
}
