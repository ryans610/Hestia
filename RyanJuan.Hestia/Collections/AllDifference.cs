namespace RyanJuan.Hestia;

public static partial class HestiaCollections
{
#if ZH_HANT
    /// <summary>
    /// 判斷序列的所有項目是否全部相異。
    /// </summary>
    /// <typeparam name="TSource">
    /// <paramref name="source"/> 項目的類型。
    /// </typeparam>
    /// <param name="source">
    /// <see cref="IEnumerable{T}"/>，其中包含要檢查相異的項目。
    /// </param>
    /// <param name="comparer">用來比較值的 <see cref="IEqualityComparer{T}"/>。</param>
    /// <returns>
    /// 如果來源序列的每個項目的值都相異，或序列是空的，則為 <see langword="true"/>，
    /// 否則為 <see langword="false"/>。
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> 的值為 <see langword="null"/>。
    /// </exception>
#else
    /// <summary>
    /// Determines whether all elements of a sequence has the difference value.
    /// </summary>
    /// <typeparam name="TSource">
    /// The type of the elements of <paramref name="source"/>.
    /// </typeparam>
    /// <param name="source">
    /// An <see cref="IEnumerable{T}"/> that contains the elements to check for difference.
    /// </param>
    /// <param name="comparer">An <see cref="IEqualityComparer{T}"/> to compare values.</param>
    /// <returns>
    /// <see langword="true"/> if every element of the source sequence has the difference value,
    /// or if the sequence is empty;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> is <see langword="null"/>.
    /// </exception>
#endif
    [PublicAPI]
    public static bool AllDifference<TSource>(
        this IEnumerable<TSource> source,
        IEqualityComparer<TSource>? comparer)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        using var iterator = source.GetEnumerator();
        if (!iterator.MoveNext())
        {
            return true;
        }
        comparer ??= EqualityComparer<TSource>.Default;
        var hashSet = new HashSet<TSource>(comparer)
        {
            iterator.Current,
        };
        while (iterator.MoveNext())
        {
            var current = iterator.Current;
            if (hashSet.Contains(current))
            {
                return false;
            }
            hashSet.Add(current);
        }
        return true;
    }

#if ZH_HANT
    /// <summary>
    /// 判斷序列的所有項目是否全部相異。
    /// </summary>
    /// <typeparam name="TSource">
    /// <paramref name="source"/> 項目的類型。
    /// </typeparam>
    /// <param name="source">
    /// <see cref="IEnumerable{T}"/>，其中包含要檢查相異的項目。
    /// </param>
    /// <returns>
    /// 如果來源序列的每個項目的值都相異，或序列是空的，則為 <see langword="true"/>，
    /// 否則為 <see langword="false"/>。
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> 的值為 <see langword="null"/>。
    /// </exception>
#else
    /// <summary>
    /// Determines whether all elements of a sequence has the difference value.
    /// </summary>
    /// <typeparam name="TSource">
    /// The type of the elements of <paramref name="source"/>.
    /// </typeparam>
    /// <param name="source">
    /// An <see cref="IEnumerable{T}"/> that contains the elements to check for difference.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if every element of the source sequence has the difference value,
    /// or if the sequence is empty;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> is <see langword="null"/>.
    /// </exception>
#endif
    [PublicAPI]
    public static bool AllDifference<TSource>(
        this IEnumerable<TSource> source)
    {
        return source.AllDifference(null);
    }
}
