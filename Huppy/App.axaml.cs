using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.Data.Core.Plugins;
using Avalonia.Controls.ApplicationLifetimes;

using Microsoft.Extensions.DependencyInjection;

using Huppy.Views;
using Huppy.Models;
using Huppy.ViewModels;

namespace Huppy;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        var services = new ServiceCollection().AddDbContext<HuppyContext>().BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow =
                new MainWindow { DataContext = ActivatorUtilities.CreateInstance<CategoryViewModel>(services) };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView =
                new MainView { DataContext = ActivatorUtilities.CreateInstance<CategoryViewModel>(services) };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
