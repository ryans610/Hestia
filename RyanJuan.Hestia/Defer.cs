namespace RyanJuan.Hestia;

#if ZH_HANT
#else
/// <summary>
/// 
/// </summary>
#endif
public static class Defer
{
#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <param name="deferAction"></param>
    /// <returns></returns>
#endif
    [PublicAPI]
    public static DeferDisposable Do(Action deferAction)
    {
        Error.ThrowIfArgumentNull(nameof(deferAction), deferAction);
        return new DeferDisposable(deferAction);
    }

#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
#endif
    public readonly struct DeferDisposable : IDisposable
    {
#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deferAction"></param>
#endif
        public DeferDisposable(Action deferAction) =>
            _deferAction = deferAction;

        private readonly Action _deferAction;

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
#endif
        public void Dispose() => _deferAction.Invoke();
    }

#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TParam"></typeparam>
    /// <param name="deferAction"></param>
    /// <param name="param"></param>
    /// <returns></returns>
#endif
    public static DeferDisposable<TParam> Do<TParam>(
        Action<TParam> deferAction,
        TParam param)
    {
        Error.ThrowIfArgumentNull(nameof(deferAction), deferAction);
        return new DeferDisposable<TParam>(deferAction, param);
    }

#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TParam"></typeparam>
#endif
    public readonly struct DeferDisposable<TParam> : IDisposable
    {
#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deferAction"></param>
        /// <param name="param"></param>
#endif
        public DeferDisposable(Action<TParam> deferAction, TParam param)
        {
            _deferAction = deferAction;
            _param = param;
        }

        private readonly Action<TParam> _deferAction;
        private readonly TParam _param;

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
#endif
        public void Dispose() => _deferAction.Invoke(_param);
    }

#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <param name="deferAction"></param>
    /// <returns></returns>
#endif
    public static DeferAsyncDisposable DoAsync(
        Func<ValueTask> deferAction)
    {
        Error.ThrowIfArgumentNull(nameof(deferAction), deferAction);
        return new DeferAsyncDisposable(deferAction);
    }

#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
#endif
    public readonly struct DeferAsyncDisposable : IAsyncDisposable
    {
#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deferAction"></param>
#endif
        public DeferAsyncDisposable(Func<ValueTask> deferAction) =>
            _deferAction = deferAction;

        private readonly Func<ValueTask> _deferAction;

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
#endif
        public ValueTask DisposeAsync() => _deferAction.Invoke();
    }

#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <param name="deferAction"></param>
    /// <param name="param"></param>
    /// <returns></returns>
#endif
    public static DeferAsyncDisposable<TParam> DoAsync<TParam>(
        Func<TParam, ValueTask> deferAction,
        TParam param)
    {
        Error.ThrowIfArgumentNull(nameof(deferAction), deferAction);
        return new DeferAsyncDisposable<TParam>(deferAction, param);
    }

#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
#endif
    public readonly struct DeferAsyncDisposable<TParam> : IAsyncDisposable
    {
#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deferAction"></param>
        /// <param name="param"></param>
#endif
        public DeferAsyncDisposable(
            Func<TParam, ValueTask> deferAction,
            TParam param) =>
            (_deferAction, _param) = (deferAction, param);

        private readonly Func<TParam, ValueTask> _deferAction;
        private readonly TParam _param;

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
#endif
        public ValueTask DisposeAsync() => _deferAction.Invoke(_param);
    }
#endif
}
