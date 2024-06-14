namespace RyanJuan.Hestia;

/// <summary>
/// 
/// </summary>
public static partial class ReflectionCenter
{
    private const BindingFlags SystemDefaultBindingAttr =
        BindingFlags.Instance |
        BindingFlags.Static |
        BindingFlags.Public;

    private const BindingFlags GetAllBindingAttr =
        BindingFlags.Instance |
        BindingFlags.Static |
        BindingFlags.Public |
        BindingFlags.NonPublic;

    internal const BindingFlags DefaultInstanceBindingAttr =
        BindingFlags.Instance |
        BindingFlags.Public |
        BindingFlags.NonPublic;


    private const string InstanceParameterExpressionName = "instance";
    private const string ValueParameterExpressionName = "value";

    private static void ThrowIfIsIndexer(PropertyInfo propertyInfo)
    {
        // ReSharper disable once InvokeAsExtensionMethod
        if (HestiaReflection.IsIndexer(propertyInfo))
        {
            throw new InvalidOperationException("Property can not be indexer.");
        }
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

    private readonly struct TypeStringTuple(
        Type type,
        string name) :
        IEquatable<TypeStringTuple>
    {
        public Type Type { get; } = type;
        public string Name { get; } = name;

        public bool Equals(TypeStringTuple other) =>
            Type == other.Type && Name == other.Name;

        public override bool Equals(object? obj) =>
            obj is TypeStringTuple tuple && this.Equals(tuple);

        public override int GetHashCode() =>
#if NETCOREAPP2_1_OR_GREATER|| NETSTANDARD2_1_OR_GREATER
            HashCode.Combine(Type, Name);
#else
            unchecked((Type?.GetHashCode() ?? 0) * (Name?.GetHashCode() ?? 0));
#endif
    }

    private readonly struct TypeBindingFlagsTuple(
        Type type,
        BindingFlags bindingAttr) :
        IEquatable<TypeBindingFlagsTuple>
    {
        public Type Type { get; } = type;
        public BindingFlags BindingAttr { get; } = bindingAttr;

        public bool Equals(TypeBindingFlagsTuple other) =>
            Type == other.Type && BindingAttr == other.BindingAttr;

        public override bool Equals(object? obj) =>
            obj is TypeBindingFlagsTuple tuple && this.Equals(tuple);

        public override int GetHashCode() =>
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            HashCode.Combine(Type, BindingAttr);
#else
            unchecked((Type?.GetHashCode() ?? 0) * BindingAttr.GetHashCode());
#endif
    }
}
