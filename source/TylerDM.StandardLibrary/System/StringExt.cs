namespace TylerDM.StandardLibrary.System;

public static class StringExt
{
	#region const
	private static readonly char[] _numbers = [.. "0123456789"];
	#endregion

	#region methods
	public static string SubstringSoft(this string str, int index, int length)
	{
		if (index > str.Length) return string.Empty;
		var remainingChars = str.Length - index;
		var maxLength = Math.Min(length, remainingChars);
		return str.Substring(index, maxLength);
	}

	public static string MaxLength(this string str, int max) =>
		str[..Math.Min(str.Length, max)];

	public static string OnlyNumbers(this string str) =>
		str.OnlyCharacters(_numbers);

	public static string OnlyCharacters(this string str, char[] whitelist) =>
		new(str.Where(x => whitelist.Contains(x)).ToArray());

	public static string WhitespaceCoalesce(this IEnumerable<string?> strs)
	{
		foreach (var str in strs)
			if (string.IsNullOrWhiteSpace(str) is false)
				return str;
		return string.Empty;
	}

	public static string WhitespaceCoalesce(params string?[] strs) =>
		WhitespaceCoalesce((IEnumerable<string?>)strs);

	public static string ToPlural(this string noun)
	{
		var last = noun.Last();
		if (last is 'y') return noun[..^1] + "ies";
		if (last is 's') return noun + "es";
		return noun + 's';
	}
	#endregion
}
