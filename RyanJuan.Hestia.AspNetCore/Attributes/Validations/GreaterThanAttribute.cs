using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// 
/// </summary>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
public class GreaterThanAttribute : CompareValueAttributeBase, IClientModelValidator
{
    private const string DefaultErrorMessage = "The value for {0} must be greater than {1}.";

    /// <inheritdoc />
    /// <param name="compareValue"></param>
    public GreaterThanAttribute(int compareValue)
        : base(compareValue, () => DefaultErrorMessage)
    {
    }

    /// <inheritdoc />
    /// <param name="compareValue"></param>
    public GreaterThanAttribute(double compareValue)
        : base(compareValue, () => DefaultErrorMessage)
    {
    }

    /// <inheritdoc />
    /// <param name="type"></param>
    /// <param name="compareValue"></param>
    public GreaterThanAttribute(
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        Type type,
        string compareValue)
        : base(type, compareValue, () => DefaultErrorMessage)
    {
    }

    // ReSharper disable once RedundantOverriddenMember
    // For xml=doc reference
    /// <inheritdoc />
    public override Type OperandType => base.OperandType;

    /// <summary>
    /// Determines whether any conversions necessary from the value being validated to <see cref="OperandType"/> as set
    /// by the <c>type</c> parameter of the <see cref="GreaterThanAttribute(Type, string)"/> constructor are carried
    /// out in the invariant culture rather than the current culture in effect at the time of the validation.
    /// </summary>
    /// <remarks>
    /// <inheritdoc/>
    /// </remarks>
    public override bool ConvertValueInInvariantCulture { get; set; }

    /// <inheritdoc />
    public override bool IsValid(object? value)
    {
        SetupConversion();

        if (value is null or string { Length: 0 })
        {
            return true;
        }

        object? convertedValue;
        try
        {
            convertedValue = Conversion!(value);
        }
        catch (FormatException)
        {
            return false;
        }
        catch (InvalidCastException)
        {
            return false;
        }
        catch (NotSupportedException)
        {
            return false;
        }

        var compareValue = (IComparable)CompareValue;
        return compareValue.CompareTo(convertedValue) < 0;
    }

    /// <inheritdoc />
    public void AddValidation(ClientModelValidationContext context)
    {
        SetupConversion();
        ArgumentNullException.ThrowIfNull(context);
        context.Attributes.TryAdd("data-val", "true");
        context.Attributes.TryAdd("data-val-greaterThan", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
        var stringCompareValue = CompareValue is IConvertible convertible
            ? convertible.ToString(CultureInfo.InvariantCulture)
            : CompareValue.ToString()!;
        context.Attributes.TryAdd("data-val-greaterThan-compareValue", stringCompareValue);
    }
}
