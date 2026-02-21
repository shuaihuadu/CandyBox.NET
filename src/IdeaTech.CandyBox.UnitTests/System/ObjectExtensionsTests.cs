// Copyright (c) IdeaTech. All rights reserved.

namespace System.Extensions.UnitTests.System;

[TestClass]
public class ObjectExtensionsTests
{
    private sealed class SampleEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int Age { get; set; }
    }

    #region IsNull / IsNotNull

    [TestMethod]
    public void IsNullWithNullShouldReturnTrue()
    {
        string? value = null;
        Assert.IsTrue(value.IsNull());
    }

    [TestMethod]
    public void IsNullWithNonNullShouldReturnFalse()
    {
        Assert.IsFalse("hello".IsNull());
    }

    [TestMethod]
    public void IsNotNullWithNonNullShouldReturnTrue()
    {
        Assert.IsTrue("hello".IsNotNull());
    }

    [TestMethod]
    public void IsNotNullWithNullShouldReturnFalse()
    {
        string? value = null;
        Assert.IsFalse(value.IsNotNull());
    }

    #endregion

    #region Is / IsNot

    [TestMethod]
    public void IsWithMatchingTypeShouldReturnTrue()
    {
        object obj = "hello";
        Assert.IsTrue(obj.Is<string>());
    }

    [TestMethod]
    public void IsWithNonMatchingTypeShouldReturnFalse()
    {
        object obj = 42;
        Assert.IsFalse(obj.Is<string>());
    }

    [TestMethod]
    public void IsNotWithNonMatchingTypeShouldReturnTrue()
    {
        object obj = 42;
        Assert.IsTrue(obj.IsNot<string>());
    }

    [TestMethod]
    public void IsNotWithMatchingTypeShouldReturnFalse()
    {
        object obj = "hello";
        Assert.IsFalse(obj.IsNot<string>());
    }

    #endregion

    #region As

    [TestMethod]
    public void AsWithCompatibleTypeShouldReturnCasted()
    {
        object obj = "hello";
        Assert.AreEqual("hello", obj.As<string>());
    }

    [TestMethod]
    public void AsWithIncompatibleTypeShouldReturnNull()
    {
        object obj = 42;
        Assert.IsNull(obj.As<string>());
    }

    #endregion

    #region In

    [TestMethod]
    public void InWithItemInListShouldReturnTrue()
    {
        Assert.IsTrue(2.In(1, 2, 3));
    }

    [TestMethod]
    public void InWithItemNotInListShouldReturnFalse()
    {
        Assert.IsFalse(5.In(1, 2, 3));
    }

    [TestMethod]
    public void InWithEmptyListShouldThrow()
    {
        Assert.ThrowsException<ArgumentNullException>(() => 1.In(Array.Empty<int>()));
    }

    #endregion

    #region Between

    [TestMethod]
    public void BetweenWithValueInRangeShouldReturnTrue()
    {
        Assert.IsTrue(5.Between(1, 10));
    }

    [TestMethod]
    public void BetweenWithValueAtLowerBoundShouldReturnTrue()
    {
        Assert.IsTrue(1.Between(1, 10));
    }

    [TestMethod]
    public void BetweenWithValueAtUpperBoundShouldReturnTrue()
    {
        Assert.IsTrue(10.Between(1, 10));
    }

    [TestMethod]
    public void BetweenWithValueOutOfRangeShouldReturnFalse()
    {
        Assert.IsFalse(15.Between(1, 10));
    }

    #endregion

    #region With

    [TestMethod]
    public void WithShouldInvokeAction()
    {
        string? captured = null;
        "hello".With(v => captured = v);
        Assert.AreEqual("hello", captured);
    }

    #endregion

    #region ToDataTable

    [TestMethod]
    public void ToDataTableShouldCreateTableWithTypeNameAndColumns()
    {
        SampleEntity entity = new() { Name = "Alice", Age = 30 };
        DataTable table = entity.ToDataTable();
        Assert.AreEqual(nameof(SampleEntity), table.TableName);
        Assert.IsTrue(table.Columns.Contains(nameof(SampleEntity.Name)));
        Assert.IsTrue(table.Columns.Contains(nameof(SampleEntity.Age)));
    }

    #endregion

    #region HiddenPropertiesValue

    [TestMethod]
    public void HiddenPropertiesValueShouldNullifySpecifiedProperties()
    {
        SampleEntity entity = new() { Name = "Alice", Email = "alice@example.com", Age = 30 };
        entity.HiddenPropertiesValue(false, nameof(SampleEntity.Email));
        Assert.IsNull(entity.Email);
        Assert.AreEqual("Alice", entity.Name);
    }

    [TestMethod]
    public void HiddenPropertiesValueWithReverseShouldNullifyNonSpecifiedProperties()
    {
        SampleEntity entity = new() { Name = "Alice", Email = "alice@example.com" };
        entity.HiddenPropertiesValue(true, nameof(SampleEntity.Name));
        Assert.IsNull(entity.Email);
        Assert.AreEqual("Alice", entity.Name);
    }

    [TestMethod]
    public void HiddenPropertiesValueWithNullObjectShouldReturnNull()
    {
        SampleEntity? entity = null;
        SampleEntity? result = entity.HiddenPropertiesValue(false, "Name");
        Assert.IsNull(result);
    }

    #endregion

    #region SafeTrimStringProperties

    [TestMethod]
    public void SafeTrimStringPropertiesShouldTrimAllStringProperties()
    {
        SampleEntity entity = new() { Name = "  Alice  ", Email = "  alice@example.com  " };
        entity.SafeTrimStringProperties();
        Assert.AreEqual("Alice", entity.Name);
        Assert.AreEqual("alice@example.com", entity.Email);
    }

    [TestMethod]
    public void SafeTrimStringPropertiesWithSpecificPropertiesShouldTrimOnlyThose()
    {
        SampleEntity entity = new() { Name = "  Alice  ", Email = "  alice@example.com  " };
        entity.SafeTrimStringProperties(nameof(SampleEntity.Name));
        Assert.AreEqual("Alice", entity.Name);
        Assert.AreEqual("  alice@example.com  ", entity.Email);
    }

    [TestMethod]
    public void SafeTrimStringPropertiesWithNullObjectShouldReturnNull()
    {
        SampleEntity? entity = null;
        SampleEntity? result = entity.SafeTrimStringProperties();
        Assert.IsNull(result);
    }

    [TestMethod]
    public void SafeTrimStringPropertiesWithNullPropertyValueShouldSetEmpty()
    {
        SampleEntity entity = new() { Name = null };
        entity.SafeTrimStringProperties();
        Assert.AreEqual(string.Empty, entity.Name);
    }

    #endregion
}
