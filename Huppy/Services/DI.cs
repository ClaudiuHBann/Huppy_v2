using System;
using System.Linq;

using Huppy.Services.Database;

using Microsoft.Extensions.DependencyInjection;

namespace Huppy.Services
{
public static class DI
{
    public static ServiceProvider ServiceProvider { get; set; }

    private static ServiceCollection Services { get; set; } = new();

    static DI()
    {
        Services.AddSingleton<DatabaseService>()
            .AddSingleton<ClipboardService>()
            .AddSingleton<NotificationService>()
            .AddSingleton<StorageService>()
            .AddSingleton<SettingsService>();

        ServiceProvider = Services.BuildServiceProvider();
    }

    public static Type Create<Type>(params object[] parameters) =>
        ActivatorUtilities.CreateInstance<Type>(ServiceProvider, parameters);

    public static void Initialize() => Apply(service => service.Initialize());
    public static void Uninitialize() => Apply(service => service.Uninitialize());

    private static void Apply(Action<BaseService> action)
    {
        Services
            .Where(serviceDescriptor => serviceDescriptor.ImplementationType != null &&
                                        serviceDescriptor.ImplementationType.IsSubclassOf(typeof(BaseService)))
            .Select(serviceDescriptor => ServiceProvider.GetService(serviceDescriptor.ImplementationType!))
            .Where(obj => obj is not null)
            .Select(obj => (BaseService)obj!)
            .ToList()
            .ForEach(action);
    }
}
}
