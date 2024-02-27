namespace RyanJuan.Hestia;

public static partial class HestiaCollections
{
#if ZH_HANT
    /// <summary>
    /// 將所有 <see cref="ConcurrentBag{T}"/> 中的元素移除。
    /// </summary>
    /// <typeparam name="TSource">
    /// <see cref="ConcurrentBag{T}"/> 中元素的型別。
    /// </typeparam>
    /// <param name="source">指定的 <see cref="ConcurrentBag{T}"/>。</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> 的值為 <see langword="null"/>。
    /// </exception>
#else
    /// <summary>
    /// Remove all elements in the <see cref="ConcurrentBag{T}"/>.
    /// </summary>
    /// <typeparam name="TSource">
    /// The type of the elements in the <see cref="ConcurrentBag{T}"/>.
    /// </typeparam>
    /// <param name="source">The specific <see cref="ConcurrentBag{T}"/>.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> is <see langword="null"/>.
    /// </exception>
#endif
    [PublicAPI]
    public static void Clear<TSource>(
        this ConcurrentBag<TSource> source)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        while (source.TryTake(out _)) { }
    }
}
