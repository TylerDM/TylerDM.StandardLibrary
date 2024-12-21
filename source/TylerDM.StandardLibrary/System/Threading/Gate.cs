namespace TylerDM.StandardLibrary.System.Threading;

public class Gate() : IDisposable
{
    private readonly CancellationTokenSource _cts = new();

    private TaskCompletionSource tcs = createTcs();

    public GateStatus Status
    {
        get
        {
            if (_cts.IsCancellationRequested) return GateStatus.Cancelled;
            return tcs.Task.IsCompleted ? GateStatus.Opened : GateStatus.Closed;
        }
    }

    /// <summary>
    /// Wait for the gate to be released. Throws if ThrowAllWaiters() is called.
    /// </summary>
    /// <returns>True if the gate was released. False if the timeout expired.</returns>
    public async Task<bool> TryWaitAsync(TimeSpan timeout)
    {
        if (Status == GateStatus.Opened) return true;

        using var cts = createCts(timeout);
        return await TryWaitAsync(cts.Token);
    }

    /// <summary>
    /// Wait for the gate to be released. Throws if ThrowAllWaiters() is called.
    /// </summary>
    /// <returns>True if the gate was released. False if the provided token was cancelled.</returns>
    public async Task<bool> TryWaitAsync(CancellationToken ct)
    {
        if (Status == GateStatus.Opened) return true;

        try
        {
            using var cts = createCts(ct);
            await tcs.Task.WaitAsync(cts.Token);
            return true;
        }
        catch (OperationCanceledException)
        {
            return false;
        }
        finally
        {
            throwIfCancelled();
        }
    }
    
    /// <summary>
    /// Wait for the gate to be released. Throws if ThrowAllWaiters() is called.
    /// </summary>
    /// <returns>True if the gate was released. False if the provided token was cancelled.</returns>
    public async Task<bool> TryWaitAsync()
    {
        if (Status == GateStatus.Opened) return true;

        try
        {
            await tcs.Task.WaitAsync(_cts.Token);
            return true;
        }
        catch (OperationCanceledException)
        {
            return false;
        }
        finally
        {
            throwIfCancelled();
        }
    }

    /// <summary>
    /// Wait for the gate to be released.
    /// </summary>
    public async Task WaitAsync(TimeSpan timeout)
    {
        if (Status == GateStatus.Opened) return;

        using var cts = createCts(timeout);
        await WaitAsync(cts.Token);
    }

    /// <summary>
    /// Wait for the gate to be released.
    /// </summary>
    public async Task WaitAsync(CancellationToken ct)
    {
        if (Status == GateStatus.Opened) return;
        
        using var cts = createCts(ct);
        await tcs.Task.WaitAsync(cts.Token);
        throwIfCancelled();
    }
    
    /// <summary>
    /// Wait for the gate to be released.
    /// </summary>
    public async Task WaitAsync()
    {
        if (Status == GateStatus.Opened) return;
        
        await tcs.Task.WaitAsync(_cts.Token);
        throwIfCancelled();
    }

    /// <summary>
    /// Release threads waiting at the gate.
    /// </summary>
    public void Open()
    {
        if (Status == GateStatus.Opened) return;
        
        throwIfCancelled();
        tcs.TrySetResult();
    }
    
    /// <summary>
    /// Release threads waiting at the gate.
    /// </summary>
    public void Close()
    {
        if (Status == GateStatus.Closed) return;
        
        throwIfCancelled();
        tcs = createTcs();
    }

    /// <summary>
    /// Throws an exception on threads waiting at the gate.
    /// </summary>
    public void Cancel()
    {
        if (Status == GateStatus.Cancelled) return;
        
        _cts.Cancel();
    }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    private static TaskCompletionSource createTcs() =>
        new(TaskCreationOptions.RunContinuationsAsynchronously);
    
    private void throwIfCancelled()
    {
        if (Status == GateStatus.Cancelled)
            throw new TaskCanceledException("The Gate was cancelled.");
    }

    private CancellationTokenSource createCts(TimeSpan timeout)
    {
        var value = createCts();
        value.CancelAfter(timeout);
        return value;
    }

    private CancellationTokenSource createCts(params CancellationToken[] externalCts) =>
        CancellationTokenSource.CreateLinkedTokenSource([_cts.Token, ..externalCts]);
}