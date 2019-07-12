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
        public static bool IsNullOrEmpty(
            this string str)
        {
            return string.IsNullOrEmpty(str);
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
#endif
        public static bool IsNotNullOrEmpty(
            this string str)
        {
            return !string.IsNullOrEmpty(str);
        }
    }
}
