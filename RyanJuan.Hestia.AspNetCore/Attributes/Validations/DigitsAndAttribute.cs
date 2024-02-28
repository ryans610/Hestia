using System.ComponentModel.DataAnnotations;
using System.Globalization;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// 
/// </summary>
/// <param name="characters"></param>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
public class DigitsAndAttribute(params char[] characters) :
    ValidationAttribute(() => "All characters in {0} must be digits or in \"{1}\"."),
    IClientModelValidator
{
    private static readonly DigitsAttribute s_digitsValidator = new();

    /// <inheritdoc />
    public override bool IsValid(object? value)
    {
        if (s_digitsValidator.IsValid(value))
        {
            return true;
        }

        return value!
            .ToString()! // null is check in DigitsAttribute
            .Where(c => c is < '0' or > '9')
            .All(c => characters.Contains(c));
    }

    /// <inheritdoc />
    public override string FormatErrorMessage(string name)
    {
        return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, new string(characters));
    }

    /// <inheritdoc />
    public void AddValidation(ClientModelValidationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        context.Attributes.TryAdd("data-val", "true");
        context.Attributes.TryAdd("data-val-digitsAnd", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
        context.Attributes.TryAdd("data-val-digitsAnd-characters", new(characters));
    }
}
