using System.Collections;

namespace RyanJuan.Hestia.NonGeneric;

public static partial class HestiaNonGenericCollections
{
#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static IEnumerable Concat(
        this IEnumerable first,
        IEnumerable second)
    {
        Error.ThrowIfArgumentNull(nameof(first), first);
        Error.ThrowIfArgumentNull(nameof(second), second);
        if (first is ICollection firstCollection &&
            second is ICollection secondCollection)
        {
            var result = new ArrayList(firstCollection.Count + secondCollection.Count);
            result.AddRange(firstCollection);
            result.AddRange(secondCollection);
            return result;
        }
        return ConcatEnumerateInternal(first, second);
    }

    private static IEnumerable ConcatEnumerateInternal(
        IEnumerable first,
        IEnumerable second)
    {
        IEnumerator? firstIterator = null;
        IEnumerator? secondIterator = null;
        try
        {
            firstIterator = first.GetEnumerator();
            while (firstIterator.MoveNext())
            {
                yield return firstIterator.Current;
            }
            try
            {
                secondIterator = second.GetEnumerator();
                while (secondIterator.MoveNext())
                {
                    yield return secondIterator.Current;
                }
            }
            finally
            {
                if (secondIterator is IDisposable secondDisposable)
                {
                    secondDisposable.Dispose();
                }
            }
        }
        finally
        {
            if (firstIterator is IDisposable firstDisposable)
            {
                firstDisposable.Dispose();
            }
        }
    }
}
