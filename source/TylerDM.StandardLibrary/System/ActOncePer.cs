#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning disable CS8603 // Possible null reference return.

namespace TylerDM.StandardLibrary.System;

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

public abstract class ActOncePerBase<TItem>
{
	private readonly HashSet<TItem> _hashSet = [];

	protected readonly SemaphoreSlim _semaphore = new(1, 1);

	protected void markHasActed(TItem item) =>
		_hashSet.Add(item);

	protected bool getHasActed(TItem item) =>
		_hashSet.Contains(item);
}
