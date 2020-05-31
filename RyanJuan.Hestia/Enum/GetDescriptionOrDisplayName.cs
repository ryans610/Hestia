using System;
using System.ComponentModel;
#if NETCOREAPP3_0 || NETCOREAPP2_1
using System.ComponentModel.DataAnnotations;
#endif
using System.Reflection;

namespace RyanJuan.Hestia
{
    public static partial class HestiaEnum
    {
#if !NET40
#if ZH_HANT
#else
#endif
        public static string? GetDescriptionDisplayName<TEnum>(
            this TEnum value)
            where TEnum : Enum
        {
            var fieldInfo = ReflectionCenter.GetField(typeof(TEnum), value.ToString());
            if (fieldInfo is null)
            {
                return null;
            }
            var description = fieldInfo.GetCustomAttribute<DescriptionAttribute>();
            if (description?.Description?.IsNotNullOrEmpty() ?? false)
            {
                return description.Description;
            }
#if NETCOREAPP3_0 || NETCOREAPP2_1
            var displayName = fieldInfo.GetCustomAttribute<DisplayAttribute>();
            if (displayName?.Name?.IsNotNullOrEmpty() ?? false)
            {
                return displayName.Name;
            }
#endif
            return null;
        }
#endif
    }
}
