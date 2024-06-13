using System.Linq.Expressions;

namespace RyanJuan.Hestia;

public static partial class ReflectionCenter
{
    private static readonly ConcurrentDictionary<PropertyInfo, Func<object, object?>> s_propertyGetValueDelegates =
        new();

    [PublicAPI]
    public static object? GetValue(PropertyInfo propertyInfo, object instance)
    {
        Error.ThrowIfArgumentNull(nameof(propertyInfo), propertyInfo);
        Error.ThrowIfArgumentNull(nameof(instance), instance);

        if (!propertyInfo.CanRead)
        {
            throw Error.Argument(
                nameof(propertyInfo),
                $"{propertyInfo} must have get accesser.");
        }

        ThrowIfIsIndexer(propertyInfo);

        var getMethod = s_propertyGetValueDelegates.GetOrAdd(
            propertyInfo,
            CreatePropertyGetMethod);
        return getMethod.Invoke(instance);
    }

    private static Func<object, object?> CreatePropertyGetMethod(PropertyInfo propertyInfo)
    {
        var instanceParameter = GetObjectParameterExpression(InstanceParameterExpressionName);
        var instanceCast = GetCastExpression(instanceParameter, propertyInfo.DeclaringType!);
        var getMethodInfo = propertyInfo.GetGetMethod(nonPublic: true);
        var getMethod = propertyInfo.IsStatic()
            ? Expression.Call(getMethodInfo)
            : Expression.Call(instanceCast, getMethodInfo);
        var lambda = Expression.Lambda<Func<object, object?>>(
            Expression.TypeAs(
                getMethod,
                typeof(object)),
            instanceParameter);
        return lambda.Compile();
    }
}
