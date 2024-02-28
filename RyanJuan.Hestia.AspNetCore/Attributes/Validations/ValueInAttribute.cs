using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

using JetBrains.Annotations;

using RyanJuan.Hestia.NonGeneric;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// Check if the value is one of the specific values.
/// </summary>
/// <inheritdoc cref="ValueInAttribute"/>
/// <param name="allowValues"></param>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class ValueInAttribute(params object[] allowValues) :
    ValidationAttribute(() => "The value for {0} must be one of the specific value. The allow value is as follow: {1}.")
{
    /// <inheritdoc />
    public override bool IsValid(object? value)
    {
        return value is null || (allowValues as IEnumerable).Contains(value);
    }

    /// <inheritdoc />
    public override string FormatErrorMessage(string name)
    {
        return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, allowValues.JoinAsString(','));
    }
}
