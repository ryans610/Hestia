#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif

namespace RyanJuan.Hestia;

public static partial class ReflectionCenter
{
    private static readonly ConcurrentDictionary<
        Type,
#if !NET40
        IReadOnlyDictionary<string, PropertyInfo>
#else
        IDictionary<string, PropertyInfo>
#endif
    > s_cachedPropertyInfosByType = new();

    [PublicAPI]
    public static PropertyInfo? GetProperty<TType>(
        string name)
    {
        Error.ThrowIfArgumentNull(nameof(name), name);
        var propertyInfos = PropertyGenericCache<TType>.PropertyInfos;
        // ReSharper disable once CanSimplifyDictionaryTryGetValueWithGetValueOrDefault
        return propertyInfos.TryGetValue(name, out var result) ? result : null;
    }

    [PublicAPI]
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

        var propertyInfos = s_cachedPropertyInfosByType.GetOrAdd(
            type,
            key => GetPropertiesForType(type));
        // ReSharper disable once CanSimplifyDictionaryTryGetValueWithGetValueOrDefault
        return propertyInfos.TryGetValue(name, out var result) ? result : null;
    }

    private static
#if !NET40
        IReadOnlyDictionary<string, PropertyInfo>
#else
        IDictionary<string, PropertyInfo>
#endif
        GetPropertiesForType(Type type)
    {
        return GetPropertiesInternal(type, GetAllBindingAttr)
                .GroupBy(x => x.Name)
                .Select(x => new NamePropertyTuple(x.Key, GetMostMatchProperty(x, type)))
                .Where(x => x.PropertyInfo is not null)
#if NET8_0_OR_GREATER
                .ToFrozenDictionary(x => x.Name, x => x.PropertyInfo!)
#else
                .ToDictionary(x => x.Name, x => x.PropertyInfo!)
#if !NET40
                .AsReadOnly()
#endif
#endif
            ;
    }

    private static PropertyInfo? GetMostMatchProperty(
        IEnumerable<PropertyInfo> properties,
        Type type)
    {
        using var iterator = properties.GetEnumerator();
        if (!iterator.MoveNext())
        {
            return null;
        }

        var first = iterator.Current;
        if (!iterator.MoveNext())
        {
            return first;
        }

        var list = new List<PropertyInfo>
        {
            first,
            iterator.Current,
        };
        while (iterator.MoveNext())
        {
            list.Add(iterator.Current);
        }

        var declareType = type;
        do
        {
            var declaringTypeMatched = list.FirstOrDefault(x => x.DeclaringType == type);
            if (declaringTypeMatched is not null)
            {
                return declaringTypeMatched;
            }

            declareType = declareType.BaseType;
        } while (declareType is not null && declareType != typeof(object));

        return list[0];
    }

    private static class PropertyGenericCache<TType>
    {
        public static readonly
#if !NET40
            IReadOnlyDictionary<string, PropertyInfo>
#else
            IDictionary<string, PropertyInfo>
#endif
            PropertyInfos = GetPropertiesForType(typeof(TType));
    }

    private readonly struct NamePropertyTuple(
        string name,
        PropertyInfo? propertyInfo)
    {
        public string Name { get; } = name;
        public PropertyInfo? PropertyInfo { get; } = propertyInfo;
    }
}
