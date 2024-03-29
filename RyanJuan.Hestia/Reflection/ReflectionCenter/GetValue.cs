namespace RyanJuan.Hestia;

public static partial class ReflectionCenter
{
    [PublicAPI]
    public static object? GetValue<T>(T instance, PropertyInfo property)
    {
        Error.ThrowIfArgumentNull(nameof(property), property);
        return ExpressionPropertyGetMethod<T>.GetGetMethod(property).Invoke(instance);
    }
}
