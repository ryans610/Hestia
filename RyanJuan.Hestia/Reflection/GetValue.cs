using System;
using System.Linq.Expressions;
using System.Reflection;

namespace RyanJuan.Hestia
{
    public static partial class HestiaReflection
    {
#if ZH_HANT
        /// <summary>
        /// 以 <see cref="MemberInfo.MemberType"/> 判斷指定的成員是欄位或屬性，並從
        /// <paramref name="obj"/> 取值。
        /// 如果指定的成員並非欄位或屬性，則回傳 <see langword="null"/>。
        /// </summary>
        /// <param name="memberInfo">成員的 <see cref="MemberInfo"/>。</param>
        /// <param name="obj">要被取值的物件。</param>
        /// <returns>物件中指定的成員的值。</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type"/> 或 <paramref name="obj"/> 的值為 <see langword="null"/>。
        /// </exception>
#else
        /// <summary>
        /// Return the field or property value of object where the member type is decided by
        /// <see cref="MemberInfo.MemberType"/>.
        /// Return <see langword="null"/> if the member type is not valid.
        /// </summary>
        /// <param name="memberInfo">The <see cref="MemberInfo"/> of the member.</param>
        /// <param name="obj">The data instance.</param>
        /// <returns>Field or property value of object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="memberInfo"/> or <paramref name="obj"/> is <see langword="null"/>.
        /// </exception>
#endif
        public static object? GetValue(
            this MemberInfo memberInfo,
            object obj)
        {
            Error.ThrowIfArgumentNull(nameof(memberInfo), memberInfo);
            Error.ThrowIfArgumentNull(nameof(obj), obj);
            return memberInfo.MemberType switch
            {
                MemberTypes.Field => (memberInfo as FieldInfo)?.GetValue(obj),
                MemberTypes.Property => (memberInfo as PropertyInfo)?.GetValue(obj),
                _ => null,
            };
        }
    }
}
