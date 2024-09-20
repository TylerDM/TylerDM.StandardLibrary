namespace TylerDM.StandardLibrary.Collections;

/// <summary>
/// A queue which automatically dequeues if an enqueue operation would exceed its capacity.
/// </summary>
public class FixedQueue<T> : ICollection, IReadOnlyCollection<T>
{
	private readonly Queue<T> _queue;

	public int Capacity { get; }
	public int Count => _queue.Count;
	public bool Empty => Count == 0;
	public bool Full => Count == Capacity;

	bool ICollection.IsSynchronized => ((ICollection)_queue).IsSynchronized;
	object ICollection.SyncRoot => ((ICollection)_queue).SyncRoot;

	public FixedQueue(int capacity)
	{
		ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1);

		_queue = new Queue<T>(capacity);
		Capacity = capacity;
	}

	public void Enqueue(T item)
	{
		if (Full) _queue.Dequeue();

		_queue.Enqueue(item);
	}

	public void Clear() =>
		_queue.Clear();

	public T Peek() =>
		_queue.Peek();

	public bool TryPeek([MaybeNullWhen(false)] out T result) =>
		_queue.TryPeek(out result);

	public T Dequeue() =>
		_queue.Dequeue();

	public bool TryDequeue([MaybeNullWhen(false)] out T item) =>
		_queue.TryDequeue(out item);

	public bool Contains(T item) =>
		_queue.Contains(item);

	public IEnumerator<T> GetEnumerator() =>
		_queue.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() =>
		GetEnumerator();

	public void CopyTo(Array array, int index) =>
		((ICollection)_queue).CopyTo(array, index);
}
