using System;
using System.Diagnostics.CodeAnalysis;

namespace RyanJuan.Hestia
{
    public static partial class HestiaString
    {
#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
#endif
        public static bool IsNullOrEmpty(
#if NETCOREAPP3_0 || NETSTANDARD2_1
            [NotNullWhen(false)]
#endif
            this string? value)
        {
            return string.IsNullOrEmpty(value);
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
#endif
        public static bool IsNotNullOrEmpty(
#if NETCOREAPP3_0 || NETSTANDARD2_1
            [NotNullWhen(true)]
#endif
            this string? value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}
