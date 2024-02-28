using System.ComponentModel.DataAnnotations;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

// TODO: additional MonthOfAttribute that can specific calendar or culture.
/// <summary>
/// Specifies that a data field value is an integer value that is between 1 and 12.
/// </summary>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class MonthAttribute() :
    ValidationAttribute(() => "The value for {0} must be between 1 and 12."),
    IClientModelValidator
{
    /// <inheritdoc />
    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return true;
        }

        return !int.TryParse(value.ToString(), out int intValue) ||
               intValue is >= 1 and <= 12;
    }

    /// <inheritdoc />
    public void AddValidation(ClientModelValidationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        context.Attributes.TryAdd("data-val", "true");
        context.Attributes.TryAdd("data-val-range", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
        context.Attributes.TryAdd("data-val-range-min", "1");
        context.Attributes.TryAdd("data-val-range-max", "12");
    }
}
