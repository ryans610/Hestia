namespace RyanJuan.Hestia;

public static partial class HestiaReflection
{
    [PublicAPI]
    [Pure]
    public static bool IsStatic(this PropertyInfo property)
    {
        return property.GetAccessors(nonPublic: true).Any(x => x.IsStatic);
    }
}
