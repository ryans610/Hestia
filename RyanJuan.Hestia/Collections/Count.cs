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
        /// <param name="source"></param>
        /// <returns></returns>
#endif
        public static int Count(
            this IEnumerable source)
        {
            if (source is null)
            {
                throw Error.ArgumentNull(nameof(source));
            }
            if (source is ICollection collection)
            {
                return collection.Count;
            }
            int count = 0;
            var iterator = source.GetEnumerator();
            checked
            {
                while (iterator.MoveNext())
                {
                    count++;
                }
            }
            if (iterator is IDisposable disposable)
            {
                disposable.Dispose();
            }
            return count;
        }

        public static long LongCount(
            IEnumerable source)
        {
            if (source is null)
            {
                throw Error.ArgumentNull(nameof(source));
            }
            if (source is ICollection collection)
            {
                return collection.Count;
            }
            long count = 0;
            var iterator = source.GetEnumerator();
            checked
            {
                while (iterator.MoveNext())
                {
                    count++;
                }
            }
            if (iterator is IDisposable disposable)
            {
                disposable.Dispose();
            }
            return count;
        }
    }
}
