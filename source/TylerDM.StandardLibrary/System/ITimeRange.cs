namespace TylerDM.StandardLibrary.System;

public interface ITimeRange
{
	public static ITimeRange Parse(string text) =>
		TryParse(text) ??
		throw new ArgumentOutOfRangeException(nameof(text));

	public static ITimeRange? TryParse(string text)
	{
		var parts = text.SplitX('-');
		if (parts.Length is not 2) return null;

		var begin = parts[0];
		var end = parts[1];

		//This order cannot be changed as DateTime and DateOnly accept a wide range of values.

		if (
			Year.TryParse(begin) is Year yearBegin &&
			Year.TryParse(end) is Year yearEnd
		)
			return new YearRange(yearBegin, yearEnd);

		if (
			MonthYear.TryParse(begin) is MonthYear beginMonthYear &&
			MonthYear.TryParse(end) is MonthYear endMonthYear
		)
			return new MonthYearRange(beginMonthYear, endMonthYear);

		if (
			DateOnly.TryParse(begin, out var dateOnlyBegin) &&
			DateOnly.TryParse(end, out var dateOnlyEnd)
		)
			return new DateOnlyRange(dateOnlyBegin, dateOnlyEnd);

		if (
			DateTime.TryParse(begin, out var dateTimeBegin) &&
			DateTime.TryParse(end, out var dateTimeEnd)
		)
			return new DateTimeRange(dateTimeBegin, dateTimeEnd);

		return null;
	}
}

public interface ITimeRange<T> : ITimeRange
{
	T Start { get; }
	T End { get; }
}
