using System;
using System.Collections;

namespace RyanJuan.Hestia.NonGeneric
{
    public static partial class HestiaNonGenericCollections
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
            Error.ThrowIfArgumentNull(nameof(source), source);
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
                    count += 1;
                }
            }
            if (iterator is IDisposable disposable)
            {
                disposable.Dispose();
            }
            return count;
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
#endif
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
                    count += 1;
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
