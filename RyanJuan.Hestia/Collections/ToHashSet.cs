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
        public static HashSet<TSource> ToHashSet<TSource>(
            this IEnumerable<TSource> source)
        {
            Error.ThrowIfArgumentNull(nameof(source), source);
            return new HashSet<TSource>(source);
        }

#if ZH_HANT
#else
#endif
        public static HashSet<TSource> ToHashSet<TSource>(
            this IEnumerable<TSource> source,
            IEqualityComparer<TSource>? comparer)
        {
            Error.ThrowIfArgumentNull(nameof(source), source);
            return new HashSet<TSource>(source, comparer);
        }
    }
}
