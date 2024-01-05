using System.Linq;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

using Huppy.ViewModels;
using Huppy.Views.Dialogs;

namespace Huppy.Pages
{
public partial class CategoryView : UserControl
{
    public CategoryView()
    {
        InitializeComponent();
    }

    private void OnCheckedCheckBoxShowProposedApps(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not CategoryViewModel categoryViewModel)
        {
            return;
        }

        categoryViewModel.ShowProposedApps(checkBoxShowProposedApps.IsChecked ?? false);
    }

    private async void OnClickButtonProposeAnApp(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not CategoryViewModel categoryViewModel)
        {
            return;
        }

        var categoryModels = categoryViewModel.CategoryToApps.Select(pair => pair.Key).ToList();

        ProposeAppDialog dialog = new(VisualRoot as Visual, categoryModels);
        var result = await dialog.Show();
        if (result == null)
        {
            return;
        }
    }
}
}
