using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace RyanJuan.Hestia
{
    public static partial class ReflectionCenter
    {
        public static T CreateInstance<T>()
        {
            return ExpressionConstructor<T>.Default();
        }

        public static void SetInstanceFactory<T>(Func<T> factory)
        {
            Error.ThrowIfArgumentNull(nameof(factory), factory);
            ExpressionConstructor<T>.Default = factory;
        }

        private static class ExpressionConstructor<T>
        {
            private static Func<T>? s_default = null;

            public static Func<T> Default
            {
                get => s_default ??= Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
                set => s_default = value;
            }
        }
    }
}
