namespace TylerDM.StandardLibrary.System;

public static class ServiceCollectionExt
{
    public static void AddInteractableHostedService<T>(this IServiceCollection services)
        where T : class, IHostedService
    {
        services.AddSingleton<T>();
        services.AddHostedService(x => x.GetRequiredService<T>());
    }
    
    public static void AddInteractableHostedService<T>(this IServiceCollection services, Func<IServiceProvider, T> factory)
        where T : class, IHostedService
    {
        services.AddSingleton(factory);
        services.AddHostedService(x => x.GetRequiredService<T>());
    }
}