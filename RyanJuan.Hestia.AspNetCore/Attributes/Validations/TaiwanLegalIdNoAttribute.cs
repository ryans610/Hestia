using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class TaiwanLegalIdNoAttribute : ValidationAttribute, IClientModelValidator
{
    private const int TaiwanLegalIdNoLength = 8;

    private static readonly ReadOnlyCollection<int> s_weights = new List<int>
    {
        1, 2, 1, 2, 1, 2, 4, 1,
    }.AsReadOnly();

    /// <inheritdoc cref="TaiwanLegalIdNoAttribute"/>
    public TaiwanLegalIdNoAttribute()
        : base(() => "The value for {0} must be a valid Taiwan LegalId.")
    {
    }

    /// <inheritdoc />
    public override bool IsValid(object? value)
    {
        var stringValue = value?.ToString();
        if (stringValue.IsNullOrEmpty())
        {
            return true;
        }
        if (stringValue.Length != TaiwanLegalIdNoLength)
        {
            return false;
        }
        if (!Regex.IsMatch(stringValue, $@"^\d{{{TaiwanLegalIdNoLength}}}$"))
        {
            return false;
        }
        var intValues = new int[TaiwanLegalIdNoLength];
        int total = 0;
        for (int i = 0; i < TaiwanLegalIdNoLength; i += 1)
        {
            intValues[i] = stringValue[i] & ValidationHelper.IntegerCharToInt32BitMask;
            int product = intValues[i] * s_weights[i];
            int result = product / 10 + product % 10;
            if (result == 10)
            {
                result = 1;
            }
            total += result;
        }
        int remainder = total % 10;
        return remainder == 0 || (remainder == 9 && intValues[6] == 7);
    }

    /// <inheritdoc />
    public void AddValidation(ClientModelValidationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        context.Attributes.TryAdd("data-val", "true");
        context.Attributes.TryAdd("data-val-taiwanLegalIdNo", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
    }
}
