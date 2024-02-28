namespace RyanJuan.Hestia;

public static partial class Hestia
{
#if ZH_HANT
    /// <summary>
    /// 將 <paramref name="t"/> 包裹為 <see cref="IEnumerable{T}"/>。
    /// </summary>
    /// <typeparam name="TSource"><paramref name="t"/>的型別。</typeparam>
    /// <param name="t">要包裹的物件。</param>
    /// <returns>包裹 <paramref name="t"/> 的 <see cref="IEnumerable{T}"/>。</returns>
#else
    /// <summary>
    /// Wrap <paramref name="t"/> into an <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="TSource">Type of <paramref name="t"/>.</typeparam>
    /// <param name="t">The object to wrap.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> wrapping <paramref name="t"/>.</returns>
#endif
    [PublicAPI]
    public static IEnumerable<TSource> ToEnumerable<TSource>(
        this TSource t)
    {
        return [t];
    }
}
