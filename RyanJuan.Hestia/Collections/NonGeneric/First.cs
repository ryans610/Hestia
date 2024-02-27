using System.Collections;

namespace RyanJuan.Hestia.NonGeneric;

public static partial class HestiaNonGenericCollections
{
#if ZH_HANT
#else
#endif
    public static object? First(
        this IEnumerable source,
        Predicate<object?> predicate)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        Error.ThrowIfArgumentNull(nameof(predicate), predicate);
        return FirstInternal(source, predicate);
    }

    public static object? First(
        this IEnumerable source)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        return FirstInternal(source, static _ => true);
    }

    internal static object? FirstInternal(
        IEnumerable source,
        Predicate<object?> predicate)
    {
        IEnumerator iterator = null;
        try
        {
            iterator = source.GetEnumerator();
            while (iterator.MoveNext())
            {
                if (predicate.Invoke(iterator.Current))
                {
                    return iterator.Current;
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
    }
}
