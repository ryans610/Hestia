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
        /// <param name="str"></param>
        /// <returns></returns>
#endif
        public static bool IsNullOrWhiteSpace(
            this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
#endif
        public static bool IsNotNullOrWhiteSpace(
            this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }
    }
}
