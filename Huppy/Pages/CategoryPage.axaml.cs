using System.Linq;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

using Huppy.ViewModels;
using Huppy.Views.Dialogs;
using Shared.Models;
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

        var categories = categoryViewModel.CategoryToApps.Select(pair => pair.Key).ToList();

        ProposeAppDialog dialog = new(VisualRoot as Visual, categories);
        var result = await dialog.Show();
        if (result == null)
        {
            return;
        }

        var appEntity = new AppEntity() { Category = result.Category, Name = result.Name, Image = result.Image };
        var linkEntity = new LinkEntity() { Url = result.Url };

        var response = await categoryViewModel.AppCreate(appEntity, linkEntity);
        if (response == null)
        {
            return;
        }

        categoryViewModel.AppAdd(response.Value.app, response.Value.link);
    }
}
}
