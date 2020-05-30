using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static ReadOnlyCollection<TSource> ToReadOnlyCollection<TSource>(
            this IEnumerable<TSource> source)
        {
            Error.ThrowIfArgumentNull(nameof(source), source);
            return source is List<TSource> list ?
                list.AsReadOnly() :
                source.ToList().AsReadOnly();
        }
    }
}
