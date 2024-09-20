namespace TylerDM.StandardLibrary.Patterns;

public class ToggleTests
{
	[Fact]
	public void Invert()
	{
		var toggle = new Toggle();
		Assert.False(toggle);
		toggle.Invert();
		Assert.True(toggle);
	}

	[Fact]
	public void ActToggled()
	{
		var acted = false;
		var toggle = new Toggle();
		Assert.False(toggle);
		toggle.ActToggled(() =>
		{
			acted = true;
			Assert.True(toggle);
		});
		Assert.True(acted);
		Assert.False(toggle);
	}

	[Fact]
	public async Task ActToggledAsync()
	{
		var acted = false;
		var toggle = new Toggle();
		Assert.False(toggle);
		await toggle.ActToggledAsync(() =>
		{
			acted = true;
			Assert.True(toggle);
			return Task.CompletedTask;
		});
		Assert.True(acted);
		Assert.False(toggle);
	}

	[Fact]
	public void Constructor()
	{
		var toggle = new Toggle(true);
		Assert.True(toggle);
		toggle.Invert();
		Assert.False(toggle);
	}

	[Fact]
	public void ToggledScope()
	{
		var toggle = new Toggle();
		Assert.False(toggle);

		using (var toggledScope = new ToggledScope(toggle))
			Assert.True(toggle);

		Assert.False(toggle);
	}

	[Fact]
	public void ToggledScopeSet()
	{
		var toggle = new Toggle();
		Assert.False(toggle);

		using (var toggledScope = new ToggledScope(toggle, false))
			Assert.False(toggle);

		Assert.True(toggle);
	}
}
