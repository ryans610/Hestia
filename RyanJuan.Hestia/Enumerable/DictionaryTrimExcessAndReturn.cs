namespace RyanJuan.Hestia;

public static partial class HestiaEnumerable
{
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static Dictionary<TKey, TValue> TrimExcessAndReturn<TKey, TValue>(
        this Dictionary<TKey, TValue> dictionary,
        int capacity)
        where TKey : notnull
    {
        Error.ThrowIfArgumentNull(nameof(dictionary), dictionary);
        dictionary.TrimExcess(capacity);
        return dictionary;
    }

#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static Dictionary<TKey, TValue> TrimExcessAndReturn<TKey, TValue>(
        this Dictionary<TKey, TValue> dictionary)
        where TKey : notnull
    {
        Error.ThrowIfArgumentNull(nameof(dictionary), dictionary);
        dictionary.TrimExcess();
        return dictionary;
    }
#endif
}
