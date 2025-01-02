using TylerDM.StandardLibrary.System.Threading.Tasks;

namespace TylerDM.StandardLibrary.System.Threading;

public class GateTests
{
    private static readonly TimeSpan _timeout = TimeSpan.FromSeconds(1);
    
    [Fact]
    public async Task ExternalCtsCancelled()
    {
        using var cts = new CancellationTokenSource();
        using var gate = new Gate();
        
        var gateWait = gate.TryWaitAsync(cts.Token);
        Assert.False(gateWait.IsCompleted);
        
        await cts.CancelAsync();
        await gateWait.TryWaitAsync(_timeout);
        
        Assert.True(gateWait.IsCompletedSuccessfully);
        Assert.False(await gateWait);
    }
    
    [Fact]
    public async Task Normal()
    {
        using var gate = new Gate();
        Assert.Equal(GateStatus.Closed, gate.Status);
        
        var gateWait = gate.TryWaitAsync();
        Assert.False(gateWait.IsCompleted);
        Assert.Equal(GateStatus.Closed, gate.Status);
        
        gate.Open();
        Assert.Equal(GateStatus.Opened, gate.Status);
        
        await gateWait.TryWaitAsync(_timeout);
        Assert.True(gateWait.IsCompletedSuccessfully);
        Assert.True(await gateWait);
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