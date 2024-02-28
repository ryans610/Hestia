namespace RyanJuan.Hestia;

public static partial class HestiaEnumerable
{
#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="key"></param>
    /// <param name="result"></param>
    /// <returns></returns>
#endif
    [PublicAPI]
    public static bool TryGetValue<TKey, TElement>(
        this ILookup<TKey, TElement> source,
        TKey key,
#if NETCOREAPP3_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [MaybeNullWhen(false)]
#endif
        out IEnumerable<TElement> result)
    {
        if (source.Contains(key))
        {
            result = source[key];
            return true;
        }

        result = default;
        return false;
    }
}
