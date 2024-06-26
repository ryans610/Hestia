namespace RyanJuan.Hestia;

public static partial class HestiaReflection
{
    [PublicAPI]
    [Pure]
    public static bool IsIndexer(this PropertyInfo property)
    {
        return !IsNotIndexer(property);
    }

    [PublicAPI]
    [Pure]
    public static bool IsNotIndexer(this PropertyInfo property)
    {
        return property.GetIndexParameters().Length == 0;
    }
}
