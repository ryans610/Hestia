using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RyanJuan.Hestia
{
    public static partial class HestiaCollections
    {
#if ZH_HANT
#else
#endif
        public static bool MoreThan<TSource>(
            this IEnumerable<TSource> source,
            int number)
        {
            Error.ThrowIfArgumentNull(nameof(source), source);
            Error.ThrowIfArgumentSmallerThanZero(nameof(number), number);
            if (source is ICollection<TSource> collectionT)
            {
                return collectionT.Count > number;
            }
#if !NET40
            if (source is IReadOnlyCollection<TSource> readOnlyCollection)
            {
                return readOnlyCollection.Count > number;
            }
#endif
            if (source is ICollection collection)
            {
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
}
