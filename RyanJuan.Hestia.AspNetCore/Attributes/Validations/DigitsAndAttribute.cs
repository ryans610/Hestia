using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
public class DigitsAndAttribute : ValidationAttribute, IClientModelValidator
{
    private static readonly DigitsAttribute s_digitsValidator = new();

    /// <inheritdoc />
    /// <param name="characters"></param>
    public DigitsAndAttribute(params char[] characters)
        : base(() => "All characters in {0} must be digits or in \"{1}\".")
    {
        _characters = characters;
    }

    private readonly char[] _characters;

    /// <inheritdoc />
    public override bool IsValid(object? value)
    {
        if (s_digitsValidator.IsValid(value))
        {
            return true;
        }
        return value!
            .ToString()!    // null is check in DigitsAttribute
            .Where(c => c is < '0' or > '9')
            .All(c => _characters.Contains(c));
    }

    /// <inheritdoc />
    public override string FormatErrorMessage(string name)
    {
        return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, new string(_characters));
    }

    /// <inheritdoc />
    public void AddValidation(ClientModelValidationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        context.Attributes.TryAdd("data-val", "true");
        context.Attributes.TryAdd("data-val-digitsAnd", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
        context.Attributes.TryAdd("data-val-digitsAnd-characters", new string(_characters));
    }
}
