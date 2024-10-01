namespace TylerDM.StandardLibrary.Patterns;

public class RunOnce(Action _action)
{
	private bool ran = false;

	public void Run()
	{
		if (ran) return;
		ran = true;

		_action();
	}
}
