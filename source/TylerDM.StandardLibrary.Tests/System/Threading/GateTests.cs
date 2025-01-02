namespace TylerDM.StandardLibrary.System.Threading;

public class GateTests
{
    [Fact]
    public async Task ExternalCtsCancelled()
    {
        using var cts = new CancellationTokenSource();
        var gate = new Gate();
        var task = Task.Run(() => gate.TryWaitAsync(cts.Token));
        Assert.False(task.IsCompleted);
        await cts.CancelAsync();
        await task.WaitAsync(TimeSpan.FromMilliseconds(10));
        Assert.True(task.IsCompletedSuccessfully);
        Assert.False(await task);
    }
    
    [Fact]
    public async Task Normal()
    {
        using var gate = new Gate();
        Assert.Equal(GateStatus.Closed, gate.Status);
        var task = Task.Run(() => gate.TryWaitAsync());
        Assert.False(task.IsCompleted);
        gate.Open();
        Assert.Equal(GateStatus.Opened, gate.Status);
        await task.WaitAsync(TimeSpan.FromMilliseconds(10));
        Assert.True(task.IsCompletedSuccessfully);
        Assert.True(await task);
    }
    
    [Fact]
    public async Task Close()
    {
        using var gate = new Gate();
        gate.Open();
        Assert.Equal(GateStatus.Opened, gate.Status);
        
        var openedTask = Task.Run(() => gate.TryWaitAsync());
        await Task.Yield();
        Assert.True(openedTask.IsCompletedSuccessfully);

        gate.Close();
        Assert.Equal(GateStatus.Closed, gate.Status);
        
        var closedTask = Task.Run(() => gate.TryWaitAsync());
        await Task.Yield();
        Assert.False(closedTask.IsCompletedSuccessfully);
    }

    [Fact]
    public void Cancelled()
    {
        using var gate = new Gate();
        Assert.Equal(GateStatus.Closed, gate.Status);
        
        gate.Cancel();
        Assert.Equal(GateStatus.Cancelled, gate.Status);
        
        var waitTask = gate.WaitAsync();
        Assert.True(waitTask.IsCompleted);
        Assert.False(waitTask.IsCompletedSuccessfully);
        
        var tryWaitTask = gate.TryWaitAsync();
        Assert.True(tryWaitTask.IsCompleted);
        Assert.False(tryWaitTask.IsCompletedSuccessfully);
    }
}