using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Controls;

using Huppy.ViewModels;

namespace Huppy.Views
{
public partial class AppView : UserControl
{
    public AppView()
    {
        InitializeComponent();
    }

    private void OnPointerReleasedApp(object? aSender, PointerReleasedEventArgs aArgs)
    {
        if (aArgs.InitialPressMouseButton != MouseButton.Left || aSender is not StackPanel stackPanel)
        {
            return;
        }

        var colorChecked = Brush.Parse("#FF9966");
        var colorUnchecked = Brush.Parse("#00A550");
        var isChecked = stackPanel.Background?.ToString() == colorChecked.ToString();

        stackPanel.Background = isChecked ? colorUnchecked : colorChecked;
    }

    private void OnLoadedItemsRepeaterApps(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is not ItemsRepeater itemsRepeater)
        {
            return;
        }

        itemsRepeater.ItemsSource = (DataContext as AppViewModel)?.Apps;
    }
}
}
