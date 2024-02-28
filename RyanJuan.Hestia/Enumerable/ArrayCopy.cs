namespace RyanJuan.Hestia;

public static partial class HestiaEnumerable
{
#if ZH_HANT
#else
#endif
    [PublicAPI]
    public static TSource[] Copy<TSource>(this TSource[] source)
    {
        Error.ThrowIfArgumentNull(nameof(source), source);
        var array = new TSource[source.Length];
        source.CopyTo(array, 0);
        return array;
    }
}
