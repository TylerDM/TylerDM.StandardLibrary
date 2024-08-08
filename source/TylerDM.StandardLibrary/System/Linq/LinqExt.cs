namespace TylerDM.StandardLibrary.System.Linq;

public static class LinqExt
{
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
