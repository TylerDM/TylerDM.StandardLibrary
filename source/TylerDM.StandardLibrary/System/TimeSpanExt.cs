namespace TylerDM.StandardLibrary.System;

public static class TimeSpanExt
{
	private static readonly double _daysInYear = 365.25d;
	private static readonly double _daysInMonth = 30.45d;
	private static readonly int _daysInWeek = 7;

	/// <summary>
	/// Waits the specified amount of time.  Does not throw on cancellation, instead returning a bool representing if the wait completed or not.
	/// </summary>
	public static async Task<bool> TryWaitAsync(this TimeSpan timeout, CancellationToken ct)
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

	/// <summary>
	/// Waits the specified amount of time.
	/// </summary>
	public static Task WaitAsync(this TimeSpan timeout, CancellationToken ct = default) =>
		Task.Delay(timeout, ct);

	public static TimeSpan Sum<T>(this IEnumerable<T> enumerable, Func<T, TimeSpan> select) =>
		enumerable.Select(select).Sum();

	public static TimeSpan Sum(this IEnumerable<TimeSpan> enumerable) =>
		new(enumerable.Select(x => x.Ticks).Sum());

	/// <summary>
	/// Converts a TimeSpan into a friendly string with the largest interval that is >= 1.  For example, a TimeSpan of 90 days would return "3 Months".
	/// </summary>
	public static string ToFriendlyString(this TimeSpan timeSpan, TimeIntervals maxInterval = TimeIntervals.Year)
	{
		var totalDays = timeSpan.TotalDays;

		if (maxInterval >= TimeIntervals.Year)
		{
			var years = totalDays / _daysInYear;
			if (years >= 1)
				return $"{years:N0} Years";
		}

		if (maxInterval >= TimeIntervals.Month)
		{
			var months = totalDays / _daysInMonth;
			if (months >= 1)
				return $"{months:N0} Months";
		}

		if (maxInterval >= TimeIntervals.Week)
		{
			var weeks = totalDays / _daysInWeek;
			if (weeks >= 1)
				return $"{weeks:N0} Weeks";
		}

		if (maxInterval >= TimeIntervals.Day)
		{
			if (totalDays >= 1)
				return $"{totalDays:N0} Days";
		}

		if (maxInterval >= TimeIntervals.Hour)
		{
			var hours = timeSpan.TotalHours;
			if (hours >= 1)
				return $"{hours:N0} Hours";
		}

		if (maxInterval >= TimeIntervals.Minute)
		{
			var minutes = timeSpan.TotalMinutes;
			if (minutes >= 1)
				return $"{minutes:N0} Minutes";
		}

		if (maxInterval >= TimeIntervals.Second)
		{
			var seconds = timeSpan.TotalSeconds;
			if (seconds >= 1)
				return $"{seconds:N0} Seconds";
		}

		return $"{timeSpan.TotalMilliseconds:N0} Milliseconds";
	}
}
