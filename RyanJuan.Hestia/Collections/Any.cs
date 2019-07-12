using System;
using System.Collections;
using System.Collections.Generic;

namespace RyanJuan.Hestia
{
    public static partial class HestiaCollections
    {
#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
#endif
        public static bool Any(
            this IEnumerable source)
        {
            if (source is null)
            {
                throw Error.ArgumentNull(nameof(source));
            }
            if (source is ICollection collection)
            {
                return collection.Count != 0;
            }
            var iterator = source.GetEnumerator();
            var result = iterator.MoveNext();
            if (iterator is IDisposable disposable)
            {
                disposable.Dispose();
            }
            return result;
        }
    }
}
