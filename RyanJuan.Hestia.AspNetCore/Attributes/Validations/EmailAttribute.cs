using System.ComponentModel.DataAnnotations;
using System.Globalization;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <inheritdoc cref="EmailAddressAttribute"/>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class EmailAttribute() : DataTypeAttribute(DataType.EmailAddress), IClientModelValidator
{
    private static readonly EmailAddressAttribute s_validator = new();

    /// <summary>
    /// Whether the empty string is disallowed or not.
    /// The default value is <see langword="false"/>.
    /// If the value is set to <see langword="true"/>,
    /// the validation behavior of <see cref="EmailAttribute"/>
    /// will be the same as <see cref="EmailAddressAttribute"/>.
    /// </summary>
    public bool DisallowEmptyStrings { get; set; } = false;

    /// <inheritdoc />
    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return true;
        }

        if (value is not string stringValue)
        {
            return false;
        }

        if (!DisallowEmptyStrings &&
            stringValue.Length == 0)
        {
            return true;
        }

        return s_validator.IsValid(value);
    }

    /// <inheritdoc />
    public override string FormatErrorMessage(string name)
    {
        if (ErrorMessageString == ValidationHelper.DefaultDataTypeAttributeErrorMessage)
        {
            return s_validator.FormatErrorMessage(name);
        }

        return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
    }

    /// <inheritdoc />
    public void AddValidation(ClientModelValidationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        context.Attributes.TryAdd("data-val", "true");
        context.Attributes.TryAdd("data-val-email", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
    }
}
