using System;

using Avalonia.Controls;

using FluentAvalonia.UI.Controls;

namespace Huppy.Views
{
public partial class HuppyView : UserControl
{
    public HuppyView()
    {
        InitializeComponent();

        var navigationView = this.FindControl<NavigationView>("navigationView");
        if (navigationView == null)
        {
            return;
        }

        navigationView.SelectionChanged += OnNavigationViewSelectionChanged;
        navigationView.SelectedItem = navigationView.MenuItems[0];
    }

    private void OnNavigationViewSelectionChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
    {
        if (sender is not NavigationView navigationView || e.SelectedItem is not NavigationViewItem item)
        {
            return;
        }

        if (item.Tag?.ToString() == "About")
        {
            navigationView.SelectedItem = navigationView.MenuItems[0];
            return;
        }
        else
        {
            navigationView.Content = CreateView(item);
        }
    }

    private static UserControl? CreateView(NavigationViewItem item)
    {
        // create the view
        var viewTypeName = $"Huppy.Views.{item.Tag}";
        var viewType = Type.GetType(viewTypeName);
        if (viewType == null)
        {
            return null;
        }

        var view = Activator.CreateInstance(viewType);
        if (view is not UserControl userControl)
        {
            return null;
        }

        // create the view model
        var viewModelTypeName = $"Huppy.ViewModels.{item.Tag}Model";
        var viewModelType = Type.GetType(viewModelTypeName);
        if (viewModelType == null)
        {
            return null;
        }
        var viewModel = Activator.CreateInstance(viewModelType);

        userControl.DataContext = viewModel;

        return userControl;
    }
}
}
