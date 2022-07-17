using System.Collections.Concurrent;
using System.Threading.Channels;

namespace RyanJuan.Hestia.AspNetCore.Geo;

public class GeocodingSourceChannel : IGeocoding, IDisposable
{
    /// <inheritdoc cref="GeocodingSourceChannel"/>
    /// <param name="geocodingSource"></param>
    public GeocodingSourceChannel(IGeocoding geocodingSource)
    {
        _geocodingSource = geocodingSource;
        _worker = Task.Factory.StartNew(WorkerJob, TaskCreationOptions.LongRunning);
    }

    private readonly IGeocoding _geocodingSource;
    private readonly CancellationTokenSource _disposedCts = new();
    private readonly Channel<LatLngTaskContainer> _channel = Channel.CreateUnbounded<LatLngTaskContainer>(new UnboundedChannelOptions
    {
        SingleReader = true,
    });
    private readonly Task _worker;

    public async ValueTask<string?> GetAddressFromLatLngAsync(double lat, double lng, CancellationToken cancellationToken = default)
    {
        var taskCompletion = new TaskCompletionSource<string?>(TaskCreationOptions.RunContinuationsAsynchronously);
        await _channel.Writer.WriteAsync(new LatLngTaskContainer(lat, lng, taskCompletion), cancellationToken);
        return await taskCompletion.Task;
    }

    private async Task WorkerJob()
    {
        var cancellationToken = _disposedCts.Token;
        while (!cancellationToken.IsCancellationRequested)
        {
            LatLngTaskContainer? work = null;
            try
            {
                work = await _channel.Reader.ReadAsync(cancellationToken);
                var address = await _geocodingSource.GetAddressFromLatLngAsync(work.Lat, work.Lng, cancellationToken);
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

    private record LatLngTaskContainer(double Lat, double Lng, TaskCompletionSource<string?> TaskCompletion);
}
