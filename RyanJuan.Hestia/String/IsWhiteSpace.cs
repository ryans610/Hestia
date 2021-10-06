using System;
using System.Diagnostics.CodeAnalysis;

namespace RyanJuan.Hestia
{
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
        public static bool IsWhiteSpace(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            [NotNullWhen(true)]
#endif
            this string? value)
        {
            if (value == null)
            {
                return false;
            }
            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }
            return true;
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
#endif
        public static bool IsNotWhiteSpace(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            [NotNullWhen(false)]
#endif
            this string? value)
        {
            if (value == null)
            {
                return true;
            }
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
