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
        public static TSource[] Copy<TSource>(this TSource[] source)
        {
            Error.ThrowIfArgumentNull(nameof(source), source);
            var array = new TSource[source.Length];
            source.CopyTo(array, 0);
            return array;
        }
    }
}
