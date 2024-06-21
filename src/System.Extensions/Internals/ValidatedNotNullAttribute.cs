// Copyright (c) IdeaTech. All rights reserved.

namespace System.Extensions.Internals;

/// <summary>
/// Signal attribute for static analysis that indicates a helper method is validating arguments for <see langword="null"/>.
/// CodeAnalysis attribute that tells the CA1062 validate arguments rule that this method validates the argument is not null.
/// Apply this attribute to the value parameter of your validation methods.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
internal sealed class ValidatedNotNullAttribute : Attribute
{
}
