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
        public static bool Any(
            this IEnumerable source)
        {
            Error.ThrowIfArgumentNull(nameof(source), source);
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

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
#endif
        public static bool Any(
            this IEnumerable source,
            Func<object?, bool> predicate)
        {
            Error.ThrowIfArgumentNull(nameof(source), source);
            var result = false;
            var iterator = source.GetEnumerator();
            while (iterator.MoveNext())
            {
                if (predicate(iterator.Current))
                {
                    result = true;
                    break;
                }
            }
            if (iterator is IDisposable disposable)
            {
                disposable.Dispose();
            }
            return result;
        }
    }
}
