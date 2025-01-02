namespace TylerDM.StandardLibrary.System.Threading;

public class CancellableBackgroundTaskTests
{
    [Fact]
    public async Task Test()
    {
        Task letPropagateAsync() =>
            TimeSpan.FromMilliseconds(10).WaitAsync(); 
        var running = false;
        
        var cbt = new CancellableBackgroundTask(async ct =>
        {
            running = true; 
            await ct.WaitForCancelAsync();
            running = false;
        });
        await letPropagateAsync();
        Assert.True(running);
        
        cbt.Dispose();
        await letPropagateAsync();
        Assert.False(running);
    }
}