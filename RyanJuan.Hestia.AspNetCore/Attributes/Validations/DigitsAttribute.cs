﻿using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class DigitsAttribute : ValidationAttribute, IClientModelValidator
{
    /// <inheritdoc cref="DigitsAttribute"/>
    public DigitsAttribute()
        : base(() => "All characters in {0} must be digits.")
    {
    }

    /// <inheritdoc />
    public override bool IsValid(object? value)
    {
        var stringValue = value?.ToString();
        return stringValue.IsNullOrEmpty() || stringValue.All(c => c is >= '0' and <= '9');
    }

    /// <inheritdoc />
    public void AddValidation(ClientModelValidationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        context.Attributes.TryAdd("data-val", "true");
        context.Attributes.TryAdd("data-val-digits", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
    }
}
