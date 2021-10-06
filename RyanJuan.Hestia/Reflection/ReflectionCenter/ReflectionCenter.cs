using System;
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

        private const BindingFlags GetAllBindingAttr =
            BindingFlags.Instance |
            BindingFlags.Static |
            BindingFlags.Public |
            BindingFlags.NonPublic;

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

            public bool Equals(TypeStringTuple other) =>
                Type == other.Type && Name == other.Name;

            public override bool Equals(object? obj) =>
                obj is TypeStringTuple tuple && this.Equals(tuple);

            public override int GetHashCode() =>
#if NETCOREAPP2_1_OR_GREATER|| NETSTANDARD2_1_OR_GREATER
                HashCode.Combine(Type, Name);
#else
                unchecked((Type?.GetHashCode() ?? 0) * (Name?.GetHashCode() ?? 0));
#endif
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

            public bool Equals(TypeBindingFlagsTuple other) =>
                Type == other.Type && BindingAttr == other.BindingAttr;

            public override bool Equals(object? obj) =>
                obj is TypeBindingFlagsTuple tuple && this.Equals(tuple);

            public override int GetHashCode() =>
#if NETCOREAPP2_1_OR_GREATER|| NETSTANDARD2_1_OR_GREATER
                HashCode.Combine(Type, BindingAttr);
#else
                unchecked((Type?.GetHashCode() ?? 0) * BindingAttr.GetHashCode());
#endif
        }
    }
}
