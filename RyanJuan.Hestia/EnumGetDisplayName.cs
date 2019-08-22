using System;
using System.Reflection;
using System.ComponentModel;

using System.ComponentModel.DataAnnotations;

namespace RyanJuan.Hestia
{
    public static partial class Hestia
    {
#if ZH_HANT
#else
#endif
        public static string GetDisplayName<TEnum>(
            this TEnum value)
            where TEnum : Enum
        {
            var fieldInfo = typeof(TEnum).GetField(value.ToString());
            var displayName = fieldInfo.GetCustomAttribute<DisplayAttribute>();
            if (displayName?.Name.IsNotNullOrEmpty()??false)
            {
                return displayName.Name;
            }

        }
    }
}
