namespace TylerDM.StandardLibrary.System;

public static class DateTimeExt
{
	public static DateOnly MonthBegin(this DateTime dt) =>
		new(dt.Year, dt.Month, 1);

	public static DateOnly MonthEnd(this DateTime dt) =>
		new(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));

	public static DateOnly? ToDateOnly(this DateTime? dtn) =>
		dtn is DateTime dt ? DateOnly.FromDateTime(dt) : null;

	public static string ToString(this DateTime? dtn) =>
		dtn is DateTime dt ? dt.ToString() : string.Empty;

	public static DateTime AddWeeks(this DateTime dateTime, int weeks) =>
		dateTime.AddWeeks(7 * weeks);
}
