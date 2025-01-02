namespace TylerDM.StandardLibrary.System.Threading;

public class CancellationTokenExtTests
{
    [Fact]
    public async Task WaitForCancelAsyncTest()
    {
        Task letPropagateAsync() =>
            TimeSpan.FromMilliseconds(100).WaitAsync(); 
        
        using var cts = new CancellationTokenSource();
        var running = false;
        
        void setRunning(bool value) =>
            running = value;
        
        var task = Task.Run(async () =>
        {
            setRunning(true);
            await cts.Token.WaitForCancelAsync();
            setRunning(false);
        });
        
        await letPropagateAsync();
        Assert.True(running);
        Assert.False(task.IsCompleted);

        cts.Cancel();
        await letPropagateAsync();
        Assert.False(running);
        Assert.True(task.IsCompletedSuccessfully);
    }
}