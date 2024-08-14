namespace TylerDM.StandardLibrary.System.Collections.Generic;

public static class CollectionExt
{
	public static void AddRange<T>(this ICollection<T> list, IEnumerable<T> items) =>
		items.ForEach(list.Add);

	public static void AddRange<T>(this ICollection<T> list, params T[] items) =>
		items.ForEach(list.Add);
}
