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
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
#endif
        public static bool Contains(
            this IEnumerable source,
            object value,
            IEqualityComparer? comparer)
        {
            Error.ThrowIfArgumentNull(nameof(source), source);
            comparer ??= UnknownTypeEqualityComparer.Default;
            var iterator = source.GetEnumerator();
            var result = false;
            while (iterator.MoveNext())
            {
                if (comparer.Equals(iterator.Current, value))
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

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
#endif
        public static bool Contains(
            this IEnumerable source,
            object value)
        {
            return source.Contains(value, null);
        }
    }
}
