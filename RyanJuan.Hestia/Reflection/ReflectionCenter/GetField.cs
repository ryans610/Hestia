using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace RyanJuan.Hestia
{
    public static partial class ReflectionCenter
    {
        private static readonly ConcurrentDictionary<TypeStringTuple, FieldInfo?> s_cachedFieldInfoByName =
            new ConcurrentDictionary<TypeStringTuple, FieldInfo?>();

        public static FieldInfo? GetField(
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
            if (s_cachedFieldInfoByName.TryGetValue(
                    new TypeStringTuple(type, name),
                    out var value))
            {
                return value;
            }
            var field = type.GetField(name, GetAllBindingAttr);
            s_cachedFieldInfoByName.TryAdd(
                new TypeStringTuple(type, name),
                field);
            return field;
        }
    }
}
