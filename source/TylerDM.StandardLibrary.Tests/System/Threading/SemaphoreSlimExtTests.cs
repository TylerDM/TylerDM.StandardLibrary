namespace TylerDM.StandardLibrary.System.Threading;

public class SemaphoreSlimExtTests
{
	[Fact]
	public void ParallelismTest()
	{
		var semaphore = new SemaphoreSlim(1, 1);
		Parallel.For(1, 1_000, (x, y) => semaphore.WaitThen(() => { }));
	}
}
