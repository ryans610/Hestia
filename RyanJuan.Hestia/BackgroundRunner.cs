using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace RyanJuan.Hestia
{
    /// <summary>
    /// 
    /// </summary>
    public static class BackgroundRunner
    {
        private static readonly Action<Task> s_ignoreExceptionHandler = task =>
        {
            try
            {
                task.Wait();
            }
            catch (Exception)
            {
                //ignore error
            }
        };

        /// <summary>
        /// Fire and forget.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="exceptionHandler"></param>
        public static void Run(
            Action action,
            Action<Exception> exceptionHandler = null)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            var task = new Task(action);
            task.ContinueWith(
                exceptionHandler is null ?
                    s_ignoreExceptionHandler :
                    t => exceptionHandler(t.Exception.GetBaseException()),
                TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnFaulted);
            task.Start();
        }

        /// <summary>
        /// Fire and forget.
        /// </summary>
        /// <param name="function"></param>
        /// <param name="exceptionHandler"></param>
        public static void Run(
            Func<Task> function,
            Action<Exception> exceptionHandler = null)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            var task = new Task<Task>(function);
            //Task.Run(function).ContinueWith(
            //    exceptionHandler is null ?
            //        s_ignoreExceptionHandler :
            //        t => exceptionHandler(t.Exception.GetBaseException()),
            //    TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnFaulted);
            task.ContinueWith(
                exceptionHandler is null ?
                    s_ignoreExceptionHandler :
                    t => exceptionHandler(t.Exception.GetBaseException()),
                TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnFaulted);
            task.Start();
        }

        /// <summary>
        /// Fire and forget.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <param name="exceptionHandler"></param>
        public static void Run<TResult>(
            Func<Task<TResult>> function,
            Action<Exception> exceptionHandler = null)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            Task.Run(function).ContinueWith(
                exceptionHandler is null ?
                    s_ignoreExceptionHandler :
                    t => exceptionHandler(t.Exception.GetBaseException()),
                TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnFaulted);
        }

        public static void ParallelForAll<TSource>(
            this IEnumerable<TSource> source,
            Action<TSource> action,
            Action<Exception> exceptionHandler = null)
        {
            source.AsParallel().ForAll(item =>
            {
                try
                {
                    action(item);
                }
                catch (Exception exception)
                {
                    if (exceptionHandler.IsNotNull())
                    {
                        exceptionHandler(exception);
                    }
                }
            });
        }
    }
}
