namespace TylerDM.StandardLibrary.Testing;

public class TestServiceProvider(Action<IServiceCollection> configure) : IServiceProvider
{
	private readonly ServiceProvider _serviceProvider = createProvider(configure);

	public object? GetService(Type serviceType) =>
		_serviceProvider.GetService(serviceType);

	private static ServiceProvider createProvider(Action<IServiceCollection> configure)
	{
		var collection = new ServiceCollection();
		configure(collection);
		return collection.BuildServiceProvider();
	}
}
