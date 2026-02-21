# CandyBox.NET

[![NuGet Version](https://img.shields.io/nuget/v/IdeaTech.CandyBox)](https://www.nuget.org/packages/IdeaTech.CandyBox)
[![NuGet Downloads](https://img.shields.io/nuget/dt/IdeaTech.CandyBox)](https://www.nuget.org/packages/IdeaTech.CandyBox)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4)](https://dotnet.microsoft.com)

A rich collection of extension methods for the .NET `System` namespace — type conversion, validation, string manipulation, collection helpers, and more. All extensions live directly in the `System` namespace so they are available everywhere without extra `using` directives.

## Table of Contents

- [Installation](#installation)
- [Features](#features)
  - [String Extensions](#string-extensions)
  - [DateTime Extensions](#datetime-extensions)
  - [Numeric Extensions](#numeric-extensions)
  - [Boolean Extensions](#boolean-extensions)
  - [Char Extensions](#char-extensions)
  - [Enum Extensions](#enum-extensions)
  - [Object Extensions](#object-extensions)
  - [Type Extensions](#type-extensions)
  - [Collection Extensions](#collection-extensions)
  - [DataTable Extensions](#datatable-extensions)
  - [Verify (Guard Helpers)](#verify-guard-helpers)
- [License](#license)

---

## Installation

```shell
dotnet add package IdeaTech.CandyBox
```

Or via the NuGet Package Manager:

```shell
Install-Package IdeaTech.CandyBox
```

---

## Features

### String Extensions

#### Validation (`Is*`)

| Method | Description |
|--------|-------------|
| `IsEmail()` | Validates an email address (RFC 5321, supports modern TLDs) |
| `IsUrl(allowQuery)` | Validates an absolute URL; optionally allows query strings |
| `IsChineseMobile()` | Validates Chinese mobile phone numbers (prefixes 13x–19x) |
| `IsHans()` | Checks whether the string contains at least one CJK character |
| `IsGuid(format)` | Validates a GUID string for a specific format specifier |
| `IsValidLength(min, max)` | Checks whether the character length is within range |
| `IsValidByteCount(min, max)` | Checks whether the byte count is within range |
| `IsValidCultureIdentifier()` | Checks whether the string is a valid culture identifier |
| `IsByte/IsShort/IsInt32/IsInt64/IsDecimal/IsFloat(style)` | Numeric string validation |
| `IsDateTime(provider, styles)` | Validates a date/time string |
| `IsLetter()` | Checks whether the string contains only ASCII letters |
| `IsBase64String()` | Validates Base64 encoding |

```csharp
"user@example.photography".IsEmail();            // true
"13812345678".IsChineseMobile();                 // true
"Hello世界".IsHans();                             // true
"6ba7b810-9dad-11d1-80b4-00c04fd430c8".IsGuid(); // true
"abc123".IsValidLength(1, 10);                   // true
```

#### Conversion (`To*`)

| Method | Description |
|--------|-------------|
| `TrimAll()` | Removes all invisible/control characters |
| `TrimBlank()` | Trims all leading and trailing invisible characters |
| `Reverse()` | Reverses the string |
| `Truncate(length, replacement)` | Truncates to a maximum length with an optional suffix |
| `FirstCharToUpper()` | Capitalizes the first character |
| `ReplaceSpecialSharacters(replacement)` | Replaces non-alphanumeric characters |
| `ToByte/ToInt16/ToInt32/ToInt64()` | Parses to a numeric type, throws on failure |
| `ToEnum<T>(ignoreCase)` | Parses to an enum value |
| `ToGuid(format)` | Parses to a `Guid`; returns `null` for empty input |
| `ToDecimal(defaultValue)` | Parses to `decimal`; returns `defaultValue` on failure |
| `ToDateTimeWithPartten(pattern, default, provider)` | Parses with an exact format pattern |
| `ToSafeFileName(replacement)` | Replaces characters invalid in file names |
| `ToSafeFilePath(replacement)` | Replaces characters invalid in file paths |
| `ToUTF8()` | Re-encodes via a UTF-8 round-trip |
| `ToBase64String(encoding)` | Encodes the string to Base64 |
| `FromBase64String(encoding)` | Decodes a Base64 string |
| `GetFullChinesePhoneticAlphabet()` | Gets the full Pinyin of a Chinese string |
| `HtmlEncode/HtmlDecode()` | HTML encoding/decoding |
| `UrlEncode/UrlDecode()` | URL encoding/decoding |
| `RemoveNewLines()` | Removes all `\r`, `\n`, and `\r\n` characters |

```csharp
"hello world".FirstCharToUpper();         // "Hello world"
"Hello, World!".Truncate(5);              // "Hello ..."
"  \x00candybox\t  ".TrimBlank();         // "candybox"
"C:/path/file<name>.txt".ToSafeFileName();// "C__path_file_name_.txt"
"SGVsbG8gV29ybGQ=".FromBase64String();    // "Hello World"
"Hello World".ToBase64String();           // "SGVsbG8gV29ybGQ="
"王小明".GetFullChinesePhoneticAlphabet(); // "WangXiaoMing"
```

---

### DateTime Extensions

| Method | Description |
|--------|-------------|
| `ToRelativeTime(format)` | Returns a human-readable relative time string (e.g. "3 days ago") |
| `ToDateWithMinTime()` | Returns the date at `00:00:00.000` |
| `ToDateWithMaxTime()` | Returns the date at `23:59:59.999` |
| `SqlServerMinValue()` | Returns `1753-01-01 00:00:00` (SQL Server `datetime` minimum) |
| `SqlServerMaxValue()` | Returns `9999-12-31 23:59:59.997` (SQL Server `datetime` maximum) |
| `ToSqlServerSafeDateTime()` | Clamps the value to the SQL Server `datetime` range |
| `Age()` | Calculates age in years relative to today |
| `IsWorkingDay()` | Returns `true` for Monday–Friday |
| `IsWeekend()` | Returns `true` for Saturday and Sunday |
| `IsNullOrEmpty()` | Returns `true` if the value equals `DateTime.MinValue` or the DB null datetime |
| `ToDateParttenString(pattern)` | Formats to a date string using the given pattern |
| `ToDateTimeString(pattern)` | Formats to a datetime string (default: `yyyy-MM-dd HH:mm:ss`) |
| `ToMonthString(pattern)` | Formats to a year-month string (default: `yyyy-MM`) |

```csharp
DateTime.Now.AddDays(-3).ToRelativeTime("{0} {1} ago"); // "3 days ago"
DateTime.Now.ToDateWithMinTime();                        // 2025-06-10 00:00:00
DateTime.Now.ToDateWithMaxTime();                        // 2025-06-10 23:59:59.999
new DateTime(1990, 5, 20).Age();                         // 35
DateTime.Now.IsWorkingDay();                             // true (if not weekend)
someDateTime.ToSqlServerSafeDateTime();                  // clamped to SQL Server range
```

---

### Numeric Extensions

#### `decimal` / `double` / `float` / `int`

| Method | Description |
|--------|-------------|
| `ToFileSizeString()` | Converts a byte count to a human-readable size string (KB, MB, GB, TB, PB, EB) |
| `ToDecimal()` | Converts `double` or `float` to `decimal` |
| `ToEnum<T>()` | Converts `byte` or `int` to a typed enum value |

```csharp
1024m.ToFileSizeString();           // "1KB"
1_536_000m.ToFileSizeString();      // "1MB"
1_073_741_824m.ToFileSizeString();  // "1GB"
((double)1.5e6).ToDecimal();        // 1500000
2.ToEnum<DayOfWeek>();              // DayOfWeek.Tuesday
```

---

### Boolean Extensions

| Method | Description |
|--------|-------------|
| `ToTrueOrFalseString(trueString, falseString)` | Returns a custom string based on the boolean value |

```csharp
true.ToTrueOrFalseString();              // "Yes"
false.ToTrueOrFalseString("On", "Off");  // "Off"
```

---

### Char Extensions

| Method | Description |
|--------|-------------|
| `IsDigit()` | Wrapper for `char.IsDigit` |
| `IsLower()` | Wrapper for `char.IsLower` |
| `IsUpper()` | Wrapper for `char.IsUpper` |
| `IsLetterOrDigit()` | Wrapper for `char.IsLetterOrDigit` |

```csharp
'A'.IsUpper();          // true
'5'.IsDigit();          // true
'中'.IsLetterOrDigit(); // true
```

---

### Enum Extensions

| Method | Description |
|--------|-------------|
| `IntValue()` | Returns the `int` value of the enum member |
| `ByteValue()` | Returns the `byte` value of the enum member |
| `Equals(int)` | Compares the enum value to an `int` |
| `Equals(byte)` | Compares the enum value to a `byte` |
| `Equals(string)` | Compares the enum name to a string (case-insensitive) |

```csharp
DayOfWeek.Monday.IntValue();               // 1
DayOfWeek.Friday.Equals(5);               // true
DayOfWeek.Wednesday.Equals("wednesday");  // true
```

---

### Object Extensions

| Method | Description |
|--------|-------------|
| `IsNull()` / `IsNotNull()` | Null checks |
| `Is<T>()` / `IsNot<T>()` | Type checks |
| `As<T>()` | Safe cast via `as` operator |
| `In<T>(params T[] list)` | Checks whether the value is in the provided list |
| `Between<T>(lower, upper)` | Checks whether the value is within an inclusive range |
| `With<T>(action)` | Executes an action with the object (fluent helper) |
| `To<T>()` | Converts via `Convert.ChangeType` |
| `ToDataTable<T>()` | Creates an empty `DataTable` reflecting the object's type schema |
| `HiddenPropertiesValue(isReverse, fields)` | Sets specified properties to `null` |
| `SafeTrimStringProperties(properties)` | Trims all string properties of the object |

```csharp
string? s = null;
s.IsNull();                            // true

5.Between(1, 10);                      // true
"admin".In("root", "admin", "user");   // true

var order = new Order { Name = "  candy  ", Code = "  001  " };
order.SafeTrimStringProperties();      // Name = "candy", Code = "001"
```

---

### Type Extensions

| Method | Description |
|--------|-------------|
| `GetSchema()` | Returns an empty `DataTable` with columns matching the type's public properties |

```csharp
DataTable schema = typeof(Order).GetSchema();
// DataTable named "Order" with columns: Id, Name, Amount, ...
```

---

### Collection Extensions

#### `IEnumerable<T>`

| Method | Description |
|--------|-------------|
| `Clone<T>()` | Creates a shallow clone (requires `ICloneable` elements) |
| `IsNullOrEmpty()` | Returns `true` if the collection is `null` or empty |
| `IsNotNullOrEmpty()` | Inverse of `IsNullOrEmpty` |
| `EmptyIfNull<T>()` | Returns an empty collection instead of `null` |
| `ToDataTable<T>()` | Converts the collection to a populated `DataTable` |

#### `ICollection<T>`

| Method | Description |
|--------|-------------|
| `AddRange<T>(params T[] elements)` | Adds multiple elements in one call |

```csharp
List<string>? nullList = null;
nullList.IsNullOrEmpty();      // true

var list = new List<int> { 1, 2 };
list.AddRange(3, 4, 5);        // [1, 2, 3, 4, 5]

DataTable table = orders.ToDataTable();
```

---

### DataTable Extensions

| Method | Description |
|--------|-------------|
| `ToMarkdownTable(alignment)` | Renders the `DataTable` as a Markdown table string |

Supported alignments: `Default`, `Left`, `Center`, `Right`.

```csharp
string markdown = orders.ToDataTable()
                        .ToMarkdownTable(MarkdownTableContentAlignment.Center);
```

Output:

|Id|Name|Amount|
|:---:|:---:|:---:|
|1|CandyBox|99.9|
|2|DotNet|199.0|

---

### Verify (Guard Helpers)

The `Verify` static class provides guard methods for validating method arguments. All methods throw a descriptive exception when the assertion fails.

| Method | Throws |
|--------|--------|
| `Verify.NotNull(obj)` | `ArgumentNullException` |
| `Verify.NotNullOrWhiteSpace(value)` | `ArgumentException` |
| `Verify.NotNullOrEmpty(collection)` | `ArgumentNullException` / `ArgumentException` |
| `Verify.Between(value, lower, upper)` | `ArgumentOutOfRangeException` |
| `Verify.True(condition, message)` | `ArgumentException` |
| `Verify.False(condition, message)` | `ArgumentException` |
| `Verify.ValidUrl(url, allowQueryString)` | `ArgumentException` |

```csharp
public void Process(string name, int count, IEnumerable<Order> orders)
{
    Verify.NotNullOrWhiteSpace(name);
    Verify.Between(count, 1, 100);
    Verify.NotNullOrEmpty(orders);
    // ...
}
```

---

## License

This project is licensed under the [MIT License](LICENSE).

© ShuaiHua Du. All rights reserved.
