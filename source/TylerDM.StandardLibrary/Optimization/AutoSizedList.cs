namespace TylerDM.StandardLibrary.Optimization;

public class AutoSizedList<T>(AutoSizedListFactory<T> factory, int initialCapacity) : List<T>(initialCapacity), IDisposable
{
	private bool disposed = false;

	public void Dispose()
	{
		if (disposed) return;
		disposed = true;

		factory.ReportFinalSize(Count);
	}
}
