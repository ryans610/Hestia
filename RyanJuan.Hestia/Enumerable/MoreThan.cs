using System.Collections;

namespace RyanJuan.Hestia;

public static partial class HestiaEnumerable
{
#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static bool MoreThan<TSource>(
        this IEnumerable<TSource> source,
        int number)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        Error.ThrowIfArgumentSmallerThanZero(nameof(number), number);
        return MoreThanInternal(source, number);
    }

    internal static bool MoreThanInternal<TSource>(
        this IEnumerable<TSource> source,
        int number)
    {
        switch (source)
        {
            case ICollection<TSource> collectionT:
                return collectionT.Count > number;
#if !NET40
            case IReadOnlyCollection<TSource> readOnlyCollection:
                return readOnlyCollection.Count > number;
#endif
            case ICollection collection:
                return collection.Count > number;
        }
        int count = 0;
        using var iterator = source.GetEnumerator();
        while (iterator.MoveNext())
        {
            count += 1;
            if (count > number)
            {
                return true;
            }
        }
        return false;
    }
}
