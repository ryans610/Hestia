using System.Collections;

namespace RyanJuan.Hestia.NonGeneric;

public static partial class HestiaNonGenericCollections
{
#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static int Count(
        this IEnumerable source)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        if (source is ICollection collection)
        {
            return collection.Count;
        }
        IEnumerator? iterator = null;
        try
        {
            int count = 0;
            iterator = source.GetEnumerator();
            checked
            {
                while (iterator.MoveNext())
                {
                    count += 1;
                }
            }
            return count;
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
    public static int Count(
        this IEnumerable source,
        Func<object?, bool> predicate)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        Error.ThrowIfArgumentNull(nameof(predicate), predicate);
        IEnumerator? iterator = null;
        try
        {
            iterator = source.GetEnumerator();
            int count = 0;
            checked
            {
                while (iterator.MoveNext())
                {
                    if (predicate.Invoke(iterator.Current))
                    {
                        count += 1;
                    }
                }
            }
            return count;
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
    public static long LongCount(
        IEnumerable source)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        if (source is ICollection collection)
        {
            return collection.Count;
        }
        IEnumerator? iterator = null;
        try
        {
            iterator = source.GetEnumerator();
            long count = 0;
            checked
            {
                while (iterator.MoveNext())
                {
                    count += 1L;
                }
            }
            return count;
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
    public static long LongCount(
        this IEnumerable source,
        Func<object?, bool> predicate)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        Error.ThrowIfArgumentNull(nameof(predicate), predicate);
        IEnumerator? iterator = null;
        try
        {
            iterator = source.GetEnumerator();
            long count = 0;
            checked
            {
                while (iterator.MoveNext())
                {
                    if (predicate.Invoke(iterator.Current))
                    {
                        count += 1L;
                    }
                }
            }
            return count;
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
