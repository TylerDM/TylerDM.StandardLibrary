namespace TylerDM.StandardLibrary.Patterns;

public class RunOnceTests
{
	[Fact]
	public void Test()
	{
		var count = 0;
		var runOnce = new RunOnce(() => count++);
		Assert.Equal(0, count);
		runOnce.Run();
		Assert.Equal(1, count);
		runOnce.Run();
		Assert.Equal(1, count);
	}

	[Fact]
	public async Task TestAsync()
	{
		var count = 0;
		var runOnce = new RunOnceAsync(async () => count++);
		Assert.Equal(0, count);
		await runOnce.RunAsync();
		Assert.Equal(1, count);
		await runOnce.RunAsync();
		Assert.Equal(1, count);
	}
}
