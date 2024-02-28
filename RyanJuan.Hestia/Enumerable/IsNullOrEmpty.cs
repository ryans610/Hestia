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
    public static bool IsNullOrEmpty<TSource>(
        this IEnumerable<TSource> source)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        return source is null || IsEmptyInternal(source);
    }
}
