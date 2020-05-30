using System;
using System.Threading.Tasks;

namespace RyanJuan.Hestia
{
    public static partial class HestiaTask
    {
#if !NET40
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
            Error.ThrowIfArgumentNull(nameof(task), task);
            return task.GetAwaiter().GetResult();
        }
#endif
    }
}
