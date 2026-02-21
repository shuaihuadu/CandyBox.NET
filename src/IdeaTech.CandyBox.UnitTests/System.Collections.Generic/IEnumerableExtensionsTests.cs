// Copyright (c) IdeaTech. All rights reserved.

namespace System.Extensions.UnitTests.System.Collections.Generic;

[TestClass]
public class IEnumerableExtensionsTests
{
    private sealed class CloneableItem : ICloneable
    {
        public int Value { get; init; }
        public object Clone() => new CloneableItem { Value = this.Value };
    }

    private sealed class SampleItem
    {
        public string? Name { get; set; }
        public int Age { get; set; }
    }

    #region Clone

    [TestMethod]
    public void CloneWithNullCollectionShouldThrow()
    {
        IEnumerable<CloneableItem>? collection = null;
        Assert.ThrowsException<ArgumentNullException>(() => collection.Clone().ToList());
    }

    [TestMethod]
    public void CloneShouldReturnNewCollectionWithSameValues()
    {
        List<CloneableItem> original = [new CloneableItem { Value = 1 }, new CloneableItem { Value = 2 }];
        List<CloneableItem> cloned = original.Clone().ToList();
        Assert.AreEqual(original.Count, cloned.Count);
        Assert.AreEqual(original[0].Value, cloned[0].Value);
        Assert.AreNotSame(original[0], cloned[0]);
    }

    #endregion

    #region IsNullOrEmpty

    [TestMethod]
    public void IsNullOrEmptyWithNullCollectionShouldReturnTrue()
    {
        IEnumerable<string>? collection = null;
        Assert.IsTrue(collection.IsNullOrEmpty());
    }

    [TestMethod]
    public void IsNullOrEmptyWithEmptyCollectionShouldReturnTrue()
    {
        Assert.IsTrue(Array.Empty<string>().IsNullOrEmpty());
    }

    [TestMethod]
    public void IsNullOrEmptyWithNonEmptyCollectionShouldReturnFalse()
    {
        Assert.IsFalse(new[] { "a" }.IsNullOrEmpty());
    }

    [TestMethod]
    public void IsNullOrEmptyNonGenericWithNullShouldReturnTrue()
    {
        global::System.Collections.IEnumerable? collection = null;
        Assert.IsTrue(collection.IsNullOrEmpty());
    }

    [TestMethod]
    public void IsNullOrEmptyNonGenericWithEmptyCollectionShouldReturnTrue()
    {
        Assert.IsTrue(new global::System.Collections.ArrayList().IsNullOrEmpty());
    }

    [TestMethod]
    public void IsNullOrEmptyNonGenericWithNonEmptyCollectionShouldReturnFalse()
    {
        global::System.Collections.ArrayList list = ["item"];
        Assert.IsFalse(list.IsNullOrEmpty());
    }

    #endregion

    #region IsNotNullOrEmpty

    [TestMethod]
    public void IsNotNullOrEmptyWithNonEmptyCollectionShouldReturnTrue()
    {
        Assert.IsTrue(new[] { 1, 2 }.IsNotNullOrEmpty());
    }

    [TestMethod]
    public void IsNotNullOrEmptyWithNullCollectionShouldReturnFalse()
    {
        IEnumerable<int>? collection = null;
        Assert.IsFalse(collection.IsNotNullOrEmpty());
    }

    [TestMethod]
    public void IsNotNullOrEmptyNonGenericWithNonEmptyCollectionShouldReturnTrue()
    {
        global::System.Collections.ArrayList list = ["item"];
        Assert.IsTrue(list.IsNotNullOrEmpty());
    }

    #endregion

    #region EmptyIfNull

    [TestMethod]
    public void EmptyIfNullWithNullCollectionShouldReturnEmptyEnumerable()
    {
        IEnumerable<string> result = ((IEnumerable<string>?)null!).EmptyIfNull();
        Assert.IsFalse(result.Any());
    }

    [TestMethod]
    public void EmptyIfNullWithNonNullCollectionShouldReturnSameCollection()
    {
        IEnumerable<string> original = ["a", "b"];
        IEnumerable<string> result = original.EmptyIfNull();
        Assert.AreSame(original, result);
    }

    #endregion

    #region ToDataTable

    [TestMethod]
    public void ToDataTableWithNullCollectionShouldThrow()
    {
        Assert.ThrowsException<ArgumentNullException>(() => IEnumerableExtensions.ToDataTable<SampleItem>(null!));
    }

    [TestMethod]
    public void ToDataTableShouldReturnTableWithCorrectSchema()
    {
        List<SampleItem> collection = [new SampleItem { Name = "Alice", Age = 30 }];
        DataTable table = IEnumerableExtensions.ToDataTable(collection);
        Assert.IsTrue(table.Columns.Contains(nameof(SampleItem.Name)));
        Assert.IsTrue(table.Columns.Contains(nameof(SampleItem.Age)));
    }

    [TestMethod]
    public void ToDataTableShouldReturnTableWithCorrectRowCount()
    {
        List<SampleItem> collection = [new SampleItem { Name = "Alice", Age = 30 }, new SampleItem { Name = "Bob", Age = 25 }];
        DataTable table = IEnumerableExtensions.ToDataTable(collection);
        Assert.AreEqual(2, table.Rows.Count);
    }

    [TestMethod]
    public void ToDataTableWithEmptyCollectionShouldReturnEmptyTable()
    {
        DataTable table = IEnumerableExtensions.ToDataTable(new List<SampleItem>());
        Assert.AreEqual(0, table.Rows.Count);
    }

    #endregion
}
