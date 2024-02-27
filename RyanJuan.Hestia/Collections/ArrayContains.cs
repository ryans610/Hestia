namespace RyanJuan.Hestia;

public static partial class HestiaCollections
{
#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <param name="array"></param>
    /// <param name="value"></param>
    /// <returns></returns>
#endif
    [PublicAPI]
    public static bool Contains<TArray>(
        this TArray[] array,
        TArray value)
    {
        return Array.IndexOf(array, value) != -1;
    }
}
