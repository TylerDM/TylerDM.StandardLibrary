namespace TylerDM.StandardLibrary.System.Collections.Generic;

public static class CollectionExt
{
	public static void AddRange<T>(this ICollection<T> list, IEnumerable<T> items)
	{
		foreach (var item in items)
			list.Add(item);
	}
}
