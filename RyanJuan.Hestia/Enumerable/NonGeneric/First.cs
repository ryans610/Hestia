using System.Collections;

namespace RyanJuan.Hestia.NonGeneric;

public static partial class HestiaNonGenericCollections
{
#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static object? First(
        this IEnumerable source,
        Predicate<object?> predicate)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        Error.ThrowIfArgumentNull(nameof(predicate), predicate);
        return FirstInternal(
            source,
            predicate,
            () => throw CreateFirstItemNotFoundException());
    }

    [PublicAPI]
    public static object? First(
        this IEnumerable source)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        return FirstInternal(
            source,
            static _ => true,
            () => throw CreateFirstItemNotFoundException());
    }

    internal static object? FirstInternal(
        IEnumerable source,
        Predicate<object?> predicate,
        Func<object?> notFoundHandler)
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

            return notFoundHandler.Invoke();
        }
        finally
        {
            if (iterator is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }

    private static InvalidOperationException CreateFirstItemNotFoundException()
    {
        return new InvalidOperationException();
    }
}
