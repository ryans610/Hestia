using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace RyanJuan.Hestia
{
    public static partial class ReflectionCenter
    {
        public static object? GetValue<T>(T instance, PropertyInfo property)
        {
            Error.ThrowIfArgumentNull(nameof(property), property);
            return ExpressionPropertyGetMethod<T>.GetGetMethod(property)(instance);
        }
    }
}
