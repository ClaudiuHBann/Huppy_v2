using System;
using System.Collections.Generic;

using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;

using FluentAvalonia.UI.Controls;

using Huppy.Services;
using Huppy.Views.Dialogs;

namespace Huppy.Views
{
public partial class HuppyView : UserControl
{
    private Dictionary<string, UserControl> _views = [];
    private int _NVSelectedIndexLast = 0;

    public HuppyView()
    {
        InitializeComponent();

        var navigationView = this.FindControl<NavigationView>("navigationView");
        if (navigationView == null)
        {
            return;
        }

        navigationView.SelectionChanged += OnNavigationViewSelectionChanged;
        navigationView.SelectedItem = navigationView.MenuItems[_NVSelectedIndexLast];
    }

    private async void OnNavigationViewSelectionChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
    {
        if (sender is not NavigationView navigationView || e.SelectedItem is not NavigationViewItem item)
        {
            return;
        }

        if (item.Tag?.ToString() == "About")
        {
            // TODO: can we remove this workaround?
            // select back the last item outside this method so the property changed will propagate
            // https://github.com/amwx/FluentAvalonia/discussions/501
            await Dispatcher.UIThread.InvokeAsync(() => navigationView.SelectedItem =
                                                      navigationView.MenuItems[_NVSelectedIndexLast]);

            await new AboutDialog(this).Show();
        }
        else
        {
            navigationView.Content = CreateView(item);
        }

        _NVSelectedIndexLast = navigationView.MenuItems.IndexOf(e.SelectedItem);
    }

    private UserControl? CreateView(NavigationViewItem item)
    {
        // search the view cache first
        var viewTypeName = $"Huppy.Views.{item.Tag}";
        if (_views.TryGetValue(viewTypeName, out UserControl? value))
        {
            return value;
        }

        // create the view
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

        // add the view to our cache
        _views.Add(viewTypeName, userControl);

        return userControl;
    }

    private void OnHuppyLoaded(object? sender, RoutedEventArgs e) => DI.Initialize();
    private void OnHuppyUnloaded(object? sender, RoutedEventArgs e) => DI.Uninitialize();
}
}
