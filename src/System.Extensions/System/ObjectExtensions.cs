// Copyright (c) IdeaTech. All rights reserved.

namespace System;

/// <summary>
/// Common extensions of <see cref="object"/>.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Indicates whether the specified object is null.
    /// </summary>
    /// <param name="obj">The object to test.</param>
    /// <returns>true if the object is null;otherwise, false.</returns>
    public static bool IsNull<T>(this T? obj) where T : class
    {
        return obj is null;
    }

    /// <summary>
    /// Indicates whether the specified object is not null.
    /// </summary>
    /// <param name="obj">The object to test.</param>
    /// <returns>true if the object is not null;otherwise, false.</returns>
    public static bool IsNotNull<T>(this T? obj) where T : class
    {
        return obj is not null;
    }

    /// <summary>
    /// Indicates whether the specified object is <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of expected.</typeparam>
    /// <param name="obj">The object to test.</param>
    /// <returns>true if the object is <typeparamref name="T"/>;otherwise, false.</returns>
    public static bool Is<T>(this object obj) where T : class
    {
        return obj is T;
    }

    /// <summary>
    /// Indicates whether the specified object is not <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of expected.</typeparam>
    /// <param name="obj">The object to test.</param>
    /// <returns>true if the object is not <typeparamref name="T"/>;otherwise, false.</returns>
    public static bool IsNot<T>(this object obj) where T : class
    {
        return !(obj.Is<T>());
    }

    /// <summary>
    /// Convert the specified object to <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of expected.</typeparam>
    /// <param name="obj">The object to convert.</param>
    /// <returns>The converted <typeparamref name="T"/></returns>
    public static T? As<T>(this object obj) where T : class
    {
        return obj as T;
    }

    /// <summary>
    /// Determines whether the <paramref name="item"/> in the specified <paramref name="list"/>.
    /// </summary>
    /// <typeparam name="T">The type of <paramref name="item"/>.</typeparam>
    /// <param name="item">The item.</param>
    /// <param name="list">The list.</param>
    /// <returns>
    ///   <c>true</c> if the <paramref name="item"/> is in the <paramref name="list"/>; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">list</exception>
    public static bool In<T>(this T item, params T[] list)
    {
        if (list.IsNull())
        {
            throw new ArgumentNullException(nameof(list));
        }

        return list.Contains(item);
    }

    /// <summary>
    /// Determines whether the <paramref name="actual"/> is between <paramref name="lower"/> and <paramref name="upper"/>.
    /// </summary>
    /// <typeparam name="T">The type where inherts <see cref="IComparable{T}"/></typeparam>
    /// <param name="actual">The actual.</param>
    /// <param name="lower">The lower.</param>
    /// <param name="upper">The upper.</param>
    /// <returns>
    ///   <c>true</c> <paramref name="actual"/> is between <paramref name="lower"/> and <paramref name="upper"/>; otherwise, <c>false</c>.
    /// </returns>
    public static bool Between<T>(this T actual, T lower, T upper) where T : IComparable<T>
    {
        return actual.CompareTo(lower) >= 0 && actual.CompareTo(upper) <= 0;
    }

    /// <summary>
    /// Withes the specified action.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">The object.</param>
    /// <param name="action">The action.</param>
    public static void With<T>(this T obj, Action<T> action)
    {
        action(obj);
    }

    /// <summary>
    /// To the specified value of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static T? To<T>(this object obj)
    {
        T? result = default;

        result = (T)Convert.ChangeType(obj, typeof(T));

        return result;
    }

    /// <summary>
    /// To the specified value of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">The object.</param>
    /// <param name="default">The default.</param>
    /// <returns></returns>
    public static T To<T>(this object obj, T @default)
    {
        T result = @default;
        try
        {
            result = (T)Convert.ChangeType(obj, typeof(T));
            return result;
        }
        catch (Exception)
        {
            return @default;
        }
    }

    /// <summary>
    /// Throws if argument is null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">The object.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void ThrowIfArgumentIsNull<T>(this T obj, string parameterName) where T : class
    {
        if (obj == null)
        {
            throw new ArgumentNullException(parameterName);
        }
    }

    /// <summary>
    /// To the data table with type name of <paramref name="entity"/>.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <param name="entity">The entity.</param>
    /// <returns>An empty data table with type name of <paramref name="entity"/>.</returns>
    public static DataTable ToDataTable<T>(this T entity) where T : class
    {
        Type entityType = entity.GetType();

        DataTable table = new(entityType.Name);

        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

        foreach (PropertyDescriptor prop in properties)
        {
            Type type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            table.Columns.Add(prop.Name, type);
        }

        return table;
    }

    /// <summary>
    /// Convertables the specified type.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static bool Convertable(this object obj, ObjectConvertbleSupportedType type)
    {
        if (obj.IsNull())
        {
            return false;
        }

        try
        {
            if (type == ObjectConvertbleSupportedType.Int)
            {
                return int.TryParse(obj.ToString(), out int result);
            }
            else if (type == ObjectConvertbleSupportedType.Decimal)
            {
                return decimal.TryParse(obj.ToString(), out decimal result);
            }
            else if (type == ObjectConvertbleSupportedType.DateTime)
            {
                return DateTime.TryParse(obj.ToString(), out DateTime result);
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Hiddens the field value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">The object.</param>
    /// <param name="isReverse">If true reverse the fileds in <typeparamref name="T"/>.</param>
    /// <param name="fileds">The fileds.</param>
    /// <returns></returns>
    public static T HiddenFieldValue<T>(this T obj, bool isReverse = false, params string[] fileds) where T : class
    {
        if (obj.IsNull())
        {
            return obj;
        }

        var properties = obj.GetType().GetProperties();
        foreach (PropertyInfo? property in properties)
        {
            bool showOrHidden = isReverse ? (!fileds.Contains(property.Name)) : fileds.Contains(property.Name);
            if (property.IsNotNull() && property.CanWrite && showOrHidden)
            {
                property.SetValue(obj, null, null);
            }
        }

        return obj;
    }

    /// <summary>
    /// Safe the trim string properties of specified <paramref name="obj"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">The object.</param>
    /// <param name="fields">The fields for trim.</param>
    /// <returns></returns>
    public static T SafeTrimStringProperties<T>(this T obj, params string[] fields) where T : class
    {
        if (obj.IsNull())
        {
            return obj;
        }

        PropertyInfo[] properties = [];

        if (fields.IsNotNullOrEmpty())
        {
            properties = obj.GetType().GetProperties().Where(p => fields.Contains(p.Name) && p.PropertyType.Name == nameof(String)).EmptyIfNull().ToArray();
        }
        else
        {
            properties = obj.GetType().GetProperties().Where(p => p.PropertyType.Name == nameof(String)).EmptyIfNull().ToArray();
        }

        foreach (PropertyInfo? property in properties)
        {
            if (property.IsNotNull() && property.CanWrite && property.PropertyType.Name == nameof(String))
            {
                var value = property.GetValue(obj, null);

                if (value is not null)
                {
                    property.SetValue(obj, value.ToString()?.SafeTrim(), null);
                }
                else
                {
                    property.SetValue(obj, string.Empty, null);
                }
            }
        }

        return obj;
    }
}

/// <summary>
/// The object convertable supported type.
/// </summary>
[Serializable]
public enum ObjectConvertbleSupportedType
{
    /// <summary>
    /// The int
    /// </summary>
    Int = 0,

    /// <summary>
    /// The decimal
    /// </summary>
    Decimal = 1,

    /// <summary>
    /// The date time
    /// </summary>
    DateTime = 2
}
