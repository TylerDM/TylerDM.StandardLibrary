namespace TylerDM.StandardLibrary.Patterns;

public class DisposeCallback(Action _callback) : IDisposable
{
    public void Dispose() =>
        _callback();
}