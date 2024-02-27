namespace RyanJuan.Hestia;

public static partial class HestiaString
{
    //feature in .Net Core and .Net Standard 2.1
#if !(NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER)
#if ZH_HANT
#else
#endif
    public static int IndexOf(
        this string str,
        char value,
        StringComparison comparisonType)
    {
        Error.ThrowIfArgumentNull(nameof(str), str);
        switch (comparisonType)
        {
            case StringComparison.CurrentCulture:
            case StringComparison.CurrentCultureIgnoreCase:
                return CultureInfo.CurrentCulture.CompareInfo.IndexOf(
                    str,
                    value,
                    GetCaseCompareOfComparisonCulture(comparisonType));
            case StringComparison.InvariantCulture:
            case StringComparison.InvariantCultureIgnoreCase:
                return CultureInfo.InvariantCulture.CompareInfo.IndexOf(
                    str,
                    value,
                    GetCaseCompareOfComparisonCulture(comparisonType));
            case StringComparison.Ordinal:
                return str.IndexOf(value);
            case StringComparison.OrdinalIgnoreCase:
                return CultureInfo.InvariantCulture.CompareInfo.IndexOf(
                    str,
                    value,
                    CompareOptions.OrdinalIgnoreCase);
            default:
                throw new ArgumentException("", nameof(comparisonType));
        }

        static CompareOptions GetCaseCompareOfComparisonCulture(StringComparison comparisonType)
        {
            return (CompareOptions)((int)comparisonType & (int)CompareOptions.IgnoreCase);
        }
    }
#endif
}
