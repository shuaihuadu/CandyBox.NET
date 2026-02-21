// Copyright (c) IdeaTech. All rights reserved.

namespace System.Data;

/// <summary>
/// The <see cref="DataTable"/> extensions.
/// </summary>
public static class DataTableExtensions
{
    /// <summary>
    /// Convert the <paramref name="dataTable"/> to markdown table.
    /// </summary>
    /// <param name="dataTable">The datatable.</param>
    /// <param name="alignment">The markdown table content alignment.</param>
    /// <returns>The markdown table string.</returns>
    public static string ToMarkdownTable(this DataTable dataTable, MarkdownTableContentAlignment alignment = MarkdownTableContentAlignment.Default)
    {
        ArgumentNullException.ThrowIfNull(dataTable);

        if (dataTable.Rows.IsNullOrEmpty() || dataTable.Columns.IsNullOrEmpty())
        {
            return string.Empty;
        }

        StringBuilder markdownTableBuilder = new();

        for (int i = 0; i < dataTable.Columns.Count; i++)
        {
            markdownTableBuilder.Append($"|{dataTable.Columns[i].ColumnName}");
        }

        markdownTableBuilder.Append('|');
        markdownTableBuilder.AppendLine();

        markdownTableBuilder.AppendLine(BuildMarkdownHeaderBodySeparator(dataTable.Columns.Count, alignment));

        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                markdownTableBuilder.Append('|');
                markdownTableBuilder.Append(dataTable.Rows[i][j].ToString());
            }

            markdownTableBuilder.Append('|');
            markdownTableBuilder.AppendLine();
        }

        return markdownTableBuilder.ToString();
    }

    private static string BuildMarkdownHeaderBodySeparator(int cellCount, MarkdownTableContentAlignment alignment = MarkdownTableContentAlignment.Default)
    {
        StringBuilder separatorBuilder = new("|");

        for (int i = 0; i < cellCount; i++)
        {
            string defaultSeparator = "---";

            string separator = alignment switch
            {
                MarkdownTableContentAlignment.Center => $":{defaultSeparator}:",
                MarkdownTableContentAlignment.Right => $"{defaultSeparator}:",
                MarkdownTableContentAlignment.Left => $":{defaultSeparator}",
                _ => defaultSeparator
            };

            separatorBuilder.Append(separator);
            separatorBuilder.Append('|');
        }

        return separatorBuilder.ToString();
    }
}
