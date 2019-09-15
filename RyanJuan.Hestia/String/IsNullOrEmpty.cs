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
        public static bool IsNullOrEmpty(
            this string value)
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
            this string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}
