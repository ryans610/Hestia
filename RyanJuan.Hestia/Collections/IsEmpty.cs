using System.Collections;

namespace RyanJuan.Hestia;

public static partial class HestiaCollections
{
#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
#endif
    public static bool IsEmpty<TSource>(
        this IEnumerable<TSource> source)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        return IsEmptyInternal(source);
    }

    internal static bool IsEmptyInternal<TSource>(
        IEnumerable<TSource> source)
    {
        switch (source)
        {
            case ICollection<TSource> collectionT:
                return collectionT.Count == 0;
#if !NET40
            case IReadOnlyCollection<TSource> readOnlyCollectionT:
                return readOnlyCollectionT.Count == 0;
#endif
            case ICollection collection:
                return collection.Count == 0;
            default:
                {
                    using var iterator = source.GetEnumerator();
                    return !iterator.MoveNext();
                }
        }
    }
}
