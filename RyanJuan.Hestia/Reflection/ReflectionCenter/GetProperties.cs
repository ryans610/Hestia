namespace RyanJuan.Hestia;

/// <summary>
/// 
/// </summary>
public static partial class ReflectionCenter
{
    private static readonly ConcurrentDictionary<Type, ReadOnlyCollection<PropertyInfo>>
        s_cachedProperties = new();

    private static readonly ConcurrentDictionary<TypeBindingFlagsTuple, ReadOnlyCollection<PropertyInfo>>
        s_cachedPropertiesByBindingFlags = new();

    [PublicAPI]
    public static
#if !NET40
        IReadOnlyList<PropertyInfo>
#else
        ReadOnlyCollection<PropertyInfo>
#endif
        GetProperties<TType>()
    {
        return PropertiesCache<TType>.Properties;
    }

    [PublicAPI]
    public static
#if !NET40
        IReadOnlyList<PropertyInfo>
#else
        ReadOnlyCollection<PropertyInfo>
#endif
        GetAllProperties<TType>()
    {
        return PropertiesCache<TType>.AllProperties;
    }

    [PublicAPI]
    public static
#if !NET40
        IReadOnlyList<PropertyInfo>
#else
        ReadOnlyCollection<PropertyInfo>
#endif
        GetInstanceProperties<TType>()
    {
        return PropertiesCache<TType>.InstanceProperties;
    }

    [PublicAPI]
    public static
#if !NET40
        IReadOnlyList<PropertyInfo>
#else
        ReadOnlyCollection<PropertyInfo>
#endif
        GetProperties(
        Type type)
    {
        return GetPropertiesInternal(type);
    }

    [PublicAPI]
    public static
#if !NET40
        IReadOnlyList<PropertyInfo>
#else
        ReadOnlyCollection<PropertyInfo>
#endif
        GetProperties(
        Type type,
        BindingFlags bindingAttr)
    {
        Error.ThrowIfArgumentNull(nameof(type), type);
        return bindingAttr switch
        {
            SystemDefaultBindingAttr => GetProperties(type),
            GetAllBindingAttr => GetAllProperties(type),
            HestiaReflection.DefaultInstanceBindingAttr => GetInstanceProperties(type),
            _ => GetPropertiesInternal(type, bindingAttr),
        };
    }

    [PublicAPI]
    public static
#if !NET40
        IReadOnlyList<PropertyInfo>
#else
        ReadOnlyCollection<PropertyInfo>
#endif
        GetAllProperties(
        Type type)
    {
        return GetPropertiesInternal(
            type,
            GetAllBindingAttr);
    }

    [PublicAPI]
    public static
#if !NET40
        IReadOnlyList<PropertyInfo>
#else
        ReadOnlyCollection<PropertyInfo>
#endif
        GetInstanceProperties(
        Type type)
    {
        return GetPropertiesInternal(
            type,
            HestiaReflection.DefaultInstanceBindingAttr);
    }

    private static ReadOnlyCollection<PropertyInfo> GetPropertiesInternal(
        Type type)
    {
        return s_cachedProperties.GetOrAdd(
            type,
            t =>
                GetProperties(t, GetAllBindingAttr)
                    .Where(x => x.IsPublic())
                    .AsReadOnlyCollection());
    }

    private static ReadOnlyCollection<PropertyInfo> GetPropertiesInternal(
        Type type,
        BindingFlags bindingAttr)
    {
        return s_cachedPropertiesByBindingFlags.GetOrAdd(
            new TypeBindingFlagsTuple(type, bindingAttr),
            tuple => tuple
                .Type
                .GetProperties(tuple.BindingAttr)
                .AsReadOnlyCollection());
    }

    private static class PropertiesCache<TType>
    {
        public static readonly ReadOnlyCollection<PropertyInfo> Properties =
            GetPropertiesInternal(typeof(TType));

        public static readonly ReadOnlyCollection<PropertyInfo> AllProperties =
            GetPropertiesInternal(typeof(TType), GetAllBindingAttr)
                .AsReadOnlyCollection();

        public static readonly ReadOnlyCollection<PropertyInfo> InstanceProperties =
            GetInstanceProperties(typeof(TType))
                .AsReadOnlyCollection();
    }
}
