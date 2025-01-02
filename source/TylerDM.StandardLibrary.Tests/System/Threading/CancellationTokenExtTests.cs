namespace TylerDM.StandardLibrary.System.Threading;

public class CancellationTokenExtTests
{
    [Fact]
    public async Task WaitForCancelAsyncTest()
    {
        Task letPropagateAsync() =>
            TimeSpan.FromMilliseconds(1).WaitAsync(); 
        
        using var cts = new CancellationTokenSource();
        var running = false;
        
        var task = Task.Run(async () =>
        {
            running = true;
            await cts.Token.WaitForCancelAsync();
            running = false;
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