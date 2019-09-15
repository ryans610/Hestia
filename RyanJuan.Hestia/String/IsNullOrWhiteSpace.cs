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
        public static bool IsNullOrWhiteSpace(
            this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
#endif
        public static bool IsNotNullOrWhiteSpace(
            this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
