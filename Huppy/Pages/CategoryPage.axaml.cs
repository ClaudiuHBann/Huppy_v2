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

    private void OnCheckedCheckBoxShowProposedApps(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not CategoryViewModel categoryViewModel)
        {
            return;
        }

        categoryViewModel.ShowProposedApps(checkBoxShowProposedApps.IsChecked ?? false);
    }

    private void OnClickButtonProposeAnApp(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not CategoryViewModel categoryViewModel)
        {
            return;
        }
    }
}
}
