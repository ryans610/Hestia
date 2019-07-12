using System;
using System.Collections.Generic;
using System.Linq;

namespace RyanJuan.Hestia
{
    public static partial class HestiaCollections
    {
#if ZH_HANT
        /// <summary>
        /// 串連兩個或多個陣列。
        /// </summary>
        /// <typeparam name="TSource">輸入陣列之項目的類型。</typeparam>
        /// <param name="first">要串連的第一個陣列。</param>
        /// <param name="others">要串連到第一個陣列的其他陣列。</param>
        /// <returns>
        /// <typeparamref name="TSource"/>[]，其中包含多個輸入陣列的串連項目。
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="first"/> 或 <paramref name="others"/> 的值為 <see langword="null"/>，
        /// 或是 <paramref name="others"/> 中任何一個項目為 <see langword="null"/>。
        /// </exception>
#else
        /// <summary>
        /// Concatenates two or many arrays.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input arrays.</typeparam>
        /// <param name="first">The first array to concatenate.</param>
        /// <param name="others">The arrays to concatenate to the first array.</param>
        /// <returns>
        /// An <typeparamref name="TSource"/>[] that contains the concatenated elements of
        /// many input arrays.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="first"/> or <paramref name="others"/> is <see langword="null"/>,
        /// or any item in <paramref name="others"/> is <see langword="null"/>.
        /// </exception>
#endif
        public static TSource[] Concat<TSource>(
            this TSource[] first,
            params TSource[][] others)
        {
            if (first is null)
            {
                throw Error.ArgumentNull(nameof(first));
            }
            if (others is null ||
                others.Any(x => x is null))
            {
                throw Error.ArgumentNull(nameof(others));
            }
            var result = new TSource[first.Length + others.Sum(x => x.Length)];
            first.CopyTo(result, 0);
            int offset = first.Length;
            foreach (var arr in others)
            {
                arr.CopyTo(result, offset);
                offset += arr.Length;
            }
            return result;
        }
    }
}
