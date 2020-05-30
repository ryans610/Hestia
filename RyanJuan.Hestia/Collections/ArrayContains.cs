using System;
using System.Collections;
using System.Collections.Generic;

namespace RyanJuan.Hestia
{
    public static partial class HestiaCollections
    {
#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
#endif
        public static bool Contains<TArray>(
            this TArray[] array,
            TArray value)
        {
            return Array.IndexOf(array, value) != -1;
        }
    }
}
