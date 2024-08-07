namespace TylerDM.StandardLibrary.System;

public readonly struct DateTimeRange : ITimeRange<DateTime>
{
	#region properties
	public DateTime Start { get; }
	public DateTime End { get; }
	#endregion

	#region constructors
	public DateTimeRange(DateTime start, DateTime end)
	{
		ArgumentOutOfRangeException.ThrowIfEqual(start, default, nameof(start));
		ArgumentOutOfRangeException.ThrowIfEqual(start, DateTime.MinValue, nameof(start));
		ArgumentOutOfRangeException.ThrowIfEqual(start, DateTime.MaxValue, nameof(start));
		ArgumentOutOfRangeException.ThrowIfEqual(end, default, nameof(end));
		ArgumentOutOfRangeException.ThrowIfEqual(end, DateTime.MinValue, nameof(end));
		ArgumentOutOfRangeException.ThrowIfEqual(end, DateTime.MaxValue, nameof(end));
		ArgumentOutOfRangeException.ThrowIfGreaterThan(start, end);

		Start = start;
		End = end;
	}
	#endregion

	#region methods
	public static DateTimeRange? ParseNullable(string str)
	{
		if (TryParse(str, out var result)) return result;
		return null;
	}

	public static bool TryParse(string str, out DateTimeRange result)
	{
		var parts = splitSegments(str);
		if (
			DateTime.TryParse(parts[0], out var start) &&
			DateTime.TryParse(parts[1], out var end)
		)
		{
			result = new(start, end);
			return true;
		}

		result = default;
		return false;
	}

	public static DateTimeRange Parse(string str)
	{
		var parts = splitSegments(str);
		var start = DateTime.Parse(parts[0]);
		var end = DateTime.Parse(parts[1]);
		return new(start, end);
	}

	public override string ToString() =>
		$"{Start} - {End}";

	public string ToString(string? format) =>
		$"{Start.ToString(format)} - {End.ToString(format)}";
	#endregion

	#region private methods
	private static string[] splitSegments(string str) =>
		str.Split(" - ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
	#endregion
}
