using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class TaiwanIdNoAttribute : ValidationAttribute, IClientModelValidator
{
    private const int TaiwanIdNoLength = 10;

    private static readonly ReadOnlyCollection<int> s_alphabetScore = new List<int>
    {
        1, 10, 19, 28, 37, 46, 55, 64, 39, 73, 82, 2, 11,
        20, 48, 29, 38, 47, 56, 65, 74, 83, 21, 3, 12, 30,
    }.AsReadOnly();

    /// <inheritdoc cref="TaiwanIdNoAttribute"/>
    public TaiwanIdNoAttribute()
        : base(() => "The value for {0} must be a valid Taiwan Id.")
    {
    }

    /// <inheritdoc />
    public override bool IsValid(object? value)
    {
        var stringValue = value?.ToString()?.ToUpperInvariant();
        if (stringValue.IsNullOrEmpty())
        {
            return true;
        }
        if (stringValue.Length != TaiwanIdNoLength)
        {
            return false;
        }
        if (!Regex.IsMatch(stringValue, $@"^[A-Z](1|2)\d{{{TaiwanIdNoLength - 2}}}$"))
        {
            return false;
        }
        int total = s_alphabetScore[stringValue[0] - ValidationHelper.ASCIIAlphabetA];
        for (var i = 1; i <= 8; i += 1)
        {
            total += (stringValue[i] & ValidationHelper.IntegerCharToInt32BitMask) * (9 - i);
        }
        total += stringValue[9] & ValidationHelper.IntegerCharToInt32BitMask;
        return total % 10 == 0;
    }

    /// <inheritdoc />
    public void AddValidation(ClientModelValidationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        context.Attributes.TryAdd("data-val", "true");
        context.Attributes.TryAdd("data-val-taiwanIdNo", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
    }
}
