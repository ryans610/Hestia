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
        /// <param name="separator"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
#endif
        public static IEnumerable<IEnumerable<TSource>> Split<TSource>(
            this IEnumerable<TSource> source,
            TSource separator,
            IEqualityComparer<TSource> comparer)
        {
            if (source is null)
            {
                throw Error.ArgumentNull(nameof(source));
            }
            if (comparer is null)
            {
                comparer = EqualityComparer<TSource>.Default;
            }
            List<TSource> buffer = null;
            using (var iterator = source.GetEnumerator())
            {
                while (iterator.MoveNext())
                {
                    if (buffer is null)
                    {
                        buffer = new List<TSource>();
                    }
                    if (comparer.Equals(separator, iterator.Current))
                    {
                        yield return buffer.Skip(0);
                        buffer = null;
                        continue;
                    }
                    buffer.Add(iterator.Current);
                }
                if (buffer?.Count > 0)
                {
                    yield return buffer.Skip(0);
                }
            }
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
#endif
        public static IEnumerable<IEnumerable<TSource>> Split<TSource>(
            this IEnumerable<TSource> source,
            TSource separator)
        {
            return source.Split(separator, null);
        }
    }
}
