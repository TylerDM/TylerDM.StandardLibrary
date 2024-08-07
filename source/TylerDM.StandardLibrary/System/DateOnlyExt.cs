namespace TylerDM.StandardLibrary.System;

public static class DateOnlyExt
{
	public static DateTime ToDateTime(this DateOnly dateOnly) =>
		dateOnly.ToDateTime(TimeOnly.MinValue);

	public static DateOnly MonthBegin(this DateOnly dt) =>
		new(dt.Year, dt.Month, 1);

	public static DateOnly MonthEnd(this DateOnly dt) =>
		new(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
}
