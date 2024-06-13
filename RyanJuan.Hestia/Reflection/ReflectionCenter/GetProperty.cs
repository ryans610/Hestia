namespace RyanJuan.Hestia;

public static partial class ReflectionCenter
{
    private static readonly ConcurrentDictionary<TypeStringTuple, PropertyInfo?> s_cachedPropertyInfoByName = new();

    [PublicAPI]
    public static PropertyInfo? GetProperty(
        Type type,
        string name)
    {
        Error.ThrowIfArgumentNull(nameof(type), type);
        Error.ThrowIfArgumentNull(nameof(name), name);
        if (name.IsEmpty())
        {
            throw Error.Argument(
                nameof(name),
                $"{nameof(name)} can not be empty.");
        }

        if (s_cachedPropertyInfoByName.TryGetValue(
                new TypeStringTuple(type, name),
                out var value))
        {
            return value;
        }

        var property = type.GetProperty(name, GetAllBindingAttr);
        s_cachedPropertyInfoByName.TryAdd(
            new TypeStringTuple(type, name),
            property);
        return property;
    }

    private static PropertyInfo GetMostMatchProperty(
        IEnumerable<PropertyInfo> properties,
        Type type)
    {
        using var iterator = properties.GetEnumerator();
        if (!iterator.MoveNext())
        {
            return null;
        }

        var first = iterator.Current;
        if (!iterator.MoveNext())
        {
            return first;
        }

        var list = new List<PropertyInfo>
        {
            first,
            iterator.Current,
        };
        while (iterator.MoveNext())
        {
            list.Add(iterator.Current);
        }

        var declareType = type;
        PropertyInfo declaringTypeMatched = null;
        do
        {
            declaringTypeMatched = list.FirstOrDefault(x => x.DeclaringType == type);
            if (declaringTypeMatched is not null)
            {
                return declaringTypeMatched;
            }

            declareType = declareType.BaseType;
        } while (declareType != typeof(object));

        return list[0];
    }
}
