using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

using JetBrains.Annotations;

using RyanJuan.Hestia.NonGeneric;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// Check if the value is one of the value in specific IEnumerable property.
/// </summary>
/// <inheritdoc cref="ValueInDependentPropertyAttribute"/>
/// <param name="dependentPropertyName"></param>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class ValueInDependentPropertyAttribute(string dependentPropertyName) :
    ValidationAttribute(() => "The value for {0} must be one of the value in {1}.")
{
    /// <inheritdoc />
    public override string FormatErrorMessage(string name)
    {
        return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, dependentPropertyName);
    }

    /// <inheritdoc />
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }

        var dependentProperty = ReflectionCenter.GetProperty(validationContext.ObjectType, dependentPropertyName) ??
                                throw new InvalidOperationException(
                                    "The specific property name does not match any property.");
        var dependentValue = dependentProperty.GetValue(validationContext.ObjectInstance);
        if (dependentValue is not IEnumerable enumerable)
        {
            throw new InvalidOperationException("The specific property is not an instance of IEnumerable.");
        }

        if (enumerable.Contains(value))
        {
            return ValidationResult.Success;
        }

        return new(FormatErrorMessage(validationContext.DisplayName));
    }
}
