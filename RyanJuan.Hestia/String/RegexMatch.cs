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
    /// <param name="pattern"></param>
    /// <returns></returns>
#endif
    public static Match RegexMatch(
        this string value,
        string pattern)
    {
        Error.ThrowIfArgumentNull(nameof(value), value);
        Error.ThrowIfArgumentNull(nameof(pattern), pattern);
        return Regex.Match(value, pattern);
    }
}
