using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RyanJuan.Hestia
{
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
        public static async void Run(
            Action action,
            Action<Exception?>? exceptionHandler = null)
        {
            Error.ThrowIfArgumentNull(nameof(action), action);
            try
            {
                await Task.Run(action);
            }
            catch (Exception ex) when (exceptionHandler is { })
            {
                exceptionHandler(ex);
            }
        }

        /// <summary>
        /// Fire and forget.
        /// </summary>
        /// <param name="function"></param>
        /// <param name="exceptionHandler"></param>
        public static async void Run(
            Func<Task> function,
            Action<Exception?>? exceptionHandler = null)
        {
            Error.ThrowIfArgumentNull(nameof(function), function);
            try
            {
                await function.Invoke();
            }
            catch (Exception ex) when (exceptionHandler is { })
            {
                exceptionHandler(ex);
            }
        }

        /// <summary>
        /// Fire and forget.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <param name="exceptionHandler"></param>
        public static async void Run<TResult>(
            Func<Task<TResult>> function,
            Action<Exception?>? exceptionHandler = null)
        {
            Error.ThrowIfArgumentNull(nameof(function), function);
            try
            {
                await function.Invoke();
            }
            catch (Exception ex) when (exceptionHandler is { })
            {
                exceptionHandler(ex);
            }
        }

        public static void ParallelForAll<TSource>(
            this IEnumerable<TSource> source,
            Action<TSource> action,
            Action<Exception?>? exceptionHandler = null)
        {
            source.AsParallel().ForAll(item =>
            {
                try
                {
                    action(item);
                }
                catch (Exception exception) when (exceptionHandler is { })
                {
                    exceptionHandler(exception);
                }
            });
        }
    }
#endif
}
