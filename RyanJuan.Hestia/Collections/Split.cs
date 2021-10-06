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
        /// <param name="options"></param>
        /// <returns></returns>
#endif
        public static IEnumerable<IEnumerable<TSource>> Split<TSource>(
            this IEnumerable<TSource> source,
            TSource separator,
            IEqualityComparer<TSource>? comparer = null,
            EnumerableSplitOptions options = EnumerableSplitOptions.None)
        {
            Error.ThrowIfArgumentNull(nameof(source), source);
            comparer ??= EqualityComparer<TSource>.Default;
            List<TSource>? buffer = null;
            using var iterator = source.GetEnumerator();
            bool inFlag = false;
            while (iterator.MoveNext())
            {
                inFlag = true;
                buffer ??= new List<TSource>();
                if (comparer.Equals(separator, iterator.Current))
                {
                    if (options != EnumerableSplitOptions.RemoveEmptyEntries ||
                        buffer.Count > 0)
                    {
                        yield return buffer.Skip(0);
                    }
                    buffer = null;
                    continue;
                }
                buffer.Add(iterator.Current);
            }
            if (buffer is null)
            {
                //end with separator if inFlag
                if (inFlag &&
                    options != EnumerableSplitOptions.RemoveEmptyEntries)
                {
                    yield return Enumerable.Empty<TSource>();
                }
            }
            else if (buffer.Count > 0)
            {
                yield return buffer.Skip(0);
            }
        }
    }

    public enum EnumerableSplitOptions
    {
        None = 0,
        RemoveEmptyEntries = 1,
    }
}
