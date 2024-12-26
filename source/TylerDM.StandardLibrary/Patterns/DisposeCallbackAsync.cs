namespace TylerDM.StandardLibrary.Patterns;

public class DisposeCallbackAsync(Func<Task> _callback) : IAsyncDisposable
{
    public async ValueTask DisposeAsync() =>
        await _callback();
}