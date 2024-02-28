using System.Collections;

namespace RyanJuan.Hestia.NonGeneric;

public static partial class HestiaNonGenericCollections
{
    [PublicAPI]
    [LinqTunnel]
    public static IEnumerable Where(
        this IEnumerable source,
        Func<object?, bool> predicate)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        Error.ThrowIfArgumentNull(nameof(predicate), predicate);
        IEnumerator? iterator = null;
        try
        {
            iterator = source.GetEnumerator();
            while (iterator.MoveNext())
            {
                if (predicate.Invoke(iterator.Current))
                {
                    yield return iterator.Current;
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
