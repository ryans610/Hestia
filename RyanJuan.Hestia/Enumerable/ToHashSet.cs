namespace RyanJuan.Hestia;

public static partial class HestiaEnumerable
{
#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static HashSet<TSource> ToHashSet<TSource>(
        this IEnumerable<TSource> source,
        IEqualityComparer<TSource>? comparer = null)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        return new(source, comparer);
    }
}
