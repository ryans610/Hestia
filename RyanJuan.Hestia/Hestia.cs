namespace RyanJuan.Hestia;

/// <summary>
/// 
/// </summary>
public static partial class Hestia
{
    /// <summary>
    /// 
    /// </summary>
    public enum SupportedRuntimes
    {
        // ReSharper disable InconsistentNaming
        /// <summary>
        /// .Net Framework 4.0
        /// </summary>
        NET40,
        /// <summary>
        /// .Net Framework 4.5
        /// </summary>
        NET45,
        /// <summary>
        /// .Net Framework 4.6.2
        /// </summary>
        NET462,
        /// <summary>
        /// .Net Framework 4.8
        /// </summary>
        NET48,
        /// <summary>
        /// .Net Standard 2.0
        /// </summary>
        NETSTANDARD2_0,
        /// <summary>
        /// .Net Standard 2.1
        /// </summary>
        NETSTANDARD2_1,
        /// <summary>
        /// .Net Core 2.2
        /// </summary>
        NETCOREAPP2_2,
        /// <summary>
        /// .Net Core 3.1
        /// </summary>
        NETCOREAPP3_1,
        /// <summary>
        /// .Net 6.0
        /// </summary>
        NET6_0,
        // ReSharper restore InconsistentNaming
    }

#if ZH_HANT
        /// <summary>
        /// Hestia 的目標 .NET 平台。
        /// 如果沒有符合的平台，.NET 將會自己挑選最接近的版本。
        /// </summary>
#else
    /// <summary>
    /// The targeted .NET runtime of Hestia.
    /// .NET will automatic chosen the closest version
    /// if there is no matching runtime.
    /// </summary>
#endif
    public static SupportedRuntimes CurrentTargetedRuntimeVersion { get; } =
        SupportedRuntimes.
#if NET40
            NET40
#elif NET45
            NET45
#elif NET462
            NET462
#elif NET48
            NET48
#elif NETSTANDARD2_0
            NETSTANDARD2_0
#elif NETSTANDARD2_1
            NETSTANDARD2_1
#elif NETCOREAPP2_2
            NETCOREAPP2_2
#elif NETCOREAPP3_1
            NETCOREAPP3_1
#elif NET6_0
            NET6_0
#endif
        ;
}
