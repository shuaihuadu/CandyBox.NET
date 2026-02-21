// Copyright (c) IdeaTech. All rights reserved.

namespace System.Extensions.UnitTests.System;

[TestClass]
public class TypeExtensionsTests
{
    private sealed class SampleModel
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public DateTime? BirthDate { get; set; }
    }

    #region GetSchema

    [TestMethod]
    public void GetSchemaWithNullTypeShouldThrow()
    {
        Assert.ThrowsException<ArgumentNullException>(() => ((Type?)null)!.GetSchema());
    }

    [TestMethod]
    public void GetSchemaShouldReturnTableWithTypeName()
    {
        DataTable table = typeof(SampleModel).GetSchema();
        Assert.AreEqual(nameof(SampleModel), table.TableName);
    }

    [TestMethod]
    public void GetSchemaShouldCreateColumnsForAllProperties()
    {
        DataTable table = typeof(SampleModel).GetSchema();
        Assert.IsTrue(table.Columns.Contains(nameof(SampleModel.Name)));
        Assert.IsTrue(table.Columns.Contains(nameof(SampleModel.Age)));
        Assert.IsTrue(table.Columns.Contains(nameof(SampleModel.BirthDate)));
    }

    [TestMethod]
    public void GetSchemaShouldUnwrapNullableTypes()
    {
        DataTable table = typeof(SampleModel).GetSchema();
        Assert.AreEqual(typeof(DateTime), table.Columns[nameof(SampleModel.BirthDate)]!.DataType);
    }

    [TestMethod]
    public void GetSchemaShouldReturnEmptyRows()
    {
        DataTable table = typeof(SampleModel).GetSchema();
        Assert.AreEqual(0, table.Rows.Count);
    }

    #endregion
}
