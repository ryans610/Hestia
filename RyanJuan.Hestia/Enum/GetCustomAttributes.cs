namespace RyanJuan.Hestia;

public static partial class HestiaEnum
{
    [PublicAPI]
    public static IEnumerable<TAttribute> GetCustomAttributes<TEnum, TAttribute>(
        TEnum enumValue)
        where TEnum : Enum
        where TAttribute : Attribute
    {
        var containers = EnumCacheCenter<TEnum>.Attributes;
        return GetAttributesFromContainer<TAttribute>(enumValue, containers);
    }

    [PublicAPI]
    public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(
        Enum enumValue)
        where TAttribute : Attribute
    {
        var containers = EnumCacheCenter.GetAttributesCacheContainers(enumValue.GetType());
        return GetAttributesFromContainer<TAttribute>(enumValue, containers);
    }

    private static IEnumerable<TAttribute> GetAttributesFromContainer<TAttribute>(
        Enum value,
        IEnumerable<AttributesCacheContainer> containers)
    {
        var container = containers.FirstOrDefault(x => x.Value.Equals(value));
        return container is null ? Enumerable.Empty<TAttribute>() : container.Attributes.OfType<TAttribute>();
    }
}
