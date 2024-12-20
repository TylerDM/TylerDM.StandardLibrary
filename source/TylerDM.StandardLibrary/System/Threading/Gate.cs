namespace TylerDM.StandardLibrary.System.Threading;

public class Gate()
{
	private readonly SemaphoreSlim _semaphore = new(0);

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

	public void Release() =>
		_semaphore.Release(int.MaxValue);
}
