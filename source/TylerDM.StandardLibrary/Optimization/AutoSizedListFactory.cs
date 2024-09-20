namespace TylerDM.StandardLibrary.Optimization;

public class AutoSizedListFactory<T>(int sampleSize = 10, int initialValue = default, float multiplier = 1.25f)
{
	private readonly MovingAverageInt _movingAverage = new(sampleSize, initialValue);

	public int SampleSize => _movingAverage.SampleSize;
	/// <summary>
	/// The multiplier by which the average is multiplied by to get the final list capacity.
	/// </summary>
	public float Multiplier { get; } = multiplier;

	public AutoSizedList<T> Create() =>
		new(this, getListSize());

	internal void ReportFinalSize(int finalSize) =>
		_movingAverage.Add(finalSize);

	private int getListSize() =>
		Convert.ToInt32(_movingAverage.GetAverage() * Multiplier);
}
