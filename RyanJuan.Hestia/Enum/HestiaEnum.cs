using System;
using System.Collections.Concurrent;

namespace RyanJuan.Hestia
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class HestiaEnum
    {
#if ZH_HANT
#else
#endif


        internal static class EnumCacheCenter
        {
            private static readonly ConcurrentDictionary<Type, Array> s_cachedEnumValues =
                new ConcurrentDictionary<Type, Array>();

            public static Array GetValues(Type type)
            {
                if (s_cachedEnumValues.TryGetValue(type, out var value))
                {
                    return value;
                }
                value = Enum.GetValues(type);
                s_cachedEnumValues.TryAdd(type, value);
                return value;
            }
        }
    }
}
