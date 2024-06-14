namespace RyanJuan.Hestia;

public static partial class ReflectionCenter
{
    private static readonly ConcurrentDictionary<PropertyInfo, Action<object, object?>> s_propertySetValueDelegates =
        new();

    [PublicAPI]
    public static void SetValue(PropertyInfo propertyInfo, object instance, object? value)
    {
        Error.ThrowIfArgumentNull(nameof(propertyInfo), propertyInfo);
        if (!propertyInfo.IsStatic())
        {
            Error.ThrowIfArgumentNull(nameof(instance), instance);
        }

        if (!propertyInfo.CanWrite)
        {
            throw Error.Argument(
                nameof(propertyInfo),
                $"{propertyInfo} must have set accesser.");
        }

        ThrowIfIsIndexer(propertyInfo);

        var setMethod = s_propertySetValueDelegates.GetOrAdd(
            propertyInfo,
            CreatePropertySetMethod);
        if (value is null && propertyInfo.PropertyType.IsValueType)
        {
            // Set value to default for value type.
            // ReSharper disable once InvokeAsExtensionMethod
            value = HestiaReflection.GetDefaultValue(propertyInfo.PropertyType);
        }

        setMethod.Invoke(instance, value);
    }

    private static Action<object, object?> CreatePropertySetMethod(PropertyInfo propertyInfo)
    {
        var instanceParameter = GetObjectParameterExpression(InstanceParameterExpressionName);
        var valueParameter = GetObjectParameterExpression(ValueParameterExpressionName);
        var instanceCast = GetCastExpression(instanceParameter, propertyInfo.DeclaringType!);
        var valueCast = GetCastExpression(valueParameter, propertyInfo.PropertyType);
        var setMethodInfo = propertyInfo.GetSetMethod(nonPublic: true)!;
        var setMethod = propertyInfo.IsStatic()
            ? Expression.Call(setMethodInfo, valueCast)
            : Expression.Call(instanceCast, setMethodInfo, valueCast);
        var lambda = Expression.Lambda<Action<object, object?>>(
            setMethod,
            instanceParameter,
            valueParameter);
        return lambda.Compile();
    }
}
