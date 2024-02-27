using System.Collections;

using RyanJuan.Hestia.Resources;

namespace RyanJuan.Hestia.NonGeneric;

public static partial class HestiaNonGenericCollections
{
#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static IEnumerable Except(
        this IEnumerable first,
        IEnumerable second,
        IEqualityComparer? comparer)
    {
        Error.ThrowIfArgumentNull(nameof(first), first);
        Error.ThrowIfArgumentNull(nameof(second), second);
        return ExceptByInternal(first, second, static x => x, comparer);
    }
}
