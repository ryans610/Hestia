using System.ComponentModel.DataAnnotations;

namespace RyanJuan.Hestia.AspNetCore.Resources;

internal static class ValidationHelper
{
    // ReSharper disable once InconsistentNaming
    internal const int ASCIIAlphabetA = 'A';
    internal const int IntegerCharToInt32BitMask = 0b_0000_1111;

    private static readonly DataTypeAttribute s_dataTypeAttribute = new(DataType.Custom);
    private static readonly RequiredAttribute s_requiredAttribute = new();

    internal static string DefaultDataTypeAttributeErrorMessage => s_dataTypeAttribute.FormatErrorMessage("{0}");
    internal static string DefaultRequiredAttributeErrorMessage => s_requiredAttribute.FormatErrorMessage("{0}");
}
