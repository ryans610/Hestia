using JetBrains.Annotations;

namespace RyanJuan.Hestia.Taiwan;

public static class HestiaTaiwan
{
    internal const string DefaultStandardDateFormat = "yyyy-MM-dd";
    internal const string DefaultStandardTimeFormat = "HH:mm:ss";
    internal const string DefaultStandardDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
    internal const string DefaultRocDateFormat = "yyy年MM月dd日";
    internal const string DefaultRocTimeFormat = "HH小時mm分ss秒";
    internal const string DefaultRocDateTimeFormat = "yyy年MM月dd日HH小時mm分ss秒";

    internal static HestiaTaiwanOptions Options { get; private set; } = new();

    [PublicAPI]
    public static void Configure(Action<HestiaTaiwanOptions> configureBinder)
    {
        Error.ThrowIfArgumentNull(nameof(configureBinder), configureBinder);
        var newOptions = new HestiaTaiwanOptions();
        configureBinder.Invoke(newOptions);
        Options = newOptions with { };
    }
}
