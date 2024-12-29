namespace TylerDM.StandardLibrary.Patterns;

public struct DisposedTracker<T>() : IDisposable
{
    private readonly CancellationTokenSource _cts = new();

    public bool Disposed { get; private set; } = false;
    public readonly CancellationToken CancellationToke => _cts.Token;

    public CancellationTokenSource CreateLinkedCts(CancellationToken token) =>
        CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, token);
    
    public readonly void ThrowIf() =>
        ObjectDisposedException.ThrowIf(Disposed, typeof(T));

    public void Dispose()
    {
        if (Disposed) return;
        Disposed = true;

        _cts.Dispose();
    }

    public static implicit operator bool(DisposedTracker<T> tracker) =>
        tracker.Disposed;
}