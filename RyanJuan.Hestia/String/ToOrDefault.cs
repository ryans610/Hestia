namespace RyanJuan.Hestia;

public static partial class HestiaString
{
#if NET8_0_OR_GREATER
    [PublicAPI]
    public static T ToOrDefault<T>(
        this string value,
        T defaultValue = default)
        where T : struct, IParsable<T>
    {
        return T.TryParse(value, provider: null, out var result) ? result : defaultValue;
    }

    [PublicAPI]
    public static T ToOrDefault<T>(
        this ReadOnlySpan<char> value,
        T defaultValue = default)
        where T : struct, ISpanParsable<T>
    {
        return T.TryParse(value, provider: null, out var result) ? result : defaultValue;
    }


    [PublicAPI]
    public static T ToOrDefault<T>(
        this ReadOnlySpan<byte> utf8Text,
        T defaultValue = default)
        where T : struct, IUtf8SpanParsable<T>
    {
        return T.TryParse(utf8Text, provider: null, out var result) ? result : defaultValue;
    }
#endif
}
