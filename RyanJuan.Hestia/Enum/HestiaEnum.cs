namespace RyanJuan.Hestia;

/// <summary>
/// 
/// </summary>
public static partial class HestiaEnum
{
    private static void ThrowIfTypeIsNotEnum(Type type)
    {
        if (!type.IsEnum)
        {
            // TODO: error message
            throw new InvalidOperationException();
        }
    }

    internal static class EnumCacheCenter
    {
        private static readonly ConcurrentDictionary<Type, Array> s_cachedEnumValues = new();
        private static readonly ConcurrentDictionary<Type, ReadOnlyCollection<string>> s_cachedEnumNames = new();

        private static readonly ConcurrentDictionary<Type, ReadOnlyCollection<AttributesCacheContainer>>
            s_cachedEnumAttributes = new();

        internal static Array GetValues(Type type)
        {
            return s_cachedEnumValues.GetOrAdd(
                type,
                Enum.GetValues);
        }

        internal static ReadOnlyCollection<string> GetNames(Type type)
        {
            return s_cachedEnumNames.GetOrAdd(
                type,
                t => Enum.GetNames(t).AsReadOnlyCollection());
        }

        internal static ReadOnlyCollection<AttributesCacheContainer> GetAttributesCacheContainers(Type type)
        {
            return s_cachedEnumAttributes.GetOrAdd(
                type,
                t => GetValues(t)
                    .Cast<Enum>()
                    .Select(x =>
                    {
                        var attributes = t
                            .GetField(x.ToString())
#if !NET40
                            ?.GetCustomAttributes()
#else
                            ?.GetCustomAttributes(inherit: false)
                            .Cast<Attribute>()
#endif
                            .AsReadOnlyCollection();
                        if (attributes is null)
                        {
                            return null!;
                        }

                        return new AttributesCacheContainer(
                            x,
                            attributes);
                    })
                    .Where(x => x is not null)
                    .AsReadOnlyCollection());
        }
    }

    internal static class EnumCacheCenter<TEnum>
        where TEnum : Enum
    {
        internal static readonly ReadOnlyCollection<TEnum> Values = EnumCacheCenter
            .GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .AsReadOnlyCollection();

        internal static readonly ReadOnlyCollection<string> Names = EnumCacheCenter
            .GetNames(typeof(TEnum));

        internal static readonly ReadOnlyCollection<AttributesCacheContainer<TEnum>> Attributes = EnumCacheCenter
            .GetAttributesCacheContainers(typeof(TEnum))
            .Select(x => x.Cast<TEnum>())
            .AsReadOnlyCollection();
    }

    internal class AttributesCacheContainer
    {
        public AttributesCacheContainer(
            Enum value,
            ReadOnlyCollection<Attribute> attributes)
        {
            Value = value;
            Attributes = attributes;
        }

        public readonly Enum Value;

        public readonly ReadOnlyCollection<Attribute> Attributes;

        public AttributesCacheContainer<TEnum> Cast<TEnum>()
            where TEnum : Enum
        {
            return new AttributesCacheContainer<TEnum>(
                (TEnum)Value,
                Attributes);
        }
    }

    internal class AttributesCacheContainer<TEnum> : AttributesCacheContainer
        where TEnum : Enum
    {
        public AttributesCacheContainer(
            TEnum value,
            ReadOnlyCollection<Attribute> attributes)
            : base(value, attributes)
        {
            Value = value;
        }

        public new readonly TEnum Value;
    }
}
