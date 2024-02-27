using System.Text.RegularExpressions;

namespace RyanJuan.Hestia;

public static partial class HestiaString
{
#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
#endif
    public static string RegexEscape(
        this string value)
    {
        Error.ThrowIfArgumentNull(nameof(value), value);
        return Regex.Escape(value);
    }
}
