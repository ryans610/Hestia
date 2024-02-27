namespace RyanJuan.Hestia;

public static partial class ReflectionCenter
{
    public static Func<T, object?> GetPropertyGetMethod<T>(
        PropertyInfo property)
    {
        Error.ThrowIfArgumentNull(nameof(property), property);
        return ExpressionPropertyGetMethod<T>.GetGetMethod(property);
    }

    private static class ExpressionPropertyGetMethod<T>
    {
        private static readonly ConcurrentDictionary<PropertyInfo, Func<T, object?>> s_cachedGetMethodByPropertyInfo = new();

        private static readonly ParameterExpression s_typeExpressionParameter =
            Expression.Parameter(typeof(T));

        public static Func<T, object?> GetGetMethod(
            PropertyInfo property)
        {
            if (s_cachedGetMethodByPropertyInfo.TryGetValue(
                    property,
                    out var value))
            {
                return value;
            }
            var expression = Expression.Lambda<Func<T, object?>>(
                Expression.Convert(
                    Expression.Call(
                        s_typeExpressionParameter,
                        property.GetGetMethod()),
                    typeof(object)),
                s_typeExpressionParameter).Compile();
            s_cachedGetMethodByPropertyInfo.TryAdd(property, expression);
            return expression;
        }
    }
}
