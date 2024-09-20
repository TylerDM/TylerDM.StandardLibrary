namespace TylerDM.StandardLibrary.Statistics;

public class MovingAverageIntTests
{
	[Fact]
	public void Test()
	{
		var x = new MovingAverageInt(5);
		Assert.True(x.Empty);
		Assert.False(x.Full);
		Assert.ThrowsAny<Exception>(() => x.GetAverage());
		Assert.False(x.TryGetAverage(out var _));

		x.Add(1);
		Assert.False(x.Empty);
		Assert.False(x.Full);
		Assert.Equal(1, x.GetAverage());
		var trySuccess = x.TryGetAverage(out var tryResult);
		Assert.True(trySuccess);
		Assert.Equal(1, tryResult);

		x.Add(10);
		Assert.Equal(5, x.GetAverage());
	}
}
