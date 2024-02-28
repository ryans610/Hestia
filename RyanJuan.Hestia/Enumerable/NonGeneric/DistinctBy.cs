using System.Collections;

using RyanJuan.Hestia.Resources;

namespace RyanJuan.Hestia.NonGeneric;

public static partial class HestiaNonGenericCollections
{
#if ZH_HANT
#else
#endif
    [PublicAPI]
    [LinqTunnel]
    public static IEnumerable DistinctBy(
        this IEnumerable source,
        Func<object?, object?> keySelector,
        IEqualityComparer? comparer)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        Error.ThrowIfArgumentNull(nameof(keySelector), keySelector);
        return DistinctByInternal(source, keySelector, comparer);
    }

#if ZH_HANT
#else
#endif
    [PublicAPI]
    [LinqTunnel]
    public static IEnumerable DistinctBy(
        this IEnumerable source,
        Func<object?, object?> keySelector)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        Error.ThrowIfArgumentNull(nameof(keySelector), keySelector);
        return DistinctByInternal(source, keySelector, null);
    }

    internal static IEnumerable DistinctByInternal(
        IEnumerable source,
        Func<object?, object?> keySelector,
        IEqualityComparer? comparer)
    {
        comparer ??= UnknownTypeEqualityComparer.Default;
        var set = new HashtableAsSetAdapter(comparer);
        IEnumerator? iterator = null;
        try
        {
            iterator = source.GetEnumerator();
            while (iterator.MoveNext())
            {
                var key = keySelector.Invoke(iterator.Current);
                if (set.Contains(key))
                {
                    continue;
                }

                yield return iterator.Current;
                set.Add(key);
            }
        }
        finally
        {
            if (iterator is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
