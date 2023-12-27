using Huppy.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Huppy.Utilities
{
public static class DI
{
    public static ServiceProvider Services { get; set; } = new ServiceCollection()
                                                               .AddSingleton<DatabaseService>()
                                                               .AddSingleton<ClipboardService>()
                                                               .AddSingleton<NotificationService>()
                                                               .BuildServiceProvider();

    public static Type Create<Type>(params object[] parameters)
    {
        return ActivatorUtilities.CreateInstance<Type>(Services, parameters);
    }
}
}
