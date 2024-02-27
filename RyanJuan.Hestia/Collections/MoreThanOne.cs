namespace RyanJuan.Hestia;

public static partial class HestiaCollections
{
#if ZH_HANT
#else
#endif
    public static bool MoreThanOne<TSource>(
        this IEnumerable<TSource> source)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        return MoreThanInternal(source, 1);
    }
}
