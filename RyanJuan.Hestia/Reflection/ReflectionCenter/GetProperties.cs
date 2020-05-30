using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RyanJuan.Hestia
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class ReflectionCenter
    {
        private const BindingFlags SystemDefaultBindingAttr =
            BindingFlags.Instance |
            BindingFlags.Static |
            BindingFlags.Public;

        private static readonly ConcurrentDictionary<TypeBindingFlagsTuple, PropertyInfo[]> s_cachedPropertyInfoArrayByBindingFlags =
            new ConcurrentDictionary<TypeBindingFlagsTuple, PropertyInfo[]>();

        public static PropertyInfo[] GetProperties(
            Type type,
            BindingFlags bindingAttr)
        {
            Error.ThrowIfArgumentNull(nameof(type), type);
            if (s_cachedPropertyInfoArrayByBindingFlags.TryGetValue(
                    new TypeBindingFlagsTuple(type, bindingAttr),
                    out var value))
            {
                return value.Copy();
            }
            var properties = type.GetProperties(bindingAttr);
            s_cachedPropertyInfoArrayByBindingFlags.TryAdd(
                new TypeBindingFlagsTuple(type, bindingAttr),
                properties.Copy());
            return properties;
        }

        public static PropertyInfo[] GetProperties(
            Type type)
        {
            return GetProperties(type, SystemDefaultBindingAttr);
        }

        public static PropertyInfo[] GetInstanceProperties(
            Type type)
        {
            return GetProperties(type, HestiaReflection.DefaultInstanceBindingAttr);
        }

        private readonly struct TypeBindingFlagsTuple : IEquatable<TypeBindingFlagsTuple>
        {
            public TypeBindingFlagsTuple(
                Type type,
                BindingFlags bindingAttr)
            {
                Type = type;
                BindingAttr = bindingAttr;
            }

            public Type Type { get; }
            public BindingFlags BindingAttr { get; }

            public bool Equals(
#if NETCOREAPP3_0 || NETSTANDARD2_1
                [AllowNull]
#endif
                TypeBindingFlagsTuple other) =>
                Type == other.Type && BindingAttr == other.BindingAttr;

            public override bool Equals(object? obj) =>
                obj is TypeBindingFlagsTuple tuple ?
                    this.Equals(tuple) :
                    false;

            public override int GetHashCode() =>
                unchecked((Type?.GetHashCode() ?? 1) * BindingAttr.GetHashCode());
        }
    }
}
