namespace TylerDM.StandardLibrary.Optimization;

public abstract class ActOncePerBase<TItem>
{
	protected readonly SemaphoreSlim _semaphore = new(1, 1);

	private readonly HashSet<TItem> _hashSet = [];

	protected void markHasActed(TItem item) =>
		_hashSet.Add(item);

	protected bool getHasActed(TItem item) =>
		_hashSet.Contains(item);
}
