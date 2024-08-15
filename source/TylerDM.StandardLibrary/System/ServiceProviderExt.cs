namespace TylerDM.StandardLibrary.System;

public static class ServiceProviderExt
{
	public static async Task InvokeAsync<TService>(this IServiceProvider serviceProvider, Func<TService, Task> func)
			where TService : class
	{
		using var scope = serviceProvider.CreateScope();
		var service = scope.ServiceProvider.GetRequiredService<TService>();
		await func(service);
	}

	public static void Invoke<TService>(this IServiceProvider serviceProvider, Action<TService> action)
		where TService : class
	{
		using var scope = serviceProvider.CreateScope();
		var service = scope.ServiceProvider.GetRequiredService<TService>();
		action(service);
	}

	public static async Task<TResult> InvokeAsync<TService, TResult>(this IServiceProvider serviceProvider, Func<TService, Task<TResult>> func)
		where TService : class
	{
		using var scope = serviceProvider.CreateScope();
		var service = scope.ServiceProvider.GetRequiredService<TService>();
		return await func(service);
	}

	public static TResult Invoke<TService, TResult>(this IServiceProvider serviceProvider, Func<TService, TResult> func)
		where TService : class
	{
		using var scope = serviceProvider.CreateScope();
		var service = scope.ServiceProvider.GetRequiredService<TService>();
		return func(service);
	}
}
