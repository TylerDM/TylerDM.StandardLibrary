namespace TylerDM.StandardLibrary.Time;

public class ITimeRangeTests
{
	[Fact]
	public void ParseDateRange()
	{
		var start = DateOnly.Parse("12/30/2023");
		var end = DateOnly.Parse("12/31/2023");
		var test = $"{start} - {end}";
		var result = ITimeRange.Parse(test);
		if (result is not DateOnlyRange range)
			throw new Exception($"Parse() did not return {nameof(DateOnlyRange)}.");
		if (range.Start != start)
			throw new Exception($"{nameof(range.Start)} value doesn't match.");
		if (range.End != end)
			throw new Exception($"{nameof(range.End)} End value doesn't match.");
	}

	[Fact]
	public void ParseDateTimeRange()
	{
		var start = DateTime.Parse("12/30/2023 2:31PM");
		var end = DateTime.Parse("12/31/2023 5:00PM");
		var test = $"{start} - {end}";
		var result = ITimeRange.Parse(test);
		if (result is not DateTimeRange range)
			throw new Exception($"Parse() did not return {nameof(DateTimeRange)}.");
		if (range.Start != start)
			throw new Exception($"{nameof(range.Start)} value doesn't match.");
		if (range.End != end)
			throw new Exception($"{nameof(range.End)} End value doesn't match.");
	}

	[Fact]
	public void ParseYearRange()
	{
		var start = Year.Parse("2021");
		var end = Year.Parse("2025");
		var test = $"{start} - {end}";
		var result = ITimeRange.Parse(test);
		if (result is not YearRange range)
			throw new Exception($"Parse() did not return {nameof(YearRange)}.");
		if (range.Start != start)
			throw new Exception($"{nameof(range.Start)} value doesn't match.");
		if (range.End != end)
			throw new Exception($"{nameof(range.End)} End value doesn't match.");
	}

	[Fact]
	public void ParseMonthYearRange()
	{
		var start = MonthYear.Parse("March 2021");
		var end = MonthYear.Parse("January 2025");
		var test = $"{start} - {end}";
		var result = ITimeRange.Parse(test);
		if (result is not MonthYearRange range)
			throw new Exception($"Parse() did not return {nameof(MonthYearRange)}.");
		if (range.Start != start)
			throw new Exception($"{nameof(range.Start)} value doesn't match.");
		if (range.End != end)
			throw new Exception($"{nameof(range.End)} End value doesn't match.");
	}
}
