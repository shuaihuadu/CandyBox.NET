﻿// Copyright (c) IdeaTech. All rights reserved.

namespace System.Collections.Generic;

/// <summary>
/// Common extensions of <see cref="IEnumerable{T}"/>.
/// </summary>
public static class IEnumerableExtensions
{
    /// <summary>
    /// Creates a new <see cref="IEnumerable{T}"/> that is a copy of the current instance.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="collection">The collection to clone.</param>
    /// <returns>A new object that is a copy of <see cref="IEnumerable{T}"/>.</returns>
    public static IEnumerable<T> Clone<T>(this IEnumerable<T>? collection) where T : ICloneable
    {
        return collection is null ? throw new ArgumentNullException(nameof(collection)) : collection.Select(item => (T)item.Clone());
    }

    /// <summary>
    /// Indicates whether the specified collection is null or an empty collection.
    /// </summary>
    /// <param name="collection">The collection.</param>
    /// <returns>true if the collection is null or an empty collection; otherwise, false.</returns>
    public static bool IsNullOrEmpty(this IEnumerable? collection)
    {
        if (collection == null)
        {
            return true;
        }

        return !collection.GetEnumerator().MoveNext();
    }

    /// <summary>
    /// Indicates whether the specified collection is null or an empty collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="collection">The collection.</param>
    /// <returns>true if the collection is null or an empty collection; otherwise, false.</returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? collection)
    {
        return collection == null || !collection.Any();
    }

    /// <summary>
    /// Indicates whether the specified collection is not null or not an empty collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="collection">The collection.</param>
    /// <returns>true if the collection is not null or not an empty collection; otherwise, false.</returns>
    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T>? collection)
    {
        return !collection.IsNullOrEmpty();
    }

    /// <summary>
    /// Indicates whether the specified collection is not null or not an empty collection.
    /// </summary>
    /// <param name="collection">The collection.</param>
    /// <returns>true if the collection is not null or not an empty collection; otherwise, false.</returns>
    public static bool IsNotNullOrEmpty(this IEnumerable? collection)
    {
        return !collection.IsNullOrEmpty();
    }

    /// <summary>
    /// Get an empty collection If <paramref name="collection"/> is null.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="collection">The collection.</param>
    /// <returns>The <paramref name="collection"/>,if <paramref name="collection"/>is null return an empty collection.</returns>
    public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> collection)
    {
        return collection ?? [];
    }

    /// <summary>
    /// Convert the <see cref="IEnumerable{T}"/> to a DataTable.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="collection">The collection to convert.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>The converted datatable.</returns>
    [Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S3267:Loops should be simplified with \"LINQ\" expressions", Justification = "Make the code harder to read.")]
    [Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1255:Simplify argument null check", Justification = "Does not support .net standard 2.0.")]
    public static DataTable ToDataTable<T>(this IEnumerable<T> collection) where T : class
    {
        if (collection is null)
        {
            throw new ArgumentNullException(nameof(collection));
        }

        Type entityType = typeof(T);

        DataTable dataTable = entityType.GetSchema();

        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

        foreach (T item in collection)
        {
            DataRow row = dataTable.NewRow();

            foreach (PropertyDescriptor prop in properties)
            {
                if (dataTable.Columns.Contains(prop.Name))
                {
                    row[prop.Name] = prop.GetValue(item);
                }
            }

            dataTable.Rows.Add(row);
        }

        return dataTable;
    }
}
