namespace RyanJuan.Hestia;

public static partial class Hestia
{
#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
#endif
    [PublicAPI]
    public static string ToLowerString(
        this bool value,
        CultureInfo culture)
    {
        return value.ToString().ToLower(culture);
    }

#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
#endif
    [PublicAPI]
    public static string ToLowerString(
        this bool value)
    {
        return ToLowerString(value, CultureInfo.InvariantCulture);
    }
}
