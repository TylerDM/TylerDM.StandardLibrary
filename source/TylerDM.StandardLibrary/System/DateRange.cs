﻿namespace TylerDM.StandardLibrary.System;

public readonly struct DateRange
{
	#region properties
	public DateOnly Start { get; }
	public DateOnly End { get; }
	#endregion

	#region constructors
	public DateRange(DateOnly start, DateOnly end)
	{
		ArgumentOutOfRangeException.ThrowIfEqual(start, default, nameof(start));
		ArgumentOutOfRangeException.ThrowIfEqual(start, DateOnly.MinValue, nameof(start));
		ArgumentOutOfRangeException.ThrowIfEqual(start, DateOnly.MaxValue, nameof(start));
		ArgumentOutOfRangeException.ThrowIfEqual(end, default, nameof(end));
		ArgumentOutOfRangeException.ThrowIfEqual(end, DateOnly.MinValue, nameof(end));
		ArgumentOutOfRangeException.ThrowIfEqual(end, DateOnly.MaxValue, nameof(end));
		ArgumentOutOfRangeException.ThrowIfGreaterThan(start, end);

		Start = start;
		End = end;
	}
	#endregion

	#region methods
	public IEnumerable<DateOnly> GetDatesInclusive()
	{
		var current = Start;
		while (current < End)
		{
			yield return current;
			current = current.AddDays(1);
		}
		yield return current;
	}

	public static DateRange? ParseNullable(string str)
	{
		if (TryParse(str, out var result)) return result;
		return null;
	}

	public static bool TryParse(string str, out DateRange result)
	{
		var parts = splitSegments(str);
		if (
			DateOnly.TryParse(parts[0], out var start) &&
			DateOnly.TryParse(parts[1], out var end)
		)
		{
			result = new(start, end);
			return true;
		}

		result = default;
		return false;
	}

	public static DateRange Parse(string str)
	{
		var parts = splitSegments(str);
		var start = DateOnly.Parse(parts[0]);
		var end = DateOnly.Parse(parts[1]);
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
