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
    /// <param name="defaultValue"></param>
    /// <returns></returns>
#endif
    [PublicAPI]
    public static IEnumerable<TElement>? GetValueOrDefault<TKey, TElement>(
        this ILookup<TKey, TElement> source,
        TKey key,
        IEnumerable<TElement>? defaultValue = default)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        return source.Contains(key) ? source[key] : defaultValue;
    }
}
