using JetBrains.Annotations;

namespace RyanJuan.Hestia.Taiwan.Extensions;

public static class DateTimeExtensions
{
    static DateTimeExtensions()
    {
        try
        {
            var taiwanFormatCulture = new CultureInfo("zh-Hant-TW");
            taiwanFormatCulture.DateTimeFormat.Calendar =
                taiwanFormatCulture.OptionalCalendars.OfType<TaiwanCalendar>().First();
            s_taiwanFormatCulture = CultureInfo.ReadOnly(taiwanFormatCulture);
        }
        catch (Exception)
        {
            s_taiwanFormatCulture = CultureInfo.InvariantCulture;
        }
    }

    private static readonly CultureInfo s_taiwanFormatCulture;

    [PublicAPI]
    public static string ToStandardDateString(this DateTime value)
    {
        return value.ToString(HestiaTaiwan.Options.StandardDateFormat);
    }

    [PublicAPI]
    public static string? ToStandardDateString(this DateTime? value)
    {
        return value.HasValue ? ToStandardDateString(value.Value) : null;
    }

    [PublicAPI]
    public static string ToStandardTimeString(this DateTime value)
    {
        return value.ToString(HestiaTaiwan.Options.StandardTimeFormat);
    }

    [PublicAPI]
    public static string? ToStandardTimeString(this DateTime? value)
    {
        return value.HasValue ? ToStandardTimeString(value.Value) : null;
    }

    [PublicAPI]
    public static string ToStandardDateTimeString(this DateTime value)
    {
        return value.ToString(HestiaTaiwan.Options.StandardDateTimeFormat);
    }

    [PublicAPI]
    public static string? ToStandardDateTimeString(this DateTime? value)
    {
        return value.HasValue ? ToStandardDateTimeString(value.Value) : null;
    }

    [PublicAPI]
    public static string ToRocString(this DateTime value, string format)
    {
        return value.ToString(format, s_taiwanFormatCulture);
    }

    [PublicAPI]
    public static string? ToRocString(this DateTime? value, string format)
    {
        return value.HasValue ? ToRocString(value.Value, format) : null;
    }

    [PublicAPI]
    public static string ToRocDateString(this DateTime value)
    {
        return ToRocString(value, HestiaTaiwan.Options.RocDateFormat);
    }

    [PublicAPI]
    public static string? ToRocDateString(this DateTime? value)
    {
        return value.HasValue ? ToRocDateString(value.Value) : null;
    }

    [PublicAPI]
    public static string ToRocTimeString(this DateTime value)
    {
        return ToRocString(value, HestiaTaiwan.Options.RocTimeFormat);
    }

    [PublicAPI]
    public static string? ToRocTimeString(this DateTime? value)
    {
        return value.HasValue ? ToRocTimeString(value.Value) : null;
    }

    [PublicAPI]
    public static string ToRocDateTimeString(this DateTime value)
    {
        return ToRocString(value, HestiaTaiwan.Options.RocDateTimeFormat);
    }

    [PublicAPI]
    public static string? ToRocDateTimeString(this DateTime? value)
    {
        return value.HasValue ? ToRocDateTimeString(value.Value) : null;
    }
}
