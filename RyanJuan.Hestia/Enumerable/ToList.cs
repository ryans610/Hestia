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
    /// <param name="capacity"></param>
    /// <returns></returns>
#endif
    [PublicAPI]
    public static List<TSource> ToList<TSource>(
        this IEnumerable<TSource> source,
        int capacity)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        if (capacity < 0)
        {
            throw Error.ArgumentOutOfRange(
                nameof(capacity),
                $"{nameof(capacity)} is less than 0.",
                capacity);
        }
        var list = new List<TSource>(capacity);
        list.AddRange(source);
        return list;
    }
}
