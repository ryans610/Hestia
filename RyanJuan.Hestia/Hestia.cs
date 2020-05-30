using System;

namespace RyanJuan.Hestia
{
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
            /// <summary>
            /// .Net Framework 4.0
            /// </summary>
            NET40,
            /// <summary>
            /// .Net Framework 4.5
            /// </summary>
            NET45,
            /// <summary>
            /// .Net Standard 2.0
            /// </summary>
            NETSTANDARD2_0,
            /// <summary>
            /// .Net Standard 2.1
            /// </summary>
            NETSTANDARD2_1,
            /// <summary>
            /// .Net Core 2.1
            /// </summary>
            NETCOREAPP2_1,
            /// <summary>
            /// .Net Core 3.0
            /// </summary>
            NETCOREAPP3_0,
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
#elif NETSTANDARD2_0
            NETSTANDARD2_0
#elif NETSTANDARD2_1
            NETSTANDARD2_1
#elif NETCOREAPP2_1
            NETCOREAPP2_1
#elif NETCOREAPP3_0
            NETCOREAPP3_0
#endif
            ;
    }
}
