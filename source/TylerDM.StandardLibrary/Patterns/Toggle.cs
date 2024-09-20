namespace TylerDM.StandardLibrary.Patterns;

public class Toggle(bool initialValue = false)
{
	public bool Value { get; set; } = initialValue;

	public void Invert() => Value = !Value;

	public void ActToggled(Action action)
	{
		Invert();
		action();
		Invert();
	}

	public async Task ActToggledAsync(Func<Task> func)
	{
		Invert();
		await func();
		Invert();
	}

	public static implicit operator bool(Toggle toggle) =>
		toggle.Value;
	public static implicit operator Toggle(bool b) =>
		new(b);
}
