namespace TylerDM.StandardLibrary.Patterns;

public class DisposedTracker<T>()
{
    private readonly CancellationTokenSource _cts = new();
    
    public bool Disposed { get; private set; } = false;
    
    /// <summary>
    /// A CancellationToken that cancels when the tracker is disposed.
    /// </summary>
    public CancellationToken CancellationToken => _cts.Token;

    /// <summary>
    /// Create a CancellationTokenSource which cancels when the supplied token is cancelled, or the DisposedTracker is disposed.
    /// </summary>
    public CancellationTokenSource CreateLinkedCts(CancellationToken token) =>
        CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, token);
    
    /// <summary>
    /// Throws ObjectDisposedException if the tracker has been disposed.
    /// </summary>
    public void ThrowIf() =>
        ObjectDisposedException.ThrowIf(Disposed, typeof(T));

    /// <summary>
    /// Disposes the tracker.
    /// </summary>
    /// <returns>True if already disposed and the caller should skip its dispose method.  False if it should execute its dispose method.</returns>
    /// <example>
    /// <code>
    /// public void Dispose()
    /// {
    ///     if (_disposeTracker.Dispose()) return;
    ///
    ///     //Clean up resources.
    /// }
    /// </code>
    /// </example>
    public bool Dispose()
    {
        if (Disposed) return true;
        Disposed = true;

        _cts.Cancel();
        _cts.Dispose();
        return false;
    }
}