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
            IEqualityComparer<TSource>? comparer)
        {
            return !source.Contains(value, comparer);
        }

#if ZH_HANT
#else
#endif
        public static bool NotContains<TSource>(
            this IEnumerable<TSource> source,
            TSource value)
        {
            return !source.Contains(value);
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
