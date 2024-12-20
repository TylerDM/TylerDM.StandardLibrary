namespace TylerDM.StandardLibrary.System.Threading;

public class Gate()
{
	private readonly SemaphoreSlim _semaphore = new(0);

	/// <summary>
	/// Wait for either the gate to be released, or the timeout to expire.
	/// </summary>
	/// <returns>True if the gate was released. False if the timeout expired.</returns>
	public async Task<bool> TryWaitAsync(TimeSpan timeout)
	{
		using var cts = new CancellationTokenSource(timeout);
		return await TryWaitAsync(cts.Token);
	}
	
	/// <summary>
	/// Wait for the gate to be released, or the token is cancelled.
	/// </summary>
	/// <returns>True if the gate was released. False if the token was cancelled.</returns>
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
	
	/// <summary>
	/// Wait for the gate to be released.
	/// </summary>
	public Task WaitAsync(CancellationToken ct) =>
		_semaphore.WaitAsync(ct);

	/// <summary>
	/// Wait for the gate to be released.
	/// </summary>
	public async Task WaitAsync(TimeSpan timeout)
	{
		using var cts = new CancellationTokenSource(timeout);
		await _semaphore.WaitAsync(cts.Token);
	}

	/// <summary>
	/// Release threads waiting at the gate.
	/// </summary>
	public void Release() =>
		_semaphore.Release(int.MaxValue);
}
