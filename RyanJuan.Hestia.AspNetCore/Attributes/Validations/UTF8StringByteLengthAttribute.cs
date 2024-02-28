using System.ComponentModel.DataAnnotations;
using System.Globalization;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// 
/// </summary>
/// <inheritdoc cref="UTF8StringByteLengthAttribute"/>
/// <param name="maximumLength">
/// The inclusive maximum byte length in UTF-8.
/// </param>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
// ReSharper disable once InconsistentNaming
public class UTF8StringByteLengthAttribute(int maximumLength) :
    ValidationAttribute(() => "The string's byte length for {0} in UTF-8 must be between {1} and {2}."),
    IClientModelValidator
{
    /// <summary>
    /// The inclusive maximum byte length in UTF-8.
    /// </summary>
    public int MaximumLength { get; } = maximumLength;

    /// <summary>
    /// The inclusive minimum byte length in UTF-8.
    /// The default value is <see langword="0"/>.
    /// </summary>
    public int MinimumLength { get; set; } = 0;

    /// <inheritdoc />
    public override bool IsValid(object? value)
    {
        EnsureLegalLengths();
        if (value is null)
        {
            return true;
        }

        if (value is not string stringValue)
        {
            return false;
        }

        int byteLength = Encoding.UTF8.GetByteCount(stringValue);
        return byteLength <= MaximumLength &&
               byteLength >= MinimumLength;
    }

    /// <inheritdoc />
    public override string FormatErrorMessage(string name)
    {
        EnsureLegalLengths();
        return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, MinimumLength, MaximumLength);
    }

    /// <inheritdoc />
    public void AddValidation(ClientModelValidationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        EnsureLegalLengths();
        context.Attributes.TryAdd("data-val", "true");
        context.Attributes.TryAdd("data-val-utf8StringByteLength",
            FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
        context.Attributes.TryAdd("data-val-utf8StringByteLength-maximum",
            MaximumLength.ToString(CultureInfo.InvariantCulture));
        context.Attributes.TryAdd("data-val-utf8StringByteLength-minimum",
            MinimumLength.ToString(CultureInfo.InvariantCulture));
    }

    private void EnsureLegalLengths()
    {
        if (MaximumLength < 0)
        {
            throw new InvalidOperationException(
                $"The value of {nameof(MaximumLength)} must be equals or greater than 0.");
        }

        if (MaximumLength < MinimumLength)
        {
            throw new InvalidOperationException(
                $"The value of {nameof(MaximumLength)} must be equals or greater than the value of {nameof(MinimumLength)}.");
        }
    }
}
