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
            if (source is ICollection<TSource> collectionT)
            {
                return collectionT.Count == 0;
            }
#if !NET40
            if (source is IReadOnlyCollection<TSource> readOnlyCollectionT)
            {
                return readOnlyCollectionT.Count == 0;
            }
#endif
            if (source is ICollection collection)
            {
                return collection.Count == 0;
            }
            using var iterator = source.GetEnumerator();
            return !iterator.MoveNext();
        }
    }
}
