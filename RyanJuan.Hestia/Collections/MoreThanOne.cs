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
        public static bool MoreThanOne<TSource>(
            this IEnumerable<TSource> source)
        {
            return source.MoreThan(1);
        }
    }
}
