namespace RyanJuan.Hestia;

public static partial class Hestia
{
#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
#endif
    [PublicAPI]
    public static bool IsNull<T>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [NotNullWhen(false)]
#endif
        this T obj)
        where T : class
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        return obj is null;
    }

#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
#endif
    [PublicAPI]
    public static bool IsNotNull<T>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [NotNullWhen(true)]
#endif
        this T obj)
        where T : class
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        return obj is not null;
    }
}
