namespace RyanJuan.Hestia;

public static partial class HestiaString
{
#if NET8_0_OR_GREATER
    [PublicAPI]
    public static T? ToNullableOrDefault<T>(
        this string value,
        T? defaultValue = default)
        where T : struct, IParsable<T>
    {
        if (IsNullOrEmpty(value) ||
            !T.TryParse(value, null, out var result))
        {
            return defaultValue;
        }

        return result;
    }

    [PublicAPI]
    public static T? ToNullableOrDefault<T>(
        this ReadOnlySpan<char> value,
        T? defaultValue = default)
        where T : struct, ISpanParsable<T>
    {
        if (value.IsEmpty ||
            !T.TryParse(value, null, out var result))
        {
            return defaultValue;
        }

        return result;
    }

    [PublicAPI]
    public static T? ToNullableOrDefault<T>(
        this ReadOnlySpan<byte> value,
        T? defaultValue = default)
        where T : struct, IUtf8SpanParsable<T>
    {
        if (value.IsEmpty ||
            !T.TryParse(value, null, out var result))
        {
            return defaultValue;
        }

        return result;
    }
#endif
}
