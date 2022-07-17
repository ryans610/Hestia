using System;
using System.Linq.Expressions;
using System.Reflection;

namespace RyanJuan.Hestia
{
    public static partial class HestiaReflection
    {
#if ZH_HANT
#else
#endif
        public static object? GetDefaultValue(this Type type)
        {
            Error.ThrowIfArgumentNull(nameof(type), type);
            return Activator.CreateInstance(type);
        }
    }
}
