namespace RyanJuan.Hestia;

public static partial class HestiaString
{
#if NET8_0_OR_GREATER
    [PublicAPI]
    public static T To<T>(
        this string value)
        where T : struct, IParsable<T>
    {
        return T.Parse(value, provider: null);
    }

    [PublicAPI]
    public static T To<T>(
        this ReadOnlySpan<char> value)
        where T : struct, ISpanParsable<T>
    {
        return T.Parse(value, provider: null);
    }


    [PublicAPI]
    public static T To<T>(
        this ReadOnlySpan<byte> utf8Text)
        where T : struct, IUtf8SpanParsable<T>
    {
        return T.Parse(utf8Text, provider: null);
    }
#endif
}
