using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

public abstract class CompareValueAttributeBase : ValidationAttribute
{
    /// <inheritdoc />
    /// <param name="compareValue"></param>
    /// <param name="errorMessageAccessor"></param>
    protected CompareValueAttributeBase(int compareValue, Func<string> errorMessageAccessor)
        : base(errorMessageAccessor)
    {
        OperandType = typeof(int);
        CompareValue = compareValue;
    }

    /// <inheritdoc />
    /// <param name="compareValue"></param>
    /// <param name="errorMessageAccessor"></param>
    protected CompareValueAttributeBase(double compareValue, Func<string> errorMessageAccessor)
        : base(errorMessageAccessor)
    {
        OperandType = typeof(double);
        CompareValue = compareValue;
    }

    /// <inheritdoc />
    /// <param name="type"></param>
    /// <param name="compareValue"></param>
    /// <param name="errorMessageAccessor"></param>
    protected CompareValueAttributeBase(
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        Type type,
        string compareValue,
        Func<string> errorMessageAccessor)
        : base(errorMessageAccessor)
    {
        OperandType = type;
        CompareValue = compareValue;
    }

    /// <summary>
    /// Gets the type of the data field whose value must be validated.
    /// </summary>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    public virtual Type OperandType { get; }

    /// <summary>
    /// Gets the value to be compared.
    /// </summary>
    public object CompareValue { get; protected set; }

    /// <summary>
    /// Gets or sets a value that determines whether string values for
    /// <see cref="CompareValue"/> are parsed using the invariant culture rather than the current culture.
    /// </summary>
    public bool ParseLimitsInInvariantCulture { get; set; }

    /// <remarks>
    /// This property has no effects with the constructors with <see cref="int"/> or <see cref="double"/>
    /// parameters, for which the invariant culture is always used for any conversions of the validated value.
    /// </remarks>
    public virtual bool ConvertValueInInvariantCulture { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected Func<object, object?>? Conversion { get; set; }

    /// <inheritdoc />
    public override string FormatErrorMessage(string name)
    {
        SetupConversion();
        return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, CompareValue);
    }

    /// <summary>
    /// Validates the properties of this attribute and sets up the conversion function.
    /// This method throws exceptions if the attribute is not configured properly.
    /// If it has once determined it is properly configured, it is a NOP.
    /// </summary>
    protected virtual void SetupConversion()
    {
        if (Conversion == null)
        {
            object compareValue = CompareValue;

            if (compareValue is null)
            {
                throw new InvalidOperationException("RangeAttribute_Must_Set_Min_And_Max");
            }

            // Careful here -- OperandType could be int or double if they used the long form of the ctor.
            // But the compare value would still be strings.  Do use the type of the min/max operands to condition
            // the following code.
            var operandType = compareValue.GetType();

            if (operandType == typeof(int))
            {
                Initialize(
                    (int)compareValue,
                    v => Convert.ToInt32((object?)v, CultureInfo.InvariantCulture));
            }
            else if (operandType == typeof(double))
            {
                Initialize(
                    (double)compareValue,
                    v => Convert.ToDouble((object?)v, CultureInfo.InvariantCulture));
            }
            else
            {
                var type = OperandType;
                if (type == null)
                {
                    throw new InvalidOperationException("RangeAttribute_Must_Set_Operand_Type");
                }
                var comparableType = typeof(IComparable);
                if (!comparableType.IsAssignableFrom(type))
                {
                    throw new InvalidOperationException("RangeAttribute_ArbitraryTypeNotIComparable");
                }

                var converter = TypeDescriptor.GetConverter(OperandType);
                var comparable = (IComparable)(ParseLimitsInInvariantCulture
                    ? converter.ConvertFromInvariantString((string)compareValue)!
                    : converter.ConvertFromString((string)compareValue))!;

                Func<object, object?> conversion;
                if (ConvertValueInInvariantCulture)
                {
                    conversion = value => value.GetType() == type
                        ? value
                        : converter.ConvertFrom(null, CultureInfo.InvariantCulture, value);
                }
                else
                {
                    conversion = value => value.GetType() == type ? value : converter.ConvertFrom(value);
                }

                Initialize(comparable, conversion);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="compareValue"></param>
    /// <param name="conversion"></param>
    protected virtual void Initialize(IComparable compareValue, Func<object, object?> conversion)
    {
        CompareValue = compareValue;
        Conversion = conversion;
    }
}
