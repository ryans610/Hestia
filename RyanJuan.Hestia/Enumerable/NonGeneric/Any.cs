using System.Collections;

namespace RyanJuan.Hestia.NonGeneric;

public static partial class HestiaNonGenericCollections
{
#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static bool Any(
        this IEnumerable source)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        if (source is ICollection collection)
        {
            return collection.Count != 0;
        }
        IEnumerator? iterator = null;
        try
        {
            iterator = source.GetEnumerator();
            return iterator.MoveNext();
        }
        finally
        {
            if (iterator is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }

#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static bool Any(
        this IEnumerable source,
        Func<object?, bool> predicate)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        IEnumerator? iterator = null;
        try
        {
            iterator = source.GetEnumerator();
            while (iterator.MoveNext())
            {
                if (predicate.Invoke(iterator.Current))
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
}
