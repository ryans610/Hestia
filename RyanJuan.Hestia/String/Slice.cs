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
        /// <param name="length"></param>
        /// <returns></returns>
#endif
        public static string Slice(
            this string value,
            int length)
        {
            return value.Substring(0, length);
        }
    }
}
