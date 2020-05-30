using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RyanJuan.Hestia
{
    public static partial class ReflectionCenter
    {
        private static readonly ConcurrentDictionary<TypeStringTuple, PropertyInfo?> s_cachedPropertyInfoByName =
            new ConcurrentDictionary<TypeStringTuple, PropertyInfo?>();

        public static PropertyInfo? GetProperty(
            Type type,
            string name)
        {
            Error.ThrowIfArgumentNull(nameof(type), type);
            Error.ThrowIfArgumentNull(nameof(name), name);
            if (name.IsEmpty())
            {
                throw Error.Argument(
                    nameof(name),
                    $"{nameof(name)} can not be empty.");
            }
            if (s_cachedPropertyInfoByName.TryGetValue(
                    new TypeStringTuple(type, name),
                    out var value))
            {
                return value;
            }
            var property = type.GetProperty(name);
            s_cachedPropertyInfoByName.TryAdd(
                new TypeStringTuple(type, name),
                property);
            return property;
        }

        private readonly struct TypeStringTuple : IEquatable<TypeStringTuple>
        {
            public TypeStringTuple(
                Type type,
                string name)
            {
                Type = type;
                Name = name;
            }

            public Type Type { get; }
            public string Name { get; }

            public bool Equals(
#if NETCOREAPP3_0 || NETSTANDARD2_1
                [AllowNull]
#endif
                TypeStringTuple other) =>
                Type == other.Type && Name == other.Name;

            public override bool Equals(object? obj) =>
                obj is TypeStringTuple tuple ?
                    this.Equals(tuple) :
                    false;

            public override int GetHashCode() =>
                unchecked((Type?.GetHashCode() ?? 1) * (Name?.GetHashCode() ?? 1));
        }
    }
}
