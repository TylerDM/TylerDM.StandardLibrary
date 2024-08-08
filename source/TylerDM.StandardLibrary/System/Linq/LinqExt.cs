namespace TylerDM.StandardLibrary.System.Linq;

public static class LinqExt
{
	public static IEnumerable<T> SelectFollow<T>(this T start, Func<T, T?> getNext)
	{
		var next = start;
		while (next is not null)
		{
			yield return next;
			next = getNext(next);
		}
	}
}
