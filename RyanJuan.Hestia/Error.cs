namespace RyanJuan.Hestia;

internal class Error
{
    internal const string ArgumentSmallerThan =
#if ZH_HANT
        "{0} 必須大於或等於 {1}。";
#else
        "{0} must be greater than or equal to {1}.";
#endif

    internal const string ArgumentBiggerThanOrEqualTo =
#if ZH_HANT
        "{0} 必須小於 {1}。";
#else
        "{0} must be less than {1}.";
#endif

    internal static ArgumentNullException ArgumentNull(
        string name)
    {
        return new ArgumentNullException(name);
    }

    internal static void ThrowIfArgumentNull<TValue>(
        string name,
        [NoEnumeration] TValue value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(name);
        }
    }

    internal static ArgumentOutOfRangeException ArgumentOutOfRange(
        string name,
        string message,
        object? actualValue = null)
    {
        if (actualValue is null)
        {
            return new ArgumentOutOfRangeException(name, message);
        }
        else
        {
            return new ArgumentOutOfRangeException(name, actualValue, message);
        }
    }

    internal static void ThrowIfArgumentSmallerThanZero(
        string name,
        int value)
    {
        if (value < 0)
        {
            throw ArgumentOutOfRange(
                name,
                string.Format(ArgumentSmallerThan, name, 0),
                value);
        }
    }

    internal static ArgumentException Argument(
        string name,
        string message)
    {
        return new ArgumentException(message, name);
    }
}
