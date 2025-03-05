// Copyright (c) IdeaTech. All rights reserved.

namespace System.Extensions.UnitTests.System.Data;

[TestClass]
public class DataTableExtensionsTests
{
    [DataTestMethod]
    [DataRow(MarkdownTableContentAlignment.Default)]
    [DataRow(MarkdownTableContentAlignment.Left)]
    [DataRow(MarkdownTableContentAlignment.Center)]
    [DataRow(MarkdownTableContentAlignment.Right)]
    public void ToMarkdownTableShouldWorksCorrectly(MarkdownTableContentAlignment alignment)
    {
        using DataTable table = CreateTestDataTable();

        string markdownTable = table.ToMarkdownTable(alignment);

        Assert.IsFalse(markdownTable.IsNullOrEmpty());
    }

    [TestMethod]
    public void ToMarkdownTableWithEmptyDataTableShouldReturnEmpty()
    {
        using DataTable table = new();

        string markdownTable = table.ToMarkdownTable();

        Assert.IsTrue(markdownTable.IsNullOrEmpty());
    }

    private static DataTable CreateTestDataTable()
    {
        DataTable dataTable = new();

        for (int i = 1; i < 4; i++)
        {
            dataTable.Columns.Add($"Column{i}");
        }

        for (int i = 1; i < 6; i++)
        {
            DataRow row = dataTable.NewRow();

            row["Column1"] = i;
            row["Column2"] = Guid.NewGuid().ToString("n");
            row["Column3"] = null;

            dataTable.Rows.Add(row);
        }

        return dataTable;
    }
}
