namespace TylerDM.StandardLibrary.System;

public readonly record struct MonthYear(
	int TotalMonths
)
{
	#region const
	private const StringSplitOptions _sso = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
	#endregion

	#region properties
	public readonly Months Month => (Months)(TotalMonths % 12 + 1);
	public readonly Year Year => new(TotalMonths / 12);
	#endregion

	#region constructors
	public MonthYear(int month, int year) : this(year * 12 + month)
	{
	}

	public MonthYear(Months month, Year year) : this((int)month - 1, year)
	{
	}
	#endregion

	#region private methods
	private static Months? tryParseMonth(string text)
	{
		if (Enum.TryParse(typeof(Months), text, true, out var e))
			return (Months)e;

		return null;
	}
	#endregion

	#region operators
	public static MonthYear Parse(string text) =>
		TryParse(text) ??
		throw new ArgumentOutOfRangeException(nameof(text));

	public static MonthYear? TryParse(string text)
	{
		var parts = text.Split(' ', _sso);
		if (parts.Length is not 2) return null;

		if (tryParseMonth(parts[0]) is not Months month)
			return null;

		if (Year.TryParse(parts[1]) is not Year year)
			return null;

		return new(month, year);
	}

	public static MonthYear operator ++(MonthYear x) =>
		new(x.TotalMonths + 1);

	public static MonthYear operator --(MonthYear x) =>
		new(x.TotalMonths - 1);

	public static bool operator <(MonthYear x, MonthYear y) =>
		x.TotalMonths < y.TotalMonths;

	public static bool operator >(MonthYear x, MonthYear y) =>
		!(x < y);
	#endregion

	#region methods
	public override string ToString() =>
		$"{Month} {Year}";
	#endregion
}