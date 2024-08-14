namespace TylerDM.StandardLibrary.System.Threading;

public static class SemaphoreSlimExt
{
	public static void WaitThen(this SemaphoreSlim semaphore, Action action)
	{
		try
		{
			semaphore.Wait();
			action();
		}
		finally
		{
			semaphore.Release();
		}
	}

	public static T WaitThen<T>(this SemaphoreSlim semaphore, Func<T> action)
	{
		try
		{
			semaphore.Wait();
			return action();
		}
		finally
		{
			semaphore.Release();
		}
	}

	public static async Task WaitThenAsync(this SemaphoreSlim semaphore, Func<Task> funcTask)
	{
		try
		{
			await semaphore.WaitAsync();
			await funcTask();
		}
		finally
		{
			semaphore.Release();
		}
	}

	public static async Task<T> WaitThenAsync<T>(this SemaphoreSlim semaphore, Func<Task<T>> funcTask)
	{
		try
		{
			await semaphore.WaitAsync();
			return await funcTask();
		}
		finally
		{
			semaphore.Release();
		}
	}

	public static async Task WaitThenAsync(this SemaphoreSlim semaphore, Action action)
	{
		try
		{
			await semaphore.WaitAsync();
			action();
		}
		finally
		{
			semaphore.Release();
		}
	}

	public static async Task<T> WaitThenAsync<T>(this SemaphoreSlim semaphore, Func<T> func)
	{
		try
		{
			await semaphore.WaitAsync();
			return func();
		}
		finally
		{
			semaphore.Release();
		}
	}
}
