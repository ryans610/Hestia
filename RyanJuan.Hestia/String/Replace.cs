namespace RyanJuan.Hestia;

public static partial class HestiaString
{
#if ZH_HANT
#else
#endif
    public static string Replace(
        this string value,
        IEnumerable<KeyValuePair<string, string>> replacePairs)
    {
        Error.ThrowIfArgumentNull(nameof(value), value);
        Error.ThrowIfArgumentNull(nameof(replacePairs), replacePairs);
        var builder = new StringBuilder(value);
        using var iterator = replacePairs.GetEnumerator();
        while (iterator.MoveNext())
        {
            Error.ThrowIfArgumentNull(
                nameof(replacePairs),
                iterator.Current.Key);
            builder.Replace(
                iterator.Current.Key,
                iterator.Current.Value);
        }

        return builder.ToString();
    }

#if ZH_HANT
#else
#endif
    public static string Replace(
        this string value,
        params KeyValuePair<string, string>[] replacePairs)
    {
        Error.ThrowIfArgumentNull(nameof(value), value);
        Error.ThrowIfArgumentNull(nameof(replacePairs), replacePairs);
        return value.Replace(replacePairs.AsEnumerable());
    }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_0_OR_GREATER
#if ZH_HANT
#else
#endif
    public static string Replace(
        this string value,
        IEnumerable<(string oldValue, string newValue)> replacePairs)
    {
        Error.ThrowIfArgumentNull(nameof(value), value);
        Error.ThrowIfArgumentNull(nameof(replacePairs), replacePairs);
        var builder = new StringBuilder(value);
        using var iterator = replacePairs.GetEnumerator();
        while (iterator.MoveNext())
        {
            Error.ThrowIfArgumentNull(
                nameof(replacePairs),
                iterator.Current.oldValue);
            builder.Replace(
                iterator.Current.oldValue,
                iterator.Current.newValue);
        }

        return builder.ToString();
    }

#if ZH_HANT
#else
#endif
    public static string Replace(
        this string value,
        params (string oldValue, string newValue)[] replacePairs)
    {
        Error.ThrowIfArgumentNull(nameof(value), value);
        Error.ThrowIfArgumentNull(nameof(replacePairs), replacePairs);
        return value.Replace(replacePairs.AsEnumerable());
    }
#endif
}
