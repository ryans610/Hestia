using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class EqualsToPropertyValueAttribute : ValidationAttribute, IClientModelValidator
{
    /// <inheritdoc cref="EqualsToPropertyValueAttribute"/>
    /// <param name="propertyName"></param>
    public EqualsToPropertyValueAttribute(string propertyName)
        : base(() => "The value for {0} must be equals to the value of {1}.")
    {
        _propertyName = propertyName;
    }

    private readonly string _propertyName;

    /// <inheritdoc />
    public override string FormatErrorMessage(string name)
    {
        return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, _propertyName);
    }

    /// <inheritdoc />
    public void AddValidation(ClientModelValidationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        context.Attributes.TryAdd("data-val", "true");
        context.Attributes.TryAdd("data-val-equalsToProperty", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
        context.Attributes.TryAdd("data-val-equalsToProperty-propertyName", _propertyName);
    }

    /// <inheritdoc />
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var property = ReflectionCenter.GetProperty(validationContext.ObjectType, _propertyName);
        if (property is null)
        {
            throw new InvalidOperationException("The specific property name does not match any property.");
        }
        var targetValue = property.GetValue(validationContext.ObjectInstance);
        if (value?.Equals(targetValue) ?? (targetValue is null))
        {
            return ValidationResult.Success;
        }
        return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
    }
}
