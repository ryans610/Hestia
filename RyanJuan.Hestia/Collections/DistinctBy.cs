namespace RyanJuan.Hestia;

public static partial class HestiaCollections
{
#if !NET6_0_OR_GREATER
#if ZH_HANT
#else
#endif
    [PublicAPI]
    [LinqTunnel]
    public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        IEqualityComparer<TKey>? comparer)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        Error.ThrowIfArgumentNull(nameof(keySelector), keySelector);
        comparer ??= EqualityComparer<TKey>.Default;
        var set = new HashSet<TKey>(comparer);
        using var iterator = source.GetEnumerator();
        while (iterator.MoveNext())
        {
            var key = keySelector.Invoke(iterator.Current);
            if (set.Add(key))
            {
                yield return iterator.Current;
            }
        }
    }

#if ZH_HANT
#else
#endif
    [PublicAPI]
    [LinqTunnel]
    public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector)
    {
        return source.DistinctBy(keySelector, null);
    }
#endif
}
