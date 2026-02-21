// Copyright (c) IdeaTech. All rights reserved.

namespace System.Extensions.UnitTests.System.Collections.Generic;

[TestClass]
public class ICollectionExtensionsTests
{
    #region AddRange

    [TestMethod]
    public void AddRangeWithNullCollectionShouldThrow()
    {
        ICollection<int>? collection = null;
        Assert.ThrowsException<ArgumentNullException>(() => collection!.AddRange(1, 2, 3));
    }

    [TestMethod]
    public void AddRangeWithNullElementsShouldThrow()
    {
        ICollection<int> collection = new List<int>();
        Assert.ThrowsException<ArgumentNullException>(() => collection.AddRange(null!));
    }

    [TestMethod]
    public void AddRangeShouldAddAllElementsToCollection()
    {
        List<int> collection = [];
        collection.AddRange(1, 2, 3);
        Assert.AreEqual(3, collection.Count);
        CollectionAssert.Contains(collection, 1);
        CollectionAssert.Contains(collection, 2);
        CollectionAssert.Contains(collection, 3);
    }

    [TestMethod]
    public void AddRangeWithEmptyArrayShouldNotModifyCollection()
    {
        List<int> collection = [10, 20];
        collection.AddRange();
        Assert.AreEqual(2, collection.Count);
    }

    [TestMethod]
    public void AddRangeWithStringsShouldAddAll()
    {
        List<string> collection = [];
        collection.AddRange("a", "b", "c");
        Assert.AreEqual(3, collection.Count);
        CollectionAssert.Contains(collection, "a");
    }

    #endregion
}
