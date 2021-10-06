using System;
using System.Threading.Tasks;

namespace RyanJuan.Hestia
{
    public static partial class HestiaTask
    {
#if !NET40
#if ZH_HANT
#else
#endif
        public static async void FireAndForget(
            this Task task,
            Action<Exception>? exceptionHandler = null)
        {
            Error.ThrowIfArgumentNull(nameof(task), task);
            try
            {
                await task;
            }
            catch (Exception exception)
            {
                if (exceptionHandler is { })
                {
                    exceptionHandler(exception);
                }
            }
        }
#endif
    }
}
