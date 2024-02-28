namespace RyanJuan.Hestia;

public static partial class HestiaEnumerable
{
#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static string JoinAsString<TSource>(
        this IEnumerable<TSource> source,
        string separator)
    {
        return string.Join(separator, source);
    }

#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static string JoinAsString<TSource>(
        this IEnumerable<TSource> source,
        char separator)
    {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        return string.Join(separator, source);
#else
        return source.JoinAsString(separator.ToString());
#endif
    }
}
