using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

using RyanJuan.Hestia.NonGeneric;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// Check if the value is one of the specific values.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class ValueInAttribute : ValidationAttribute
{
    /// <inheritdoc cref="ValueInAttribute"/>
    /// <param name="allowValues"></param>
    public ValueInAttribute(params object[] allowValues)
        : base(() => "The value for {0} must be one of the specific value. The allow value is as follow: {1}.")
    {
        _allowValues = allowValues;
    }

    private readonly object[] _allowValues;

    /// <inheritdoc />
    public override bool IsValid(object? value)
    {
        return value is null || (_allowValues as IEnumerable).Contains(value);
    }

    /// <inheritdoc />
    public override string FormatErrorMessage(string name)
    {
        return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, _allowValues.JoinAsString(','));
    }
}
