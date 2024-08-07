namespace TylerDM.StandardLibrary.System.Collections.Generic;

public class DistinctList<T>(
	int initialSize = 50
) : IEnumerable<T>, IReadOnlyCollection<T>
{
	private readonly List<T> _list = new(initialSize);

	public int Count => _list.Count;

	public void AddRange(IEnumerable<T> values)
	{
		foreach (T value in values)
			Add(value);
	}

	public void Add(T value)
	{
		if (_list.Contains(value)) return;

		_list.Add(value);
	}

	public IEnumerator<T> GetEnumerator() =>
		_list.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() =>
		_list.GetEnumerator();
}
