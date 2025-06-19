namespace TylerDM.StandardLibrary.System;

public class TimeSpanExtTests
{
	[Fact]
	public void ToFriendlyStringMaxInterval()
	{
		var eightDays = TimeSpan.FromDays(8);
		Assert.Equal("1 Weeks", eightDays.ToFriendlyString(TimeIntervals.Week));
		Assert.Equal("8 Days", eightDays.ToFriendlyString(TimeIntervals.Day));
	}
}
