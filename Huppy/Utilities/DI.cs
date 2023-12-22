using Huppy.Models;

using Microsoft.Extensions.DependencyInjection;

namespace Huppy.Utilities
{
public static class DI
{
    private static readonly ServiceProvider _services =
        new ServiceCollection()
            .AddDbContext<HuppyContext>(options => options.EnableSensitiveDataLogging())
            .BuildServiceProvider();

    public static Type Create<Type>(params object[] parameters)
    {
        return ActivatorUtilities.CreateInstance<Type>(_services, parameters);
    }
}
}
