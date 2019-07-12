using System;
using System.Threading.Tasks;

namespace RyanJuan.Hestia
{
    public static partial class HestiaTask
    {
#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="task"></param>
        /// <returns></returns>
#endif
        public static TResult WaitResult<TResult>(
            this Task<TResult> task)
        {
            return task.GetAwaiter().GetResult();
        }
    }
}
