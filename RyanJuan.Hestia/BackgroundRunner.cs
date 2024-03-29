namespace RyanJuan.Hestia;

#if !NET40
/// <summary>
/// 
/// </summary>
public static class BackgroundRunner
{
    /// <summary>
    /// Fire and forget.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="exceptionHandler"></param>
    [PublicAPI]
    public static async void Run(
        Action action,
        Action<Exception?>? exceptionHandler = null)
    {
        Error.ThrowIfArgumentNull(nameof(action), action);
        try
        {
            await Task.Run(action);
        }
        catch (Exception ex)
        {
            // ReSharper disable once UseNullPropagation
#pragma warning disable IDE0031
            if (exceptionHandler is not null)
#pragma warning restore IDE0031
            {
                exceptionHandler.Invoke(ex);
            }
        }
    }

    /// <summary>
    /// Fire and forget.
    /// </summary>
    /// <param name="function"></param>
    /// <param name="exceptionHandler"></param>
    [PublicAPI]
    public static async void Run(
        Func<Task> function,
        Action<Exception?>? exceptionHandler = null)
    {
        Error.ThrowIfArgumentNull(nameof(function), function);
        try
        {
            await function.Invoke();
        }
        catch (Exception ex)
        {
            // ReSharper disable once UseNullPropagation
#pragma warning disable IDE0031
            if (exceptionHandler is not null)
#pragma warning restore IDE0031
            {
                exceptionHandler.Invoke(ex);
            }
        }
    }

    /// <summary>
    /// Fire and forget.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="function"></param>
    /// <param name="exceptionHandler"></param>
    [PublicAPI]
    public static async void Run<TResult>(
        Func<Task<TResult>> function,
        Action<Exception?>? exceptionHandler = null)
    {
        Error.ThrowIfArgumentNull(nameof(function), function);
        try
        {
            await function.Invoke();
        }
        catch (Exception ex)
        {
            // ReSharper disable once UseNullPropagation
#pragma warning disable IDE0031
            if (exceptionHandler is not null)
#pragma warning restore IDE0031
            {
                exceptionHandler.Invoke(ex);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="action"></param>
    /// <param name="exceptionHandler"></param>
    [PublicAPI]
    public static void ParallelForAll<TSource>(
        this IEnumerable<TSource> source,
        Action<TSource> action,
        Action<Exception?>? exceptionHandler = null)
    {
        source.AsParallel().ForAll(item =>
        {
            try
            {
                action.Invoke(item);
            }
            catch (Exception ex)
            {
                // ReSharper disable once UseNullPropagation
#pragma warning disable IDE0031
                if (exceptionHandler is not null)
#pragma warning restore IDE0031
                {
                    exceptionHandler.Invoke(ex);
                }
            }
        });
    }
}
#endif
