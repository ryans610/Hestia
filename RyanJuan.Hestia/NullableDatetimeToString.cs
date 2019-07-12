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
        /// <param name="datetime"></param>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
#endif
        public static string ToStringOrEmpty(
            this DateTime? datetime,
            string format,
            IFormatProvider provider)
        {
            if (datetime.HasValue)
            {
                return datetime.Value.ToString(format, provider);
            }
            return string.Empty;
        }
    }
}
