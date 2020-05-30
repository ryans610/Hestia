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
        /// <param name="replacement"></param>
        /// <returns></returns>
#endif
        public static string RegexReplace(
            this string value,
            string pattern,
            string replacement)
        {
            Error.ThrowIfArgumentNull(nameof(value), value);
            Error.ThrowIfArgumentNull(nameof(pattern), pattern);
            Error.ThrowIfArgumentNull(nameof(replacement), replacement);
            return Regex.Replace(value, pattern, replacement);
        }
    }
}
