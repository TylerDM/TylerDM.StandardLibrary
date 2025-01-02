namespace TylerDM.StandardLibrary.System.Threading;

public class CancellableBackgroundTaskTests
{
    [Fact]
    public async Task Test()
    {
        var running = false;
        using var gate = new Gate();
        
        var cbt = new CancellableBackgroundTask(async ct =>
        {
            running = true;
            gate.Open();
            await ct.WaitForCancelAsync();
            running = false;
            gate.Open();
        });
        await gate.WaitAsync();
        Assert.True(running);
        gate.Close();
        
        cbt.Dispose();
        await gate.WaitAsync();
        Assert.False(running);
    }
}