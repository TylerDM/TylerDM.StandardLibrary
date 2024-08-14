namespace TylerDM.StandardLibrary.System.Collections.Generic;

public static class IEnumerableExt
{
	public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
	{
		foreach (var item in enumerable)
			action(item);
	}

	public static IEnumerable<T> SelectMany<T>(this IEnumerable<IEnumerable<T>> enumerable) =>
			enumerable.SelectMany(x => x);

	public static bool None<TSource>(this IEnumerable<TSource> enumerable, Func<TSource, bool> predicate) =>
			enumerable.Any(predicate) == false;

	public static bool None<TSource>(this IEnumerable<TSource> enumerable) =>
			enumerable.Any() == false;

	public static TSource WithMax<TSource, TKey>(this IEnumerable<TSource> enumerable, Func<TSource, TKey> func) =>
			enumerable.OrderByDescending(x => func(x)).First();

	public static TSource WithMin<TSource, TKey>(this IEnumerable<TSource> enumerable, Func<TSource, TKey> func) =>
			enumerable.OrderBy(x => func(x)).First();

	public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> enumerable)
			where T : notnull
	{
		foreach (var item in enumerable)
			if (item is not null)
				yield return item;
	}

	public static IEnumerable<string> WhereNotNullOrWhitespace<T>(this IEnumerable<string?> enumerable)
	{
		foreach (var item in enumerable)
			if (string.IsNullOrWhiteSpace(item) == false)
				yield return item;
	}
}
