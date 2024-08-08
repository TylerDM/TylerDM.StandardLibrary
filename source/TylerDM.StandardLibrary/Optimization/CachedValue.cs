#pragma warning disable CS8601 // Possible null reference assignment.

namespace TylerDM.StandardLibrary.Optimization;

public class CachedValue<T>(Func<T> func)
{
	private readonly object _lock = new();

	private bool hasValue = false;
	private T value = default;

	public void Clear() =>
		hasValue = false;

	public T GetValue()
	{
		lock (_lock)
		{
			if (hasValue) return value;

			value = func();
			hasValue = true;
			return value;
		}
	}
}
