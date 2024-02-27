using System;

namespace RyanJuan.Hestia;

public static partial class HestiaEnum
{
#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static
#if NET40
        IList<string>
#else
        IReadOnlyList<string>
#endif
        GetNames<TEnum>()
        where TEnum : Enum
    {
        return EnumCacheCenter<TEnum>.Names;
    }

    [PublicAPI]
    public static
#if NET40
        IList<string>
#else
        IReadOnlyList<string>
#endif
        GetNames(Type type)
    {
        ThrowIfTypeIsNotEnum(type);
        return EnumCacheCenter.GetNames(type);
    }
}
