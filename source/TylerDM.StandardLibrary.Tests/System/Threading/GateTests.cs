namespace TylerDM.StandardLibrary.System.Threading;

public class GateTests
{
    [Fact]
    public async Task Cancelled()
    {
        using var cts = new CancellationTokenSource();
        var gate = new Gate();
        var task = Task.Run(() => gate.TryWaitAsync(cts.Token));
        Assert.False(task.IsCompleted);
        await cts.CancelAsync();
        await task.WaitAsync(TimeSpan.FromMilliseconds(100));
        Assert.True(task.IsCompletedSuccessfully);
        Assert.False(await task);
    }
    
    [Fact]
    public async Task Normal()
    {
        using var cts = new CancellationTokenSource();
        var gate = new Gate();
        var task = Task.Run(() => gate.TryWaitAsync(cts.Token));
        Assert.False(task.IsCompleted);
        gate.Release();
        await task.WaitAsync(TimeSpan.FromMilliseconds(100));
        Assert.True(task.IsCompletedSuccessfully);
        Assert.True(await task);
    }
}