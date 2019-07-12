using System;
using System.Collections.Generic;
using System.Linq;

namespace RyanJuan.Hestia
{
    public static partial class HestiaCollections
    {
#if ZH_HANT
        /// <summary>
        /// 判斷序列的所有項目是否全部相等。
        /// </summary>
        /// <typeparam name="TSource">
        /// <paramref name="source"/> 項目的類型。
        /// </typeparam>
        /// <param name="source">
        /// <see cref="IEnumerable{T}"/>，其中包含要檢查相等的項目。
        /// </param>
        /// <param name="comparer">用來比較值的 <see cref="IEqualityComparer{T}"/>。</param>
        /// <returns>
        /// 如果來源序列的每個項目的值都相等，或序列是空的，則為 <see langword="true"/>，
        /// 否則為 <see langword="false"/>。
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> 的值為 <see langword="null"/>。
        /// </exception>
#else
        /// <summary>
        /// Determines whether all elements of a sequence has the same value.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">
        /// An <see cref="IEnumerable{T}"/> that contains the elements to check for equality.
        /// </param>
        /// <param name="comparer">An <see cref="IEqualityComparer{T}"/> to compare values.</param>
        /// <returns>
        /// <see langword="true"/> if every element of the source sequence has the same value,
        /// or if the sequence is empty;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is <see langword="null"/>.
        /// </exception>
#endif
        public static bool AllEquals<TSource>(
            this IEnumerable<TSource> source,
            IEqualityComparer<TSource> comparer)
        {
            if (source is null)
            {
                throw Error.ArgumentNull(nameof(source));
            }
            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                {
                    return true;
                }
                if (comparer is null)
                {
                    comparer = EqualityComparer<TSource>.Default;
                }
                var value = iterator.Current;
                while (iterator.MoveNext())
                {
                    if (!comparer.Equals(value, iterator.Current))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

#if ZH_HANT
        /// <summary>
        /// 判斷序列的所有項目是否全部相等。
        /// </summary>
        /// <typeparam name="TSource">
        /// <paramref name="source"/> 項目的類型。
        /// </typeparam>
        /// <param name="source">
        /// <see cref="IEnumerable{T}"/>，其中包含要檢查相等的項目。
        /// </param>
        /// <returns>
        /// 如果來源序列的每個項目的值都相等，或序列是空的，則為 <see langword="true"/>，
        /// 否則為 <see langword="false"/>。
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> 的值為 <see langword="null"/>。
        /// </exception>
#else
        /// <summary>
        /// Determines whether all elements of a sequence has the same value.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">
        /// An <see cref="IEnumerable{T}"/> that contains the elements to check for equality.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if every element of the source sequence has the same value,
        /// or if the sequence is empty;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is <see langword="null"/>.
        /// </exception>
#endif
        public static bool AllEquals<TSource>(
            this IEnumerable<TSource> source)
        {
            return source.AllEquals(null);
        }
    }
}
