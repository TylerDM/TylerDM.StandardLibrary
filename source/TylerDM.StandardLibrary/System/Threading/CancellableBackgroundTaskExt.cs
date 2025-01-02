namespace TylerDM.StandardLibrary.System.Threading;

public static class CancellableBackgroundTaskExt
{
    public static CancellableBackgroundTask RunInBackground(this Func<CancellationToken, Task> func) =>
        new(func);
}