using System.Collections;

namespace RyanJuan.Hestia.NonGeneric;

public static partial class HestiaNonGenericCollections
{
    [PublicAPI]
    [LinqTunnel]
    public static IEnumerable Select(
        this IEnumerable source,
        Func<object?, object?> selector)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        Error.ThrowIfArgumentNull(nameof(selector), selector);
        IEnumerator? iterator = null;
        try
        {
            iterator = source.GetEnumerator();
            while (iterator.MoveNext())
            {
                yield return selector.Invoke(iterator.Current);
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

    [PublicAPI]
    [LinqTunnel]
    public static IEnumerable Select(
        this IEnumerable source,
        Func<object?, int, object?> selector)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        Error.ThrowIfArgumentNull(nameof(selector), selector);
        IEnumerator? iterator = null;
        try
        {
            iterator = source.GetEnumerator();
            int index = 0;
            while (iterator.MoveNext())
            {
                yield return selector.Invoke(iterator.Current, index);
                index += 1;
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
