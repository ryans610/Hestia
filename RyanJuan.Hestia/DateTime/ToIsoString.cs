namespace RyanJuan.Hestia;

public static partial class HestiaDateTime
{
    [PublicAPI]
    [Pure]
    public static string ToIsoString(this DateTime value)
    {
        return value.ToString("o", CultureInfo.InvariantCulture);
    }

    [PublicAPI]
    [Pure]
    public static string? ToIsoString(this DateTime? value)
    {
        return value.HasValue ? ToIsoString(value.Value) : null;
    }
}
