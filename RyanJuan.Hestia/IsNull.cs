using System;
using System.Diagnostics.CodeAnalysis;

namespace RyanJuan.Hestia
{
    public static partial class Hestia
    {
#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
#endif
        public static bool IsNull<T>(
#if NETCOREAPP3_0 || NETSTANDARD2_1
            [NotNullWhen(false)]
#endif
            this T obj)
            where T : class
        {
            return obj is null;
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
#endif
        public static bool IsNotNull<T>(
#if NETCOREAPP3_0 || NETSTANDARD2_1
            [NotNullWhen(true)]
#endif
            this T obj)
            where T : class
        {
            return !(obj is null);
        }
    }
}
