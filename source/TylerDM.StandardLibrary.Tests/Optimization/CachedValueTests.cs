namespace TylerDM.StandardLibrary.Optimization;

public class CachedValueTests
{
	[Fact]
	public void Test()
	{
		var calls = 0;
		var cachedValue = new CachedValue<int>(() => calls++);
		cachedValue.GetValue();
		cachedValue.GetValue();
		Assert.Equal(1, calls);
		cachedValue.Clear();
		cachedValue.GetValue();
		Assert.Equal(2, calls);
	}

	[Fact]
	public void TestThreadSafety()
	{
		const int targetIterations = 100;

		var acts = 0;
		var cachedValue = new CachedValue<int>(() => acts++);

		Parallel.For(0, targetIterations, x => cachedValue.GetValue());
		Assert.Equal(1, acts);
	}
}
