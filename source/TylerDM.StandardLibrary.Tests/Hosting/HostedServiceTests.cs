#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

namespace TylerDM.StandardLibrary.Hosting;

public class HostedServiceTests
{
    private class TestService(
        bool startupCrash = false,
        bool executeCrash = false,
        bool stopCrash = false
    ) : HostedService
    {
        protected override Task startAsync(CancellationToken ct)
        {
            if (startupCrash) throw new Exception();
            return simulateWorkAsync();
        }

        protected override async Task executeAsync(CancellationToken ct)
        {
            while (ct.ShouldContinue())
            {
                await simulateWorkAsync();
                if (executeCrash) throw new Exception();
            }
        }

        protected override Task stopAsync(CancellationToken ct)
        {
            if (stopCrash) throw new Exception();
            return simulateWorkAsync();
        }
        
        private static Task simulateWorkAsync() =>
            Task.Delay(TimeSpan.FromMilliseconds(10));
    }

    [Fact]
    public async Task StartupSuccess()
    {
        using var service = new TestService();
        Assert.Equal(HostedServiceStatus.NotStarted, service.Status);
        
        service.StartAsync(CancellationToken.None);//Do not await.
        Assert.Equal(HostedServiceStatus.Starting, service.Status);
        
        await service.WaitForStartAsync(CancellationToken.None);
        Assert.Equal(HostedServiceStatus.Running, service.Status);
        
        var stopTask = service.StopAsync(CancellationToken.None);
        Assert.Equal(HostedServiceStatus.Stopping, service.Status);

        await stopTask;
        Assert.Equal(HostedServiceStatus.Stopped, service.Status);
    }
    
    [Fact]
    public async Task StartupTimeoutExpires()
    {
        using var service = new TestService();
        service.StartAsync(CancellationToken.None);
        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => service.WaitForStartAsync(TimeSpan.Zero)
        );
    }
    
    [Fact]
    public async Task StartupCrash()
    {
        using var service = new TestService(true);
        service.StartAsync(CancellationToken.None);
        await Assert.ThrowsAnyAsync<TaskCanceledException>(() => service.WaitForStartAsync(CancellationToken.None));
        Assert.Equal(HostedServiceStatus.Crashed, service.Status);
    }
    
    [Fact]
    public async Task ExecuteCrash()
    {
        using var service = new TestService(executeCrash: true);
        Assert.Equal(HostedServiceStatus.NotStarted, service.Status);
        await service.StartAsync(CancellationToken.None);
        await Task.Delay(TimeSpan.FromMilliseconds(20));
        Assert.Equal(HostedServiceStatus.Crashed, service.Status);
    }
}