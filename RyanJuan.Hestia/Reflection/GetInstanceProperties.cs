using System;
using System.Reflection;

namespace RyanJuan.Hestia
{
    public static partial class HestiaReflection
    {
#if ZH_HANT
        /// <summary>
        /// 取得指定的 <see cref="Type"/> 的公開和非公開、非靜態宣告屬性。
        /// </summary>
        /// <param name="type">指定的 <see cref="Type"/>。</param>
        /// <returns>
        /// 指定的 <see cref="Type"/> 的公開和非公開、非靜態宣告屬性。
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type"/> 的值為 <see langword="null"/>。
        /// </exception>
#else
        /// <summary>
        /// Search the properties of the specific <see cref="Type"/> which is declared instance
        /// member both public and non-public.
        /// </summary>
        /// <param name="type">The specific <see cref="Type"/>.</param>
        /// <returns>
        /// The properties of the specific <see cref="Type"/> which is declared instance
        /// member both public and non-public.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type"/> is <see langword="null"/>.
        /// </exception>
#endif
        public static PropertyInfo[] GetInstanceProperties(
            this Type type)
        {
            if (type is null)
            {
                throw Error.ArgumentNull(nameof(type));
            }
            return type.GetProperties(
                BindingFlags.DeclaredOnly |
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic);
        }
    }
}
