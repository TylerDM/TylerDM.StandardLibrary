namespace TylerDM.StandardLibrary.System.Threading;

/// <summary>
/// Represents a cancellable task running in the background. Calling Dispose() cancels the task. Use to run methods in the background until a scope ends.
/// </summary>
public class CancellableBackgroundTask : IDisposable
{
    private readonly CancellationTokenSource _cts = new();
    private readonly Task _task;
    
    private bool disposed = false;

    public CancellableBackgroundTask(Func<CancellationToken, Task> func) =>
        _task = Task.Run(() => func(_cts.Token));

    /// <summary>
    /// Wait for the task to complete, or the timeout to expire.
    /// </summary>
    public async Task WaitAsync(TimeSpan timeout)
    {
        using var cts = new CancellationTokenSource(timeout);
        await WaitAsync(cts.Token); 
    }
    
    /// <summary>
    /// Wait for the task to complete.
    /// </summary>
    public Task WaitAsync(CancellationToken ct = default) =>
        _task.WaitAsync(ct);

    /// <summary>
    /// Wait for the task to complete.
    /// </summary>
    /// <returns>True if the task completed successfully.</returns>
    public async Task<bool> TryWaitAsync(TimeSpan timeout)
    {
        using var cts = new CancellationTokenSource(timeout);
        return await TryWaitAsync(cts.Token); 
    }
    
    /// <summary>
    /// Wait for the task to complete.
    /// </summary>
    /// <returns>True if the task completed successfully.</returns>
    public Task<bool> TryWaitAsync(CancellationToken ct = default) =>
        _task.TryWaitAsync(ct);
    
    /// <summary>
    /// Cancels the background task and disposes unmanaged internal resources.
    /// </summary>
    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        
        _cts.Cancel();
        _cts.Dispose();
    }
}
