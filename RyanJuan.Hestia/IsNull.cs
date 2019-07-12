using System;

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
        /// <param name="str"></param>
        /// <returns></returns>
#endif
        public static bool IsNull(
            this string str)
        {
            return str is null;
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
            this T obj)
            where T : class
        {
            return !(obj is null);
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
#endif
        public static bool IsNotNull(
            this string str)
        {
            return !(str is null);
        }
    }
}
