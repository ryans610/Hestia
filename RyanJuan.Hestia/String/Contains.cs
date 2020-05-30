using System;
using System.Text;

namespace RyanJuan.Hestia
{
    public static partial class HestiaString
    {
        //feature in .Net Core and .Net Standard 2.1
#if NET40 || NET45 || NETSTANDARD2_0
#if ZH_HANT
#else
#endif
        public static bool Contains(
            this string str,
            string value,
            StringComparison comparisonType)
        {
            Error.ThrowIfArgumentNull(nameof(str), str);
            Error.ThrowIfArgumentNull(nameof(value), value);
            return str.IndexOf(value, comparisonType) >= 0;
        }

#if ZH_HANT
#else
#endif
        public static bool Contains(
            this string str,
            char value)
        {
            Error.ThrowIfArgumentNull(nameof(str), str);
            return str.IndexOf(value) >= 0;
        }

#if ZH_HANT
#else
#endif
        public static bool Contains(
            this string str,
            char value,
            StringComparison comparisonType)
        {
            Error.ThrowIfArgumentNull(nameof(str), str);
            return str.IndexOf(value, comparisonType) >= 0;
        }
#endif
    }
}
