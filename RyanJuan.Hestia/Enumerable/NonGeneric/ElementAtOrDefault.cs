using System.Collections;

namespace RyanJuan.Hestia.NonGeneric;

public static partial class HestiaNonGenericCollections
{
#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static object? ElementAtOrDefault(
        this IEnumerable source,
        int index,
        object? defaultValue)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        Error.ThrowIfArgumentSmallerThanZero(nameof(index), index);
        return ElementAtInternal(
            source,
            index,
            _ => defaultValue);
    }

#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static object? ElementAtOrDefault(
        this IEnumerable source,
        int index)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        Error.ThrowIfArgumentSmallerThanZero(nameof(index), index);
        return ElementAtInternalWithTypeCheck(
            source,
            index,
            (type, _) => type?.GetDefaultValue());
    }


    internal static object? ElementAtInternalWithTypeCheck(
        IEnumerable source,
        int index,
        Func<Type?, int, object?> outOfRangeHandler)
    {
        if (source is IList list)
        {
            if (index >= list.Count)
            {
                var typeList = list
                    .Where(x => x is not null)
                    .Select(x => x!.GetType())
                    .Cast<Type>()
                    .Distinct<Type>()
                    .ToList();
                return outOfRangeHandler.Invoke(
                    typeList.Count == 1 ? typeList[0] : null,
                    index);
            }
            return list[index];
        }
        IEnumerator? iterator = null;
        try
        {
            Type? elementType = null;
            var typeCheckStart = false;
            iterator = source.GetEnumerator();
            int current = 0;
            while (iterator.MoveNext())
            {
                if (current == index)
                {
                    return iterator.Current;
                }
                if (iterator.Current is not null)
                {
                    if (typeCheckStart)
                    {
                        if (elementType is not null)
                        {
                            var type = iterator.Current.GetType();
                            if (elementType != type)
                            {
                                elementType = null;
                            }
                        }
                    }
                    else
                    {
                        elementType = iterator.Current.GetType();
                        typeCheckStart = true;
                    }
                }
                current += 1;
            }
            return outOfRangeHandler.Invoke(elementType, index);
        }
        finally
        {
            if (iterator is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
