namespace TylerDM.StandardLibrary.System.Threading.Tasks;

public static class TaskExt
{
	public static readonly Func<Task> Empty = () => Task.CompletedTask;
}
