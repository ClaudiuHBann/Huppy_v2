using Microsoft.Extensions.DependencyInjection;

namespace Huppy.Utilities
{
public static class DI
{
    private static readonly ServiceProvider _services =
        new ServiceCollection().AddSingleton<Database>().BuildServiceProvider();

    public static Type Create<Type>(params object[] parameters)
    {
        return ActivatorUtilities.CreateInstance<Type>(_services, parameters);
    }
}
}
