namespace RyanJuan.Hestia;

public static partial class HestiaTask
{
#if !NET40
#if ZH_HANT
#else
#endif
    [PublicAPI]
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
            // ReSharper disable once UseNullPropagation
#pragma warning disable IDE0031
            if (exceptionHandler is not null)
#pragma warning restore IDE0031
            {
                exceptionHandler.Invoke(exception);
            }
        }
    }
#endif
}
