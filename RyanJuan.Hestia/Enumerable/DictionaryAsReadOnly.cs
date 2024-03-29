namespace RyanJuan.Hestia;

public static partial class HestiaEnumerable
{
#if !NET40 && !NET7_0_OR_GREATER
#if ZH_HANT
    /// <summary>
    /// 將指定的 <see cref="IDictionary{TKey, TValue}"/> 包裝為
    /// <see cref="ReadOnlyDictionary{TKey, TValue}"/>。
    /// </summary>
    /// <typeparam name="TKey">
    /// <paramref name="dictionary"/> 索引值的型別。
    /// </typeparam>
    /// <typeparam name="TValue">
    /// <paramref name="dictionary"/> 值的型別。
    /// </typeparam>
    /// <param name="dictionary">
    /// 要包裝成 <see cref="ReadOnlyDictionary{TKey, TValue}"/> 的
    /// <see cref="IDictionary{TKey, TValue}"/>。
    /// </param>
    /// <returns>
    /// 以指定的 <see cref="IDictionary{TKey, TValue}"/> 包裝成的
    /// <see cref="ReadOnlyDictionary{TKey, TValue}"/>。
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="dictionary"/> 的值為 <see langword="null"/>。
    /// </exception>
#else
    /// <summary>
    /// Returns a read-only <see cref="ReadOnlyDictionary{TKey, TValue}"/> wrapper for
    /// the specific <see cref="IDictionary{TKey, TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey">
    /// The type of the key of <paramref name="dictionary"/>.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// The type of the value of <paramref name="dictionary"/>.
    /// </typeparam>
    /// <param name="dictionary">
    /// The <see cref="IDictionary{TKey, TValue}"/> to wrap as
    /// <see cref="ReadOnlyDictionary{TKey, TValue}"/>.
    /// </param>
    /// <returns>
    /// A <see cref="ReadOnlyDictionary{TKey, TValue}"/> that wrap around the specific
    /// <see cref="IDictionary{TKey, TValue}"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="dictionary"/> is <see langword="null"/>.
    /// </exception>
#endif
    [PublicAPI]
    public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary)
        where TKey : notnull
    {
        Error.ThrowIfArgumentNull(nameof(dictionary), dictionary);
        return new ReadOnlyDictionary<TKey, TValue>(dictionary);
    }
#endif
}
