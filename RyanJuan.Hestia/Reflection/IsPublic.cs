namespace RyanJuan.Hestia;

public static partial class HestiaReflection
{
    [PublicAPI]
    [Pure]
    public static bool IsPublic(this PropertyInfo property)
    {
        return property.GetAccessors().Length > 0;
    }
}
