namespace TylerDM.StandardLibrary.System.Threading.Tasks;

public static class TaskExt
{
	public static readonly Func<Task> Empty = () => Task.CompletedTask;

	public static async Task<bool> TryWaitAsync(this Task task, TimeSpan timeout)
	{
		using var cts = new CancellationTokenSource(timeout);
		return await task.TryWaitAsync(cts.Token);
	}
	
	public static async Task<bool> TryWaitAsync(this Task task, CancellationToken cancellationToken = default)
	{
		try
		{
			await task.WaitAsync(cancellationToken);
			return task.IsCompletedSuccessfully;
		}
		catch
		{
			return false;
		}
	}
}
