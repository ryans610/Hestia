namespace RyanJuan.Hestia;

public static partial class HestiaString
{
#if NET8_0_OR_GREATER
    [PublicAPI]
    public static T? ToNullableOrDefault<T>(
        this string value,
        T? defaultValue = null)
        where T : struct, IParsable<T>
    {
        return T.TryParse(value, provider: null, out var result) ? result : defaultValue;
    }

    [PublicAPI]
    public static T? ToNullableOrDefault<T>(
        this ReadOnlySpan<char> value,
        T? defaultValue = null)
        where T : struct, ISpanParsable<T>
    {
        return T.TryParse(value, provider: null, out var result) ? result : defaultValue;
    }

    [PublicAPI]
    public static T? ToNullableOrDefault<T>(
        this ReadOnlySpan<byte> utf8Text,
        T? defaultValue = null)
        where T : struct, IUtf8SpanParsable<T>
    {
        return T.TryParse(utf8Text, provider: null, out var result) ? result : defaultValue;
    }
#endif
}
