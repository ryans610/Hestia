using System;

namespace RyanJuan.Hestia
{
    public static partial class HestiaEnum
    {
#if ZH_HANT
#else
#endif
        public static TEnum[] GetValues<TEnum>()
        {
            return (TEnum[])Enum.GetValues(typeof(TEnum));
        }
    }
}
