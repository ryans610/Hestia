using System.Collections;

using RyanJuan.Hestia.Resources;

namespace RyanJuan.Hestia.NonGeneric;

public static partial class HestiaNonGenericCollections
{
#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static bool Contains(
        this IEnumerable source,
        object value,
        IEqualityComparer? comparer)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        comparer ??= UnknownTypeEqualityComparer.Default;
        IEnumerator? iterator = null;
        try
        {
            iterator = source.GetEnumerator();
            while (iterator.MoveNext())
            {
                if (comparer.Equals(iterator.Current, value))
                {
                    return true;
                }
            }
        }
        finally
        {
            if (iterator is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
        return false;
    }

#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static bool Contains(
        this IEnumerable source,
        object value)
    {
        return source.Contains(value, null);
    }
}
