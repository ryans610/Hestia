using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RyanJuan.Hestia
{
    public static partial class HestiaEnum
    {
#if ZH_HANT
#else
#endif
        public static string GetDisplayName<TEnum>(
            this TEnum value)
            where TEnum : Enum
        {
            var fieldInfo = typeof(TEnum).GetField(value.ToString());
            if (fieldInfo is null)
            {
                return string.Empty;
            }
            var displayName = fieldInfo.GetCustomAttribute<DisplayAttribute>();
            if (displayName?.Name.IsNotNullOrEmpty() ?? false)
            {
                return displayName.Name;
            }
            return string.Empty;
        }
    }
}
