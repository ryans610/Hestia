namespace RyanJuan.Hestia;

public static partial class HestiaEnumerable
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
#if NET6_0_OR_GREATER
    [Obsolete("Use native Enumerable.Chunk<TSource>(IEnumerable<TSource>, Int32) instead.", false)]
#endif
    [PublicAPI]
    [LinqTunnel]
    public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(
        this IEnumerable<TSource> source,
        int batchSize)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        if (batchSize <= 0)
        {
            throw Error.ArgumentOutOfRange(
                nameof(batchSize),
                $"{nameof(batchSize)} is less than or equals to 0.",
                batchSize);
        }
#if NET6_0_OR_GREATER
        return source.Chunk(batchSize);
#else
        return BatchInternal(source, batchSize);
#endif
    }

#if !NET6_0_OR_GREATER
    [LinqTunnel]
    internal static IEnumerable<IEnumerable<TSource>> BatchInternal<TSource>(
        IEnumerable<TSource> source,
        int batchSize)
    {
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        if (source is TSource[] array)
        {
            int start = 0;
            while (start < array.Length)
            {
                int end = start + batchSize;
                if (end > array.Length)
                {
                    end = array.Length;
                }

                yield return array[start..end].Skip(0);
                start += batchSize;
            }

            yield break;
        }
#endif
        TSource[]? buffer = null;
        int count = 0;
        // ReSharper disable once PossibleMultipleEnumeration
        // will not happen
        using var iterator = source.GetEnumerator();
        while (iterator.MoveNext())
        {
            buffer ??= new TSource[batchSize];
            buffer[count] = iterator.Current;
            count += 1;
            if (count == batchSize)
            {
                yield return buffer.Skip(0);
                count = 0;
                buffer = null;
            }
        }
        if (count > 0)
        {
            // when buffer is null, count is always 0.
            yield return buffer!.Take(count);
        }
    }
#endif
}
