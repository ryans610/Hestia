using System;
using System.Collections.Generic;
using System.Linq;

namespace RyanJuan.Hestia
{
    public static partial class HestiaCollections
    {
#if ZH_HANT
        /// <summary>
        /// 在 <see cref="IDictionary{TKey, TValue}"/> 中加入多個
        /// <see cref="KeyValuePair{TKey, TValue}"/>。
        /// </summary>
        /// <typeparam name="TKey">
        /// <paramref name="dictionary"/> 索引值的型別。
        /// </typeparam>
        /// <typeparam name="TValue">
        /// <paramref name="dictionary"/> 值的型別。
        /// </typeparam>
        /// <param name="dictionary">
        /// 要被加入 <see cref="KeyValuePair{TKey, TValue}"/> 的
        /// <see cref="IDictionary{TKey, TValue}"/>。
        /// </param>
        /// <param name="values">
        /// 要加入 <paramref name="dictionary"/> 的多個
        /// <see cref="KeyValuePair{TKey, TValue}"/>。
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dictionary"/> 或 <paramref name="values"/> 的值為
        /// <see langword="null"/>。
        /// </exception>
#else
        /// <summary>
        /// Adds the multiple <see cref="KeyValuePair{TKey, TValue}"/> to the
        /// <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of keys in the <paramref name="dictionary"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of values in the <paramref name="dictionary"/>.
        /// </typeparam>
        /// <param name="dictionary">
        /// The <see cref="IDictionary{TKey, TValue}"/> for adding.
        /// </param>
        /// <param name="values">
        /// The key value pairs to add.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dictionary"/> or <paramref name="values"/>
        /// is <see langword="null"/>.
        /// </exception>
#endif
        public static void AddRange<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            IEnumerable<KeyValuePair<TKey, TValue>> values)
        {
            if (dictionary is null)
            {
                throw Error.ArgumentNull(nameof(dictionary));
            }
            if (values is null)
            {
                throw Error.ArgumentNull(nameof(values));
            }
            foreach (var pair in values)
            {
                dictionary.Add(pair);
            }
        }

#if ZH_HANT
        /// <summary>
        /// 在 <see cref="IDictionary{TKey, TValue}"/> 中加入多個項目。
        /// </summary>
        /// <typeparam name="TSource">
        /// <paramref name="source"/> 項目的類型。
        /// </typeparam>
        /// <typeparam name="TKey">
        /// <paramref name="dictionary"/> 索引值的型別。
        /// </typeparam>
        /// <param name="dictionary">
        /// 要被加入值的 <see cref="IDictionary{TKey, TValue}"/>。
        /// </param>
        /// <param name="source">
        /// 用來加入 <see cref="IDictionary{TKey, TValue}"/> 的來源 <see cref="IEnumerable{T}"/>。
        /// </param>
        /// <param name="keySelector">用來從各個項目擷取索引鍵的函式。</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dictionary"/>、<paramref name="source"/> 或
        /// <paramref name="keySelector"/> 的值為 <see langword="null"/>。
        /// </exception>
#else
        /// <summary>
        /// Adds the multiple values to the <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of <paramref name="source"/>.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of keys in the <paramref name="dictionary"/>.
        /// </typeparam>
        /// <param name="dictionary">
        /// The <see cref="IDictionary{TKey, TValue}"/> for adding.
        /// </param>
        /// <param name="source">
        /// An enumerable of values to add.
        /// </param>
        /// <param name="keySelector">
        /// A function to extract a key from each element.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dictionary"/>, <paramref name="source"/> or
        /// <paramref name="keySelector"/> is <see langword="null"/>.
        /// </exception>
#endif
        public static void AddRange<TSource, TKey>(
            this IDictionary<TKey, TSource> dictionary,
            IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            if (dictionary is null)
            {
                throw Error.ArgumentNull(nameof(dictionary));
            }
            if (source is null)
            {
                throw Error.ArgumentNull(nameof(source));
            }
            if (keySelector is null)
            {
                throw Error.ArgumentNull(nameof(keySelector));
            }
            foreach (var value in source)
            {
                dictionary.Add(keySelector(value), value);
            }
        }

#if ZH_HANT
        /// <summary>
        /// 在 <see cref="IDictionary{TKey, TValue}"/> 中加入多個項目。
        /// </summary>
        /// <typeparam name="TSource">
        /// <paramref name="source"/> 項目的類型。
        /// </typeparam>
        /// <typeparam name="TKey">
        /// <paramref name="dictionary"/> 索引值的型別。
        /// </typeparam>
        /// <typeparam name="TElement">
        /// <paramref name="dictionary"/> 值的型別。
        /// </typeparam>
        /// <param name="dictionary">
        /// 要被加入值的 <see cref="IDictionary{TKey, TValue}"/>。
        /// </param>
        /// <param name="source">
        /// 用來加入 <see cref="IDictionary{TKey, TValue}"/> 的來源 <see cref="IEnumerable{T}"/>。
        /// </param>
        /// <param name="keySelector">用來從各個項目擷取索引鍵的函式。</param>
        /// <param name="elementSelector">用來從每個項目產生結果項目值的轉換函式。</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dictionary"/>、<paramref name="source"/>、
        /// <paramref name="keySelector"/> 或 <paramref name="elementSelector"/>
        /// 的值為 <see langword="null"/>。
        /// </exception>
#else
        /// <summary>
        /// Adds the multiple values to the <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of <paramref name="source"/>.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of keys in the <paramref name="dictionary"/>.
        /// </typeparam>
        /// <typeparam name="TElement">
        /// The type of values in the <paramref name="dictionary"/>.
        /// </typeparam>
        /// <param name="dictionary">
        /// The <see cref="IDictionary{TKey, TValue}"/> for adding.
        /// </param>
        /// <param name="source">
        /// An enumerable of values to add.
        /// </param>
        /// <param name="keySelector">
        /// A function to extract a key from each element.
        /// </param>
        /// <param name="elementSelector">
        /// A transform function to produce a result element value from each element.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dictionary"/>, <paramref name="source"/>, <paramref name="keySelector"/>
        /// or <paramref name="elementSelector"/>
        /// is <see langword="null"/>.
        /// </exception>
#endif
        public static void AddRange<TSource, TKey, TElement>(
            this IDictionary<TKey, TElement> dictionary,
            IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector)
        {
            if (dictionary is null)
            {
                throw Error.ArgumentNull(nameof(dictionary));
            }
            if (source is null)
            {
                throw Error.ArgumentNull(nameof(source));
            }
            if (keySelector is null)
            {
                throw Error.ArgumentNull(nameof(keySelector));
            }
            if (elementSelector is null)
            {
                throw Error.ArgumentNull(nameof(elementSelector));
            }
            foreach (var value in source)
            {
                dictionary.Add(keySelector(value), elementSelector(value));
            }
        }
    }
}
