namespace RyanJuan.Hestia;

public static partial class Hestia
{
#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
#endif
    public static bool IsWhiteSpace(
        this char c)
    {
        return char.IsWhiteSpace(c);
    }

#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
#endif
    public static bool IsNotWhiteSpace(
        this char c)
    {
        return !char.IsWhiteSpace(c);
    }
}
