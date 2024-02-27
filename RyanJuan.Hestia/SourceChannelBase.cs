#if NETCOREAPP3_1_OR_GREATER
using System.Threading.Channels;
using System.Threading;

namespace RyanJuan.Hestia;

public abstract class SourceChannelBase<TRequest, TResponse> : IDisposable
{
    protected SourceChannelBase()
    {
        _worker = Task.Factory.StartNew(WorkerJob, TaskCreationOptions.LongRunning);
    }

    private readonly CancellationTokenSource _disposedCts = new();

    private readonly Channel<TaskContainer> _channel =
        Channel.CreateUnbounded<TaskContainer>(
            new UnboundedChannelOptions
            {
                SingleReader = true,
            });

    [UsedImplicitly] private readonly Task _worker;

    [PublicAPI]
    public async ValueTask<TResponse> DoAsync(
        TRequest request,
        CancellationToken cancellationToken = default)
    {
        var taskCompletion = new TaskCompletionSource<TResponse>(TaskCreationOptions.RunContinuationsAsynchronously);
        await _channel.Writer.WriteAsync(new TaskContainer(request, taskCompletion), cancellationToken);
        return await taskCompletion.Task;
    }

    protected abstract ValueTask<TResponse> ActionAsync(
        TRequest request,
        CancellationToken cancellationToken = default);

    private async Task WorkerJob()
    {
        var cancellationToken = _disposedCts.Token;
        while (!cancellationToken.IsCancellationRequested)
        {
            TaskContainer? work = null;
            try
            {
                work = await _channel.Reader.ReadAsync(cancellationToken);
                var address = await ActionAsync(work.Request, cancellationToken);
                work.TaskCompletion.SetResult(address);
            }
            catch (Exception exception)
            {
                work?.TaskCompletion?.SetException(exception);
            }
        }
    }

    void IDisposable.Dispose()
    {
        _disposedCts.Cancel();
    }

#if NET6_0_OR_GREATER
    private record TaskContainer(TRequest Request, TaskCompletionSource<TResponse> TaskCompletion);
#else
    private class TaskContainer
    {
        public TaskContainer(TRequest request, TaskCompletionSource<TResponse> taskCompletion)
        {
            Request = request;
            TaskCompletion = taskCompletion;
        }

        public TRequest Request { get; }

        public TaskCompletionSource<TResponse> TaskCompletion { get; }
    }
#endif
}
#endif
