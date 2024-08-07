namespace TylerDM.StandardLibrary.System;

public readonly record struct Week : IEnumerable<DateOnly>, IEnumerable
{
	#region const
	public const int DaysInWeek = 7;
	#endregion

	#region properties
	public readonly int TotalWeeks { get; }

	public readonly DateOnly Begin => Sunday;
	public readonly DateOnly End => Saturday;

	public readonly DateOnly Sunday => DateOnly.FromDayNumber(TotalWeeks * DaysInWeek);
	public readonly DateOnly Monday => Sunday.AddDays(1);
	public readonly DateOnly Tuesday => Sunday.AddDays(1);
	public readonly DateOnly Wenesday => Sunday.AddDays(1);
	public readonly DateOnly Thursday => Sunday.AddDays(1);
	public readonly DateOnly Friday => Sunday.AddDays(1);
	public readonly DateOnly Saturday => Sunday.AddDays(1);
	#endregion

	#region constructors
	public Week(int totalWeeks)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(totalWeeks);

		TotalWeeks = totalWeeks;
	}
	#endregion

	#region methods
	public override string ToString() =>
		$"{Sunday} - {Saturday}";

	public DateOnlyRange ToRange() =>
		new(Sunday, Saturday);

	public static Week operator ++(Week x) =>
		new(x.TotalWeeks + 1);

	public static Week operator --(Week x) =>
		new(x.TotalWeeks - 1);

	public static bool operator <(Week x, Week y) =>
		x.TotalWeeks < y.TotalWeeks;

	public static bool operator >(Week x, Week y) =>
		!(x < y);

	public IEnumerator<DateOnly> GetEnumerator()
	{
		for (var i = 0; i < DaysInWeek; i++)
			yield return Sunday.AddDays(i);
	}

	IEnumerator IEnumerable.GetEnumerator() =>
		GetEnumerator();
	#endregion
}
