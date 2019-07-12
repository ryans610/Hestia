using System;
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
        /// <param name="capacity"></param>
        /// <returns></returns>
#endif
        public static List<TSource> ToList<TSource>(
            this IEnumerable<TSource> source,
            int capacity)
        {
            if (source is null)
            {
                throw Error.ArgumentNull(nameof(source));
            }
            if (capacity < 0)
            {
                throw Error.ArgumentOutOfRange(
                    nameof(capacity),
                    $"{nameof(capacity)} is less than 0.",
                    capacity);
            }
            var list = new List<TSource>(capacity);
            list.AddRange(source);
            return list;
        }
    }
}
