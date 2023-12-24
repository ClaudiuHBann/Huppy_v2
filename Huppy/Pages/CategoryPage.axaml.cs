using Avalonia.Controls;
using Avalonia.Interactivity;

using Huppy.ViewModels;

namespace Huppy.Pages
{
public partial class CategoryView : UserControl
{
    public CategoryView()
    {
        InitializeComponent();
    }

    private async void OnLoadedCategoryToApps(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not CategoryViewModel categoryViewModel)
        {
            return;
        }

        await categoryViewModel.Populate();
    }
}
}
