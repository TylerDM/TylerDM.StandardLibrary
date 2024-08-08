namespace TylerDM.StandardLibrary.Optimization;

public static class CachedValueTests
{
	[Fact]
	public static void Test()
	{
		var calls = 0;
		var cachedValue = new CachedValue<int>(() => calls++);
		cachedValue.GetValue();
		cachedValue.GetValue();
		if (calls != 1) throw new Exception("Get function called an incorrect number of times.");
		cachedValue.Clear();
		cachedValue.GetValue();
		if (calls != 2) throw new Exception("Get function called an incorrect number of times.");
	}
}
