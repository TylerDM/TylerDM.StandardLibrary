namespace TylerDM.StandardLibrary.System;

public static class DateOnlyExt
{
	public static DateTime ToDateTime(this DateOnly dateOnly) =>
		dateOnly.ToDateTime(TimeOnly.MinValue);
}
