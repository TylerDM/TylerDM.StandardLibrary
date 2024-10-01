namespace TylerDM.StandardLibrary.Patterns;

public class RunOnceAsync(Func<Task> _func)
{
	private bool ran = false;

	public async ValueTask RunAsync()
	{
		if (ran) return;
		ran = true;

		await _func();
	}
}
