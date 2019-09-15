using System;

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
        public static bool IsEmpty(
            this string value)
        {
            return value == null ? false : value.Length == 0;
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
#endif
        public static bool IsNotEmpty(
            this string value)
        {
            return value == null ? true : value.Length > 0;
        }
    }
}
