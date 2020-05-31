﻿using System;
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
            var property = type.GetProperty(name, GetAllBindingAttr);
            s_cachedPropertyInfoByName.TryAdd(
                new TypeStringTuple(type, name),
                property);
            return property;
        }
    }
}
