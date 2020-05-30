using System;
using System.Text.RegularExpressions;

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
        /// <param name="pattern"></param>
        /// <returns></returns>
#endif
        public static bool RegexIsMatch(
            this string value,
            string pattern)
        {
            Error.ThrowIfArgumentNull(nameof(value), value);
            Error.ThrowIfArgumentNull(nameof(pattern), pattern);
            return Regex.IsMatch(value, pattern);
        }
    }
}
