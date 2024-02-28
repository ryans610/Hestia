using System.Collections;

namespace RyanJuan.Hestia.NonGeneric;

public static partial class HestiaNonGenericCollections
{
#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static object? ElementAt(
        this IEnumerable source,
        int index)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        Error.ThrowIfArgumentSmallerThanZero(nameof(index), index);
        return ElementAtInternal(
            source,
            index,
            i => throw CreateIndexTooBigException(i));

        static ArgumentOutOfRangeException CreateIndexTooBigException(int index) => Error.ArgumentOutOfRange(
            nameof(index),
            string.Format(
                Error.ArgumentBiggerThanOrEqualTo,
                nameof(index),
                "number of elements in source"),
            index);
    }

    internal static object? ElementAtInternal(
        IEnumerable source,
        int index,
        Func<int, object?> outOfRangeHandler)
    {
        if (source is IList list)
        {
            if (index >= list.Count)
            {
                return outOfRangeHandler.Invoke(index);
            }
            return list[index];
        }
        IEnumerator? iterator = null;
        try
        {
            iterator = source.GetEnumerator();
            int current = 0;
            while (iterator.MoveNext())
            {
                if (current == index)
                {
                    return iterator.Current;
                }
                current += 1;
            }
            return outOfRangeHandler.Invoke(index);
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
