// Copyright (c) IdeaTech. All rights reserved.

namespace IdeaTech.CandyBox.UnitTests.Diagnostics;

[TestClass]
public class VerifyTests
{
    #region NotNull

    [TestMethod]
    public void NotNullWithNullShouldThrow()
    {
        Assert.ThrowsException<ArgumentNullException>(() => Verify.NotNull(null));
    }

    [TestMethod]
    public void NotNullWithNonNullShouldNotThrow()
    {
        Verify.NotNull("hello");
    }

    #endregion

    #region NotNullOrWhiteSpace

    [TestMethod]
    public void NotNullOrWhiteSpaceWithNullShouldThrow()
    {
        Assert.ThrowsException<ArgumentNullException>(() => Verify.NotNullOrWhiteSpace(null));
    }

    [DataTestMethod]
    [DataRow("")]
    [DataRow("   ")]
    public void NotNullOrWhiteSpaceWithWhiteSpaceShouldThrow(string? value)
    {
        Assert.ThrowsException<ArgumentException>(() => Verify.NotNullOrWhiteSpace(value));
    }

    [TestMethod]
    public void NotNullOrWhiteSpaceWithValidValueShouldNotThrow()
    {
        Verify.NotNullOrWhiteSpace("hello");
    }

    #endregion

    #region NotNullOrEmpty

    [TestMethod]
    public void NotNullOrEmptyWithNullCollectionShouldThrow()
    {
        Assert.ThrowsException<ArgumentNullException>(() => Verify.NotNullOrEmpty<int>(null));
    }

    [TestMethod]
    public void NotNullOrEmptyWithEmptyCollectionShouldThrow()
    {
        Assert.ThrowsException<ArgumentException>(() => Verify.NotNullOrEmpty(Array.Empty<int>()));
    }

    [TestMethod]
    public void NotNullOrEmptyWithNonEmptyCollectionShouldNotThrow()
    {
        Verify.NotNullOrEmpty(new[] { 1, 2, 3 });
    }

    #endregion

    #region Between

    [TestMethod]
    public void BetweenWithValueInRangeShouldNotThrow()
    {
        Verify.Between(5, 1, 10);
    }

    [TestMethod]
    public void BetweenWithValueAtLowerBoundShouldNotThrow()
    {
        Verify.Between(1, 1, 10);
    }

    [TestMethod]
    public void BetweenWithValueAtUpperBoundShouldNotThrow()
    {
        Verify.Between(10, 1, 10);
    }

    [TestMethod]
    public void BetweenWithValueOutOfRangeShouldThrow()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Verify.Between(15, 1, 10));
    }

    #endregion

    #region True

    [TestMethod]
    public void TrueWithTrueConditionShouldNotThrow()
    {
        Verify.True(true, "Should not throw");
    }

    [TestMethod]
    public void TrueWithFalseConditionShouldThrow()
    {
        Assert.ThrowsException<ArgumentException>(() => Verify.True(false, "Condition failed"));
    }

    #endregion

    #region False

    [TestMethod]
    public void FalseWithFalseConditionShouldNotThrow()
    {
        Verify.False(false, "Should not throw");
    }

    [TestMethod]
    public void FalseWithTrueConditionShouldThrow()
    {
        Assert.ThrowsException<ArgumentException>(() => Verify.False(true, "Condition failed"));
    }

    #endregion

    #region ValidUrl

    [TestMethod]
    public void ValidUrlWithNullShouldThrow()
    {
        Assert.ThrowsException<ArgumentNullException>(() => Verify.ValidUrl(null!));
    }

    [DataTestMethod]
    [DataRow("")]
    [DataRow("   ")]
    public void ValidUrlWithWhiteSpaceShouldThrow(string? url)
    {
        Assert.ThrowsException<ArgumentException>(() => Verify.ValidUrl(url!));
    }

    [TestMethod]
    public void ValidUrlWithInvalidUrlShouldThrow()
    {
        Assert.ThrowsException<ArgumentException>(() => Verify.ValidUrl("not-a-url"));
    }

    [TestMethod]
    public void ValidUrlWithValidUrlShouldNotThrow()
    {
        Verify.ValidUrl("https://www.example.com");
    }

    [TestMethod]
    public void ValidUrlWithQueryStringAndNotAllowedShouldThrow()
    {
        Assert.ThrowsException<ArgumentException>(() => Verify.ValidUrl("https://example.com?q=1", allowQueryString: false));
    }

    [TestMethod]
    public void ValidUrlWithQueryStringAndAllowedShouldNotThrow()
    {
        Verify.ValidUrl("https://example.com?q=1", allowQueryString: true);
    }

    #endregion
}
