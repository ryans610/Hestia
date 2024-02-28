using JetBrains.Annotations;

namespace RyanJuan.Hestia.Taiwan.Extensions;

public static class BooleanExtensions
{
    [PublicAPI]
    public static string ToTaiwanMandarinString(this bool value)
    {
        return value ? "是" : "否";
    }
}
