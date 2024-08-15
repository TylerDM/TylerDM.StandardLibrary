#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning disable CS8603 // Possible null reference return.

namespace TylerDM.StandardLibrary.Optimization;

public class ActOncePer<TItem>(Action<TItem> action) : ActOncePerBase<TItem>
{
	public bool GetHasActed(TItem item) =>
		_semaphore.WaitThen(() => getHasActed(item));

	public void Act(TItem item) =>
		_semaphore.WaitThen(() =>
		{
			if (getHasActed(item)) return;

			markHasActed(item);
			action(item);
		});
}

public class ActOncePer<TItem, TResult>(Func<TItem, TResult> func) : ActOncePerBase<TItem>
{
	public bool GetHasActed(TItem item) =>
		_semaphore.WaitThen(() => getHasActed(item));

	public TResult Act(TItem item) =>
		_semaphore.WaitThen(() =>
		{
			if (getHasActed(item)) return default;

			markHasActed(item);
			return func(item);
		});
}
