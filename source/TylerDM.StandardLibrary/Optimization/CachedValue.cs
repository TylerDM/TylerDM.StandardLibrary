#pragma warning disable CS8601 // Possible null reference assignment.

namespace TylerDM.StandardLibrary.Optimization;

public class CachedValue<T>(Func<T> func)
{
	private readonly SemaphoreSlim _semaphore = new(1, 1);

	private bool hasValue = false;
	private T value = default;

	public void Clear() =>
		hasValue = false;

	public T GetValue() =>
		_semaphore.WaitThen(() =>
		{
			if (hasValue) return value;

			value = func();
			hasValue = true;
			return value;
		});
}
