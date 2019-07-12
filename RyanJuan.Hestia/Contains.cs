using System;
using System.Text;

namespace RyanJuan.Hestia
{
    public static partial class Hestia
    {
#if !NETCOREAPP2_1
#if ZH_HANT
#else
#endif
        public static bool Contains(
            this string str,
            string value,
            StringComparison comparisonType)
        {
            return str.IndexOf(value, comparisonType) >= 0;
        }
#endif
    }
}
