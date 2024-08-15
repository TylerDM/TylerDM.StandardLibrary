using TylerDM.StandardLibrary.Testing;

namespace TylerDM.StandardLibrary.System;

public class ServiceProviderExtTests
{
	class TestingService
	{
		public void Action(Action action) => action();
		public Task ActionAsync(Func<Task> func) => func();
		public bool GetTrue() => true;
		public Task<bool> GetTrueAsync() => Task.FromResult(true);
	}

	readonly TestServiceProvider _serviceProvider =
		new(x => x.AddSingleton<TestingService>());

	[Fact]
	public async Task InvokeAsync()
	{
		var worked = false;
		await _serviceProvider.InvokeAsync<TestingService>(
			x => x.ActionAsync(
				() => Task.FromResult(worked = true)
			)
		);
		Assert.True(worked);
	}

	[Fact]
	public void Invoke()
	{
		var worked = false;
		_serviceProvider.Invoke<TestingService>(
			x => x.Action(
				() => worked = true
			)
		);
		Assert.True(worked);
	}

	[Fact]
	public async Task InvokeWithResultAsync()
	{
		var result = await _serviceProvider.InvokeAsync<TestingService, bool>(x => x.GetTrueAsync());
		Assert.True(result);
	}

	[Fact]
	public void InvokeWithResult()
	{
		var result = _serviceProvider.Invoke<TestingService, bool>(x => x.GetTrue());
		Assert.True(result);
	}
}
