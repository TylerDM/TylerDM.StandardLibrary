namespace TylerDM.StandardLibrary.System.Threading;

public static class CancellationTokenExt
{
    public static async Task<bool> TryWaitForCancelAsync(this CancellationToken ct, TimeSpan timeout)
    {
        using var cancelCts = new CancellationTokenSource(timeout);
        return await TryWaitForCancelAsync(ct, cancelCts.Token);
    }
    
    public static async Task<bool> TryWaitForCancelAsync(this CancellationToken ct, CancellationToken cancelCt = default)
    {
        try
        {
            await ct.WaitForCancelAsync(cancelCt);
            return true;
        }
        catch (TaskCanceledException)
        {
            return false;
        }
    }

    public static async Task WaitForCancelAsync(this CancellationToken ct, TimeSpan timeout)
    {
        using var cancelCts = new CancellationTokenSource(timeout);
        await WaitForCancelAsync(ct, cancelCts.Token);
    }
    
    public static Task WaitForCancelAsync(this CancellationToken ct, CancellationToken cancelCt = default)
    {
        var tcs = new TaskCompletionSource();
        cancelCt.Register(() =>
        {
            if (tcs.Task.Status != TaskStatus.Running) return;
            tcs.SetCanceled();
        });
        ct.Register(() => tcs.SetResult());
        return tcs.Task;
    }
    
    public static bool ShouldContinue(this CancellationToken ct) =>
        ct.IsCancellationRequested == false;
}