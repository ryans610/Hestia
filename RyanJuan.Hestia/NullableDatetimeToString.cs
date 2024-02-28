namespace RyanJuan.Hestia;

public static partial class Hestia
{
#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <param name="datetime"></param>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
#endif
    [PublicAPI]
    public static string ToStringOrEmpty(
        this DateTime? datetime,
        string? format,
        IFormatProvider? provider)
    {
        return datetime?.ToString(format, provider) ?? string.Empty;
    }
}
