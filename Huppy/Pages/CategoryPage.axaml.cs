using System.Linq;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

using Huppy.ViewModels;
using Huppy.Views.Dialogs;

using Shared.Requests;

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

        var categories = categoryViewModel.CategoryToApps.Select(pair => pair.Key.Category).ToList();

        ProposeAppDialog dialog = new(VisualRoot as Visual, categories);
        var result = await dialog.Show();
        if (result == null)
        {
            return;
        }

        var appRequest =
            new AppRequest() { Category = result.Category, Name = result.Name, ImageRaw = result.ImageRaw };

        var appEntity = await categoryViewModel.AppCreate(appRequest);
        if (appEntity == null)
        {
            return;
        }

        categoryViewModel.CategoryToApps.First(pair => pair.Key.Category.Id == appEntity.Category)
            .Value.Apps.Add(new(appEntity));
    }
}
}
