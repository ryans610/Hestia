using System;
using System.Collections.Generic;
using System.Linq;

namespace RyanJuan.Hestia
{
    public static partial class HestiaCollections
    {
#if ZH_HANT
#else
#endif
        public static bool NotContains<TSource>(
            this IEnumerable<TSource> source,
            TSource value,
            IEqualityComparer<TSource>? comparer = null)
        {
            return comparer is null ?
                !source.Contains(value) :
                !source.Contains(value, comparer);
        }

#if ZH_HANT
#else
#endif
        public static bool NotContains<TSource>(
            this ICollection<TSource> source,
            TSource value)
        {
            return !source.Contains(value);
        }
    }
}
