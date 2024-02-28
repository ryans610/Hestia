namespace RyanJuan.Hestia;

public static partial class HestiaEnumerable
{
#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
#endif
    [PublicAPI]
    public static ReadOnlyCollection<TSource> ToReadOnlyCollection<TSource>(
        this IEnumerable<TSource> source)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        return source.ToList().AsReadOnly();
    }

    internal static ReadOnlyCollection<TSource> AsReadOnlyCollection<TSource>(
        this IEnumerable<TSource> source)
    {
        if (source is ReadOnlyCollection<TSource> readOnlyCollection)
        {
            return readOnlyCollection;
        }

        if (source is not IList<TSource> list)
        {
            list = source.ToList();
        }

        return new(list);
    }
}
