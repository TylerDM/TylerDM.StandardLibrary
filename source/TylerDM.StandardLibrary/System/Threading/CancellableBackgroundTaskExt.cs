namespace TylerDM.StandardLibrary.System.Threading;

public static class CancellableBackgroundTaskExt
{
    public static CancellableBackgroundTask RunInBackground(this Func<CancellationToken, Task> func) =>
        new(func);
    
    public static CancellableBackgroundTask RunInBackground(this Action<CancellationToken> action) =>
        new(action);
}