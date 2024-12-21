namespace TylerDM.StandardLibrary.Hosting;

public abstract class HostedService(ILogger? _logger = null): BackgroundService
{
    private readonly Gate _startupGate = new();

    private Task? startTask;
    private HostedServiceStatus status = HostedServiceStatus.Stopped; 
    
    public HostedServiceStatus Status
    {
        get => status;
        private set
        {
            status = value;
            _logger?.LogInformation($"{nameof(Status)}: {status}");
        }
    }

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
        catch (Exception ex)
        {
            Status = HostedServiceStatus.Crashed;
            _logger?.LogError(ex, $"Error occured during {nameof(startAsync)}().");
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
        catch (Exception ex)
        {
            Status = HostedServiceStatus.Crashed;
            _logger?.LogError(ex, $"Error occured during {nameof(executeAsync)}().");
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
        catch (Exception ex)
        {
            Status = HostedServiceStatus.Crashed;
            _logger?.LogError(ex, $"Error occured during {nameof(executeAsync)}().");
            throw;
        }
        Status = HostedServiceStatus.Stopped;
    }
    
    protected abstract Task executeAsync(CancellationToken ct);
    
    protected virtual Task startAsync(CancellationToken ct) => Task.CompletedTask;
    
    protected virtual Task stopAsync(CancellationToken ct) => Task.CompletedTask;
}
