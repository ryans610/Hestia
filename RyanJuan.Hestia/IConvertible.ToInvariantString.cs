namespace RyanJuan.Hestia;

public static partial class Hestia
{
    [PublicAPI]
    public static string ToInvariantString<TType>(this TType value)
        where TType : IConvertible
    {
        return value.ToString(CultureInfo.InvariantCulture);
    }
}
