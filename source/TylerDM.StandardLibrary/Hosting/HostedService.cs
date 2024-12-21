namespace TylerDM.StandardLibrary.Hosting;

public abstract class HostedService: BackgroundService
{
    private readonly Gate _startupGate = new();

    private Task? startTask;

    public HostedServiceStatus Status { get; private set; } = HostedServiceStatus.NotStarted;

    public Task WaitForStartAsync(TimeSpan timeout) =>
        _startupGate.WaitAsync(timeout);

    public Task WaitForStartAsync(CancellationToken ct = default) =>
        _startupGate.WaitAsync(ct);

    public sealed override async Task StartAsync(CancellationToken ct)
    {
        try
        {
            Status = HostedServiceStatus.Starting;
            await startAsync(ct);
            await base.StartAsync(ct);
            _startupGate.Open();
        }
        catch (OperationCanceledException)
        {
            Status = HostedServiceStatus.Stopping;
            _startupGate.Cancel();
        }
        catch
        {
            Status = HostedServiceStatus.Crashed;
            _startupGate.Cancel();
            throw;
        }
    }

    protected sealed override async Task ExecuteAsync(CancellationToken ct)
    {
        try
        {
            Status = HostedServiceStatus.Running;
            await executeAsync(ct);
        }
        catch (OperationCanceledException)
        {
            Status = HostedServiceStatus.Stopped;
        }
        catch
        {
            Status = HostedServiceStatus.Crashed;
            throw;
        }
    }

    public sealed override async Task StopAsync(CancellationToken ct)
    {
        try
        {
            Status = HostedServiceStatus.Stopping;
            if (ExecuteTask is not null)
            {
                await base.StopAsync(ct);
                await stopAsync(ct);
            }
        }
        catch (OperationCanceledException)
        {
        }
        Status = HostedServiceStatus.Stopped;
    }
    
    protected abstract Task executeAsync(CancellationToken ct);
    
    protected virtual Task startAsync(CancellationToken ct) => Task.CompletedTask;
    
    protected virtual Task stopAsync(CancellationToken ct) => Task.CompletedTask;
}
