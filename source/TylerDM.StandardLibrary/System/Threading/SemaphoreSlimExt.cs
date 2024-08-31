namespace TylerDM.StandardLibrary.System.Threading;

public static class SemaphoreSlimExt
{
	public static void WaitThen(this SemaphoreSlim semaphore, Action action)
	{
		semaphore.Wait();
		try
		{
			action();
		}
		finally
		{
			semaphore.Release();
		}
	}

	public static T WaitThen<T>(this SemaphoreSlim semaphore, Func<T> action)
	{
		semaphore.Wait();
		try
		{
			return action();
		}
		finally
		{
			semaphore.Release();
		}
	}

	public static async Task WaitThenAsync(this SemaphoreSlim semaphore, Func<Task> funcTask)
	{
		await semaphore.WaitAsync();
		try
		{
			await funcTask();
		}
		finally
		{
			semaphore.Release();
		}
	}

	public static async Task<T> WaitThenAsync<T>(this SemaphoreSlim semaphore, Func<Task<T>> funcTask)
	{
		await semaphore.WaitAsync();
		try
		{
			return await funcTask();
		}
		finally
		{
			semaphore.Release();
		}
	}

	public static async Task WaitThenAsync(this SemaphoreSlim semaphore, Action action)
	{
		await semaphore.WaitAsync();
		try
		{
			action();
		}
		finally
		{
			semaphore.Release();
		}
	}

	public static async Task<T> WaitThenAsync<T>(this SemaphoreSlim semaphore, Func<T> func)
	{
		await semaphore.WaitAsync();
		try
		{
			return func();
		}
		finally
		{
			semaphore.Release();
		}
	}
}
