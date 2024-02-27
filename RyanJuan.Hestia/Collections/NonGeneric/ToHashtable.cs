using System.Collections;

using RyanJuan.Hestia.Resources;

namespace RyanJuan.Hestia.NonGeneric;

public static partial class HestiaNonGenericCollections
{
#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static Hashtable ToHashtable(
        this IEnumerable source,
        Func<object?, object> keySelector,
        Func<object?, object?> valueSelector,
        IEqualityComparer? comparer)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        Error.ThrowIfArgumentNull(nameof(keySelector), keySelector);
        Error.ThrowIfArgumentNull(nameof(valueSelector), valueSelector);
        return ToHashtableInternal(source, keySelector, valueSelector, comparer);
    }

#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static Hashtable ToHashtable(
        this IEnumerable source,
        Func<object?, object> keySelector,
        Func<object?, object?> valueSelector)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        Error.ThrowIfArgumentNull(nameof(keySelector), keySelector);
        Error.ThrowIfArgumentNull(nameof(valueSelector), valueSelector);
        return ToHashtableInternal(
            source,
            keySelector,
            valueSelector,
            null);
    }

#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static Hashtable ToHashtable(
        this IEnumerable source,
        Func<object?, object> keySelector,
        IEqualityComparer? comparer)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        Error.ThrowIfArgumentNull(nameof(keySelector), keySelector);
        return ToHashtableInternal(
            source,
            keySelector,
            null,
            comparer);
    }

#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static Hashtable ToHashtable(
        this IEnumerable source,
        Func<object?, object> keySelector)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        Error.ThrowIfArgumentNull(nameof(keySelector), keySelector);
        return ToHashtableInternal(
            source,
            keySelector,
            null,
            null);
    }

    internal static Hashtable ToHashtableInternal(
        IEnumerable source,
        Func<object?, object> keySelector,
        Func<object?, object?>? valueSelector,
        IEqualityComparer? comparer)
    {
        valueSelector ??= static x => x;
        comparer ??= UnknownTypeEqualityComparer.Default;
        IEnumerator? iterator = null;
        try
        {
            iterator = source.GetEnumerator();
            var hashtable = new Hashtable(comparer);
            while (iterator.MoveNext())
            {
                hashtable.Add(
                    keySelector.Invoke(iterator.Current),
                    valueSelector.Invoke(iterator.Current));
            }
            return hashtable;
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
