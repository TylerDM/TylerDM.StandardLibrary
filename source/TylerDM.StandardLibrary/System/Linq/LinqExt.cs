namespace TylerDM.StandardLibrary.System.Linq;

public static class LinqExt
{
	public static IEnumerable<(T? Previous, T? Current, T? Next)> SelectWithSurrounding<T>(this IEnumerable<T> enumerable)
		where T : class
	{
		T? previous = null;
		T? current = null;
		T? next = null;

		(T?, T?, T?) build() => (previous, current, next);

		var first = true;
		foreach (var item in enumerable)
		{
			previous = current;
			current = next;
			next = item;

			if (first)
				first = false;
			else
				yield return build();
		}

		previous = current;
		current = next;
		next = default;
		yield return build();
	}

	public static IEnumerable<(T? Previous, T? Current, T? Next)> SelectWithSurroundingStruct<T>(this IEnumerable<T> enumerable)
		where T : struct
	{
		T? previous = null;
		T? current = null;
		T? next = null;

		(T?, T?, T?) build() => (previous, current, next);

		var first = true;
		foreach (var item in enumerable)
		{
			previous = current;
			current = next;
			next = item;

			if (first)
				first = false;
			else
				yield return build();
		}

		previous = current;
		current = next;
		next = default;
		yield return build();
	}

	public static IEnumerable<(int Index, T Element)> SelectWithIndex<T>(this T[] array)
	{
		var i = 0;
		foreach (var element in array)
			yield return (i++, element);
	}

	public static IEnumerable<T> SelectFollow<T>(this T root, Func<T, T?> getNext, bool includeRoot = true)
	{
		var next = includeRoot ? root : getNext(root);
		while (next is not null)
		{
			yield return next;
			next = getNext(next);
		}
	}

	public static IEnumerable<T> SelectMany<T>(this IEnumerable<IEnumerable<T>> enumerables) =>
		enumerables.SelectMany(x => x);
}
