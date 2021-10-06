using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace RyanJuan.Hestia
{
    public static partial class HestiaCollections
    {
#if ZH_HANT
        /// <summary>
        /// 嘗試取得與 <paramref name="dictionary"/> 中所指定
        /// <paramref name="key"/> 建立關聯的值。
        /// </summary>
        /// <typeparam name="TKey">字典中索引鍵的類型。</typeparam>
        /// <typeparam name="TValue">字典中值的類型。</typeparam>
        /// <param name="dictionary">
        /// 具有 <typeparamref name="TKey"/> 類型索引鍵和 
        /// <typeparamref name="TValue"/> 類型值的字典。
        /// </param>
        /// <param name="key">要取得之值的索引鍵。</param>
        /// <param name="defaultValue">
        /// 當 <paramref name="dictionary"/> 找不到與所指定 
        /// <paramref name="key"/> 建立關聯的值時，所要傳回的預設值。
        /// </param>
        /// <returns>
        /// <typeparamref name="TValue"/> 執行個體。
        /// 當方法成功時，傳回之物件會是與所指定 <paramref name="key"/>
        /// 建立關聯的值。 當方法失敗時，會傳回 <paramref name="defaultValue"/>。
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dictionary"/> 為 <see langword="null"/>。
        /// </exception>
#else
        /// <summary>
        /// Tries to get the value associated with the specified key in the
        /// <paramref name="dictionary"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">
        /// A dictionary with keys of type <typeparamref name="TKey"/>
        /// and values of type <typeparamref name="TValue"/>.
        /// </param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">
        /// The default value to return when the <paramref name="dictionary"/>
        /// cannot find a value associated with the specified 
        /// <paramref name="key"/>.
        /// </param>
        /// <returns>
        /// A <typeparamref name="TValue"/> instance.
        /// When the method is successful,
        /// the returned object is the value associated with the specified
        /// <paramref name="key"/>. When the method fails, it returns
        /// <paramref name="defaultValue"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dictionary"/> is <see langword="null"/>.
        /// </exception>
#endif
        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue)
            where TKey : notnull
        {
            Error.ThrowIfArgumentNull(nameof(dictionary), dictionary);
            return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
        }

#if ZH_HANT
        /// <summary>
        /// 嘗試取得與 <paramref name="dictionary"/> 中所指定
        /// <paramref name="key"/> 建立關聯的值。
        /// </summary>
        /// <typeparam name="TKey">字典中索引鍵的類型。</typeparam>
        /// <typeparam name="TValue">字典中值的類型。</typeparam>
        /// <param name="dictionary">
        /// 具有 <typeparamref name="TKey"/> 類型索引鍵和 
        /// <typeparamref name="TValue"/> 類型值的字典。
        /// </param>
        /// <param name="key">要取得之值的索引鍵。</param>
        /// <returns>
        /// <typeparamref name="TValue"/> 執行個體。
        /// 當方法成功時，傳回之物件會是與所指定 <paramref name="key"/>
        /// 建立關聯的值。 當方法失敗時，會傳回 <typeparamref name="TValue"/> 的
        /// <see langword="default"/> 值。
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dictionary"/> 為 <see langword="null"/>。
        /// </exception>
#else
        /// <summary>
        /// Tries to get the value associated with the specified key in the
        /// <paramref name="dictionary"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">
        /// A dictionary with keys of type <typeparamref name="TKey"/>
        /// and values of type <typeparamref name="TValue"/>.
        /// </param>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>
        /// A <typeparamref name="TValue"/> instance.
        /// When the method is successful,
        /// the returned object is the value associated with the specified
        /// <paramref name="key"/>. When the method fails, it returns
        /// the <see langword="default"/> value for 
        /// <typeparamref name="TValue"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dictionary"/> is <see langword="null"/>.
        /// </exception>
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [return: MaybeNull]
#endif
        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key)
            where TKey : notnull =>
            GetValueOrDefault(dictionary, key, default!);
    }
}
