using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

using RyanJuan.Hestia.NonGeneric;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// Specifies that a data field value is required and must not be empty.
/// The empty value for string is <see cref="string.Empty"/>,
/// and for enumerable means that there is no item in the enumerable.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class NotEmptyAttribute : RequiredAttribute
{
    /// <inheritdoc />
    public override bool IsValid(object? value)
    {
        if (!base.IsValid(value))
        {
            return false;
        }
        return value switch
        {
            string stringValue => stringValue.Length > 0,
            IEnumerable enumerable => enumerable.Any(),
            _ => true,
        };
    }

    /// <inheritdoc />
    public override string FormatErrorMessage(string name)
    {
        var errorMessageString = ErrorMessageString;
        if (errorMessageString == ValidationHelper.DefaultRequiredAttributeErrorMessage)
        {
            errorMessageString = "The value for {0} must not be empty.";
        }
        return string.Format(CultureInfo.CurrentCulture, errorMessageString, name);
    }
}
