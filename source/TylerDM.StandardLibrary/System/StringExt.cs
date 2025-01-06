namespace TylerDM.StandardLibrary.System;

public static class StringExt
{
	#region const
	public static string[] LineEndings { get; } = ["\r\n", "\n"];

	private static readonly char[] _numbers = [.. "0123456789"];
	private const StringSplitOptions _removeAndTrim =
	StringSplitOptions.RemoveEmptyEntries |
	StringSplitOptions.TrimEntries;
	private const StringComparison _sc =
		StringComparison.InvariantCultureIgnoreCase;
	private static readonly char[] _wordDelineators = [.. " \t\n\r"];
	#endregion

	#region methods
	/// <summary>
	/// Throw if IsNullOrWhiteSpace() == false.
	/// </summary>
	public static void RequireContent(this string? value)
	{
		if (value.HasContent() == false) 
			throw new Exception("Unexpected null or whitespace string.");
	}

	/// <summary>
	/// Inverse of IsNullOrWhiteSpace().
	/// </summary>
	public static bool HasContent(this string? text) =>
		string.IsNullOrWhiteSpace(text) == false;
	
	public static bool IsEmpty(this string? text) =>
		string.IsNullOrWhiteSpace(text);

	public static string[] SplitLines(this string text) =>
		text.SplitX(LineEndings);

	public static string WhiteSpaceCoalesce(params string[] strings) =>
		strings.FirstOrDefault(x => string.IsNullOrWhiteSpace(x) == false) ?? "";

	public static string GetFirstLine(this string text) =>
		text
			.SplitLines()
			.FirstOrDefault() ?? string.Empty;

	public static string Remove(this string input, string remove) =>
		input.Replace(remove, string.Empty);

	public static string[] SplitX(this string text, string[] separators) =>
		text.Split(separators, _removeAndTrim);

	public static string[] SplitX(this string text, char ch) =>
		text.Split(ch, _removeAndTrim);

	public static string[] SplitX(this string text, string str) =>
		text.Split(str, _removeAndTrim);

	public static string[] SplitX(this string text, char[] chs) =>
		text.Split(chs, _removeAndTrim);

	public static bool ContainsX(this string text, char ch) =>
		text.Contains(ch, _sc);

	public static bool ContainsX(this string text, string str) =>
		text.Contains(str, _sc);

	public static int CountWords(this string text) =>
		text.Split(_wordDelineators, StringSplitOptions.RemoveEmptyEntries).Length;

	public static int Count(this string text, char ch) =>
		text.Count(x => x == ch);

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

	public static string WhitespaceCoalesce(this IEnumerable<string?> strs) =>
		strs.FirstOrDefault(x => string.IsNullOrWhiteSpace(x) == false) ?? "";

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
