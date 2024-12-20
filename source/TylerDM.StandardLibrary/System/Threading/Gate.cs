namespace TylerDM.StandardLibrary.System.Threading;

public class Gate()
{
	private readonly SemaphoreSlim _semaphore = new(0);

	public async Task<bool> TryWaitAsync(TimeSpan timeout)
	{
		using var cts = new CancellationTokenSource(timeout);
		return await TryWaitAsync(cts.Token);
	}
	
	public async Task<bool> TryWaitAsync(CancellationToken ct)
	{
		try
		{
			await _semaphore.WaitAsync(ct);
			return true;
		}
		catch (OperationCanceledException)
		{
			return false;
		}
	}
	
	public Task WaitAsync(CancellationToken ct) =>
		_semaphore.WaitAsync(ct);

	public async Task WaitAsync(TimeSpan timeout)
	{
		using var cts = new CancellationTokenSource(timeout);
		await _semaphore.WaitAsync(cts.Token);
	}

	public void Release() =>
		_semaphore.Release(int.MaxValue);
}
