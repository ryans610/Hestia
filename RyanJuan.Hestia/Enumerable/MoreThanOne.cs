namespace RyanJuan.Hestia;

public static partial class HestiaEnumerable
{
#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static bool MoreThanOne<TSource>(
        this IEnumerable<TSource> source)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        return MoreThanInternal(source, 1);
    }
}
