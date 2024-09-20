namespace TylerDM.StandardLibrary.Optimization;

public class AutoSizedListTests
{
	[Fact]
	public void Test()
	{
		const float multiplier = 1.25f;
		const int sampleSize = 10;
		const int initialValue = 10;

		int getExpectedSize(int size) =>
			Convert.ToInt32(size * multiplier);

		var factory = new AutoSizedListFactory<int>(sampleSize, initialValue, multiplier);

		using (var list = factory.Create())
		{
			Assert.Equal(getExpectedSize(10), list.Capacity);
			list.AddRange(Enumerable.Range(0, 30));
		}

		using (var list = factory.Create())
			Assert.Equal(getExpectedSize(20), list.Capacity);
	}
}
