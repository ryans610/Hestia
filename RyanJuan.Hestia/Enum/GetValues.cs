namespace RyanJuan.Hestia;

public static partial class HestiaEnum
{
#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static
#if NET40
        IList<TEnum>
#else
        IReadOnlyList<TEnum>
#endif
        GetValues<TEnum>()
        where TEnum : Enum
    {
        return EnumCacheCenter<TEnum>.Values;
    }

    [PublicAPI]
    public static Array GetValues(Type type)
    {
        Error.ThrowIfArgumentNull(nameof(type), type);
        ThrowIfTypeIsNotEnum(type);
        var values = EnumCacheCenter.GetValues(type);
        return (Array)values.Clone();
    }
}
