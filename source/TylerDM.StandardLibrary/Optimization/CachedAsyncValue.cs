#pragma warning disable CS8601 // Possible null reference assignment.

namespace TylerDM.StandardLibrary.Optimization;

public class CachedAsyncValue<T>(Func<Task<T>> func)
{
	private readonly SemaphoreSlim _semaphore = new(1, 1);

	private bool hasValue = false;
	private T value = default;

	public void Clear() =>
		hasValue = false;

	public async Task<T> GetValue()
	{
		await _semaphore.WaitAsync();

		try
		{
			if (hasValue) return value;

			value = await func();
			hasValue = true;
			return value;
		}
		finally
		{
			_semaphore.Release();
		}
	}
}
