namespace TylerDM.StandardLibrary.Statistics;

public class MovingAverageInt
{
	private readonly FixedQueue<int> _queue;

	public int SampleSize => _queue.Capacity;
	public bool Empty => _queue.Empty;
	public bool Full => _queue.Full;

	public MovingAverageInt(int sampleSize, int initialValue = default)
	{
		ArgumentOutOfRangeException.ThrowIfLessThan(sampleSize, 2);

		_queue = new(sampleSize);

		if (initialValue == default) return;
		for (int i = 0; i < sampleSize; i++)
			Add(initialValue);
	}

	public void Add(int sample) =>
	_queue.Enqueue(sample);

	public int GetAverage() =>
		_queue.Sum() / _queue.Count;

	public bool TryGetAverage(out int avg)
	{
		if (Empty)
		{
			avg = default;
			return false;
		}

		avg = GetAverage();
		return true;
	}
}
