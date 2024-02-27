﻿namespace RyanJuan.Hestia;

public static partial class HestiaString
{
#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
#endif
    public static bool IsNullOrEmpty(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [NotNullWhen(false)]
#endif
        this string? value)
    {
        return string.IsNullOrEmpty(value);
    }

#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
#endif
    public static bool IsNotNullOrEmpty(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [NotNullWhen(true)]
#endif
        this string? value)
    {
        return !string.IsNullOrEmpty(value);
    }
}
