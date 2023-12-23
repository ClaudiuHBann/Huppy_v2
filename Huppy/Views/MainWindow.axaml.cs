using Avalonia.Controls;
using Avalonia.Interactivity;

using Huppy.Utilities;

namespace Huppy.Views
{
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnLoadedWindow(object? sender, RoutedEventArgs e)
    {
        Notifications.Initialize(this);
    }
}
}
