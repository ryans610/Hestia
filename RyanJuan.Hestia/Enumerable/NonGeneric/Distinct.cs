using System.Collections;

namespace RyanJuan.Hestia.NonGeneric;

public static partial class HestiaNonGenericCollections
{
#if ZH_HANT
#else
#endif
    [PublicAPI]
    [LinqTunnel]
    public static IEnumerable Distinct(
        this IEnumerable source,
        IEqualityComparer? comparer)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        return DistinctByInternal(source, static x => x, comparer);
    }

#if ZH_HANT
#else
#endif
    [PublicAPI]
    [LinqTunnel]
    public static IEnumerable Distinct(
        this IEnumerable source)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        return DistinctByInternal(source, static x => x, null);
    }
}
