namespace RyanJuan.Hestia;

public static partial class HestiaReflection
{
    private static readonly MethodInfo s_methodInfoGetDefaultValueT = typeof(HestiaReflection).GetMethod(
        nameof(GetDefaultValue),
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        1,
#endif
        Type.EmptyTypes)!;

    private static readonly ConcurrentDictionary<Type, object?> s_cachedDefaultValue = new();

    [PublicAPI]
    public static TType? GetDefaultValue<TType>()
    {
        return default;
    }

    [PublicAPI]
    public static object? GetDefaultValue(this Type type)
    {
        Error.ThrowIfArgumentNull(nameof(type), type);
        return s_cachedDefaultValue.GetOrAdd(
            type,
            t => s_methodInfoGetDefaultValueT.MakeGenericMethod(t).Invoke(null, null));
    }
}
