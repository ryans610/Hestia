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
        /// <param name="batchSize"></param>
        /// <returns></returns>
#endif
        public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(
            this IEnumerable<TSource> source,
            int batchSize)
        {
            if (source is null)
            {
                throw Error.ArgumentNull(nameof(source));
            }
            if (batchSize <= 0)
            {
                throw Error.ArgumentOutOfRange(
                    nameof(batchSize),
                    $"{nameof(batchSize)} is less than or equals to 0.",
                    batchSize);
            }
            TSource[] buffer = null;
            int count = 0;
            using (var iterator = source.GetEnumerator())
            {
                while (iterator.MoveNext())
                {
                    if (buffer is null)
                    {
                        buffer = new TSource[batchSize];
                    }
                    buffer[count] = iterator.Current;
                    count++;
                    if (count == batchSize)
                    {
                        yield return buffer.Skip(0);
                        count = 0;
                        buffer = null;
                    }
                }
                if (count > 0)
                {
                    yield return buffer.Take(count);
                }
            }
        }
    }
}
