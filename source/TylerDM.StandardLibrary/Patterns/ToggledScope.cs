namespace TylerDM.StandardLibrary.Patterns;

public class ToggledScope : IDisposable
{
	private readonly Toggle _toggle;
	private readonly bool? _setValue;

	private bool disposed = false;

	public ToggledScope(Toggle toggle, bool? setValue = null)
	{
		_toggle = toggle;
		_setValue = setValue;
		if (setValue is bool v)
			_toggle.Value = v;
		else
			_toggle.Invert();
	}

	public void Dispose()
	{
		ObjectDisposedException.ThrowIf(disposed, this);
		disposed = true;

		if (_setValue is bool v)
			_toggle.Value = !v;
		else
			_toggle.Invert();
	}
}
