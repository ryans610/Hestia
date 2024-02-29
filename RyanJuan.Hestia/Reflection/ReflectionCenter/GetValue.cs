using Expression = System.Linq.Expressions.Expression;
using ParameterExpression = System.Linq.Expressions.ParameterExpression;

namespace RyanJuan.Hestia;

public static partial class ReflectionCenter
{
    [PublicAPI]
    public static object? GetValue<T>(T instance, PropertyInfo property)
    {
        Error.ThrowIfArgumentNull(nameof(property), property);
        return ExpressionPropertyGetMethod<T>.GetGetMethod(property).Invoke(instance);
    }

    private static readonly ConcurrentDictionary<PropertyInfo, Func<object, object>> s_get = new();
    private static readonly ConcurrentDictionary<PropertyInfo, Action<object, object>> s_set = new();

    public static object? GetValue(PropertyInfo propertyInfo, object instance)
    {
        if (!propertyInfo.CanRead)
        {
        }

        //check indexer
        var getMethod = s_get.GetOrAdd(
            propertyInfo,
            CreatePropertyGetMethod);
        return getMethod.Invoke(instance);
    }

    public static void SetValue(PropertyInfo propertyInfo, object instance, object? value)
    {
        if (!propertyInfo.CanWrite)
        {

        }
        //check indexer
        var setMethod = s_set.GetOrAdd(
            propertyInfo,
            CreatePropertySetMethod);
        if (value is null && propertyInfo.PropertyType.IsValueType)
        {
            value = HestiaReflection.GetDefaultValue(propertyInfo.PropertyType);
        }
        setMethod.Invoke(instance,value);
    }

    private static ParameterExpression GetObjectParameterExpression(string parameterName)
    {
        return Expression.Parameter(typeof(object), parameterName);
    }

    private static UnaryExpression GetCastExpression(Expression expression, Type castType)
    {
        return castType.IsValueType
            ? Expression.Convert(expression, castType)
            : Expression.TypeAs(expression, castType);
    }

    private static Func<object, object> CreatePropertyGetMethod(PropertyInfo propertyInfo)
    {
        var instanceParameter = GetObjectParameterExpression("instance");
        var instanceCast = GetCastExpression(instanceParameter, propertyInfo.DeclaringType);
        var lambda = Expression.Lambda<Func<object, object>>(
            Expression.TypeAs(
                Expression.Call(
                    instanceCast,
                    propertyInfo.GetGetMethod(nonPublic: true)),
                typeof(object)),
            instanceParameter);
        return lambda.Compile();
    }

    private static Action<object, object> CreatePropertySetMethod(PropertyInfo propertyInfo)
    {
        var instanceParameter = GetObjectParameterExpression("instance");
        var valueParameter = GetObjectParameterExpression("value");
        var instanceCast = GetCastExpression(instanceParameter, propertyInfo.DeclaringType);
        var valueCast = GetCastExpression(valueParameter, propertyInfo.PropertyType);
        var lambda = Expression.Lambda<Action<object, object>>(
            Expression.Call(
                instanceCast,
                propertyInfo.GetSetMethod(),
                valueCast),
            instanceParameter,
            valueParameter);
        return lambda.Compile();
    }
}
