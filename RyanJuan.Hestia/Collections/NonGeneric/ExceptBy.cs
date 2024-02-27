using System.Collections;

using RyanJuan.Hestia.Resources;

namespace RyanJuan.Hestia.NonGeneric;

public static partial class HestiaNonGenericCollections
{
#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static IEnumerable ExceptBy(
        this IEnumerable first,
        IEnumerable second,
        Func<object?, object?> keySelector,
        IEqualityComparer? comparer)
    {
        Error.ThrowIfArgumentNull(nameof(first), first);
        Error.ThrowIfArgumentNull(nameof(second), second);
        return ExceptByInternal(first, second, keySelector, comparer);
    }

    internal static IEnumerable ExceptByInternal(
        IEnumerable first,
        IEnumerable second,
        Func<object?, object?> keySelector,
        IEqualityComparer? comparer)
    {
        comparer ??= UnknownTypeEqualityComparer.Default;
        var set = new HashtableAsSetAdapter(second, comparer);
        IEnumerator? firstIterator = null;
        try
        {
            firstIterator = first.GetEnumerator();
            while (firstIterator.MoveNext())
            {
                var key = keySelector.Invoke(firstIterator.Current);
                if (set.Contains(key))
                {
                    continue;
                }
                yield return firstIterator.Current;
                set.Add(key);
            }
        }
        finally
        {
            if (firstIterator is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
