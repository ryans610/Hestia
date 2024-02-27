﻿using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

using RyanJuan.Hestia.NonGeneric;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class RequiredIfAttribute : ValidationAttribute
{
    private static readonly RequiredAttribute s_validator = new();

    /// <inheritdoc cref="RequiredIfAttribute"/>
    /// <param name="mode"></param>
    /// <param name="targetPropertyName"></param>
    /// <param name="targetValue"></param>
    public RequiredIfAttribute(RequiredIfMode mode, string targetPropertyName, object? targetValue = null)
        : base(() => "The value for {0} is requried when {1] is {2}.")
    {
        _mode = mode;
        _targetPropertyName = targetPropertyName;
        _targetValue = targetValue;
    }

    private readonly RequiredIfMode _mode;
    private readonly string _targetPropertyName;
    private readonly object? _targetValue;

    /// <inheritdoc />
    public override string FormatErrorMessage(string name)
    {
        var condition = _mode switch
        {
            RequiredIfMode.TargetIsNullOrEmptyString => "null or empty",
            RequiredIfMode.TargetIsNotNullAndNotEmptyString => "not null or empty",
            RequiredIfMode.TargetValueEquals => $"equals to {_targetValue}",
            RequiredIfMode.TargetValueNotEquals => $"not equals to {_targetValue}",
            _ => null,
        };
        return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, _targetPropertyName, condition);
    }

    /// <inheritdoc />
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var targetProperty = ReflectionCenter.GetProperty(validationContext.ObjectType, _targetPropertyName);
        if (targetProperty is null)
        {
            throw new InvalidOperationException("The specific property name does not match any property.");
        }
        var targetValue = targetProperty.GetValue(validationContext.ObjectInstance);
        switch (_mode)
        {
            case RequiredIfMode.TargetIsNullOrEmptyString:
                if (IsNullOrEmptyString(targetValue))
                {
                    return ValidateRequired(value);
                }
                break;
            case RequiredIfMode.TargetIsNotNullAndNotEmptyString:
                if (IsNotNullAndNotEmptyString(targetValue))
                {
                    return ValidateRequired(value);
                }
                break;
            case RequiredIfMode.TargetIsNullOrEmptyEnumerable:
                if (IsNullOrEmptyEnumerable(targetValue))
                {
                    return ValidateRequired(value);
                }
                break;
            case RequiredIfMode.TargetIsNotNullAndNotEmptyEnumerable:
                if (IsNotNullAndNotEmptyEnumerable(targetValue))
                {
                    return ValidateRequired(value);
                }
                break;
            case RequiredIfMode.TargetValueEquals:
                if (IsValueEquals(targetValue, _targetValue))
                {
                    return ValidateRequired(value);
                }
                break;
            case RequiredIfMode.TargetValueNotEquals:
                if (IsValueNotEquals(targetValue, _targetValue))
                {
                    return ValidateRequired(value);
                }
                break;
        }
        return ValidationResult.Success;

        ValidationResult ValidateRequired(object? validateValue)
        {
            return s_validator.IsValid(validateValue)
                ? ValidationResult.Success!
                : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        static bool IsNullOrEmptyString(object? targetValue)
        {
            return targetValue is null or string { Length: 0 };
        }

        static bool IsNotNullAndNotEmptyString(object? targetValue)
        {
            return targetValue is string stringValue ? stringValue.Length > 0 : targetValue is not null;
        }

        static bool IsNullOrEmptyEnumerable(object? targetValue)
        {
            return targetValue is null || (targetValue is IEnumerable enumerable && !enumerable.Any());
        }

        static bool IsNotNullAndNotEmptyEnumerable(object? targetValue)
        {
            return targetValue is IEnumerable enumerable ? enumerable.Any() : targetValue is not null;
        }

        static bool IsValueEquals(object? targetValue, object? equalsTargetValue)
        {
            return equalsTargetValue?.Equals(targetValue) ?? targetValue is null;
        }

        static bool IsValueNotEquals(object? targetValue, object? equalsTargetValue)
        {
            return !IsValueEquals(targetValue, equalsTargetValue);
        }
    }
}
