using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RyanJuan.Hestia
{
    public static partial class HestiaCollections
    {
#if ZH_HANT
#else
#endif
        public static string JoinAsString<TSource>(
            this IEnumerable<TSource> source,
            string separator)
        {
            return string.Join(separator, source);
        }

#if ZH_HANT
#else
#endif
        public static string JoinAsString<TSource>(
            this IEnumerable<TSource> source,
            char separator)
        {
#if NETCOREAPP3_0 || NETCOREAPP2_1 || NETSTANDARD2_1
            return string.Join(separator, source);
#else
            return source.JoinAsString(separator.ToString());
#endif
        }
    }
}
