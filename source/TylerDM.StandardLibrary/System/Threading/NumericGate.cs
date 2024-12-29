namespace TylerDM.StandardLibrary.System.Threading;

/// <summary>
/// A version of Gate where it's open when the expression is true.  Default expression is == 0;
/// </summary>
public class NumericGate(int initialValue, Func<int, bool>? openWhen) : IDisposable
{
    private static readonly Func<int, bool> _defaultOpenWhen = x => x == 0;
    
    private readonly Gate _gate = new();
    private readonly Func<int, bool> _openWhen = openWhen ?? _defaultOpenWhen;
    
    private DisposedTracker<NumericGate> disposed;

    public int Value { get; private set; } = initialValue;

    public void Set(int value)
    {
        disposed.ThrowIf();
        
        Value = value;
        manageGate();
    }
    
    public void Increment()
    {
        disposed.ThrowIf();
        
        Value++;
        manageGate();
    }
    
    public void Decrement()
    {
        disposed.ThrowIf();
        
        Value--;
        manageGate();
    }

    public void Cancel()
    {
        disposed.ThrowIf();
        
        _gate.Cancel();
    }

    #region wait methods
    public Task<bool> TryWaitAsync(TimeSpan timeSpan) =>
        _gate.TryWaitAsync(timeSpan);
    
    public Task<bool> TryWaitAsync(CancellationToken ct) =>
        _gate.TryWaitAsync(ct);
    
    public Task<bool> TryWaitAsync() =>
        _gate.TryWaitAsync();
    
    public Task WaitAsync(TimeSpan timeSpan) =>
        _gate.WaitAsync(timeSpan);
    
    public Task WaitAsync(CancellationToken ct) =>
        _gate.WaitAsync(ct);
    
    public Task WaitAsync() =>
        _gate.WaitAsync();
    #endregion

    public void Dispose()
    {
        if (disposed) return;
        disposed.Dispose(); 
        
        _gate.Dispose();
    }

    private void manageGate() 
    {
        if (_openWhen(Value))
            _gate.Open();
        else
            _gate.Close();
    }
}