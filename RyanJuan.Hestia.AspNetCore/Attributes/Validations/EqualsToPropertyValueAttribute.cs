using System.ComponentModel.DataAnnotations;
using System.Globalization;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// 
/// </summary>
/// <inheritdoc cref="EqualsToPropertyValueAttribute"/>
/// <param name="propertyName"></param>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class EqualsToPropertyValueAttribute(string propertyName) :
    ValidationAttribute(() => "The value for {0} must be equals to the value of {1}."),
    IClientModelValidator
{
    /// <inheritdoc />
    public override string FormatErrorMessage(string name)
    {
        return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, propertyName);
    }

    /// <inheritdoc />
    public void AddValidation(ClientModelValidationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        context.Attributes.TryAdd("data-val", "true");
        context.Attributes.TryAdd("data-val-equalsToProperty",
            FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
        context.Attributes.TryAdd("data-val-equalsToProperty-propertyName", propertyName);
    }

    /// <inheritdoc />
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var property = ReflectionCenter.GetProperty(validationContext.ObjectType, propertyName) ??
                       throw new InvalidOperationException("The specific property name does not match any property.");
        var targetValue = property.GetValue(validationContext.ObjectInstance);
        if (value?.Equals(targetValue) ?? (targetValue is null))
        {
            return ValidationResult.Success;
        }

        return new(FormatErrorMessage(validationContext.DisplayName));
    }
}
