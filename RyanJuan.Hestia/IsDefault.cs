using System;
using System.Collections.Generic;

namespace RyanJuan.Hestia
{
    public static partial class Hestia
    {
#if ZH_HANT
#else
#endif
        public static bool IsDefault<T>(
            this T obj)
        {
            return EqualityComparer<T>.Default.Equals(obj, default);
        }

#if ZH_HANT
#else
#endif
        public static bool IsNotDefault<T>(
            this T obj)
        {
            return !obj.IsDefault();
        }
    }
}
