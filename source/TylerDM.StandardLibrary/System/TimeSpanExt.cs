namespace TylerDM.StandardLibrary.System;

public static class TimeSpanExt
{
	private static readonly double _daysInYear = 365.25d;
	private static readonly double _daysInMonth = 30.45d;
	private static readonly int _daysInWeek = 7;

	/// <summary>
	/// Waits the specified amount of time.  Does not throw on cancellation, instead returning a bool representing if the wait completed or not.
	/// </summary>
	public static async Task<bool> WaitAsync(this TimeSpan timeout, CancellationToken ct)
	{
		try
		{
			await Task.Delay(timeout, ct);
			return true;
		}
		catch (TaskCanceledException)
		{
			return false;
		}
	}
	
	public static TimeSpan Sum<T>(this IEnumerable<T> enumerable, Func<T, TimeSpan> select) =>
		enumerable.Select(select).Sum();

	public static TimeSpan Sum(this IEnumerable<TimeSpan> enumerable) =>
		new(enumerable.Select(x => x.Ticks).Sum());

	public static string ToFriendlyString(this TimeSpan timeSpan)
	{
		var totalDays = timeSpan.TotalDays;

		var years = totalDays / _daysInYear;
		if (years >= 1)
			return $"{years:N0} Years";

		var months = totalDays / _daysInMonth;
		if (months >= 1)
			return $"{months:N0} Months";

		var weeks = totalDays / _daysInWeek;
		if (weeks >= 1)
			return $"{weeks:N0} Weeks";

		if (totalDays >= 1)
			return $"{totalDays:N0} Days";

		var hours = timeSpan.TotalHours;
		if (hours >= 1)
			return $"{hours:N0} Hours";

		var minutes = timeSpan.TotalMinutes;
		if (minutes >= 1)
			return $"{minutes:N0} Minutes";

		var seconds = timeSpan.TotalSeconds;
		return $"{seconds:N0} Seconds";
	}
}
