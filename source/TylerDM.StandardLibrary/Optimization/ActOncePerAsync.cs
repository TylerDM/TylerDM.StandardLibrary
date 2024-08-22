#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning disable CS8603 // Possible null reference return.

namespace TylerDM.StandardLibrary.Optimization;

public class ActOncePerAsync<TItem>(Func<TItem, Task> func) : ActOncePerBase<TItem>
{
	public Task<bool> GetHasActedAsync(TItem item) =>
		_semaphore.WaitThenAsync(() => getHasActed(item));

	public Task ActAsync(TItem item) =>
		_semaphore.WaitThen(() =>
		{
			if (getHasActed(item)) return Task.CompletedTask;

			markHasActed(item);
			return func(item);
		});
}

public class ActOncePerAsync<TItem, TResult>(Func<TItem, Task<TResult>> func) : ActOncePerBase<TItem>
{
	public Task<bool> GetHasActedAsync(TItem item) =>
		_semaphore.WaitThenAsync(() => getHasActed(item));

	public Task<TResult> ActAsync(TItem item) =>
		_semaphore.WaitThenAsync(async () =>
		{
			if (getHasActed(item)) return default;

			markHasActed(item);
			return await func(item);
		});
}
