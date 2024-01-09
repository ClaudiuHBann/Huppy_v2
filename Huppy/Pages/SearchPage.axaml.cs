using Avalonia.Input;
using Avalonia.Controls;

using Huppy.ViewModels;
using System;

namespace Huppy.Pages
{
public partial class SearchView : UserControl
{
    public SearchView()
    {
        InitializeComponent();
    }

    private void OnTextChangedQuery(object? sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox textBox || textBox.Text is null || DataContext is not SearchViewModel searchViewModel)
        {
            return;
        }

        searchViewModel.Search.Query = textBox.Text;
        searchViewModel.Filter();
    }

    private void OnSelectionChangedCategory(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox comboBox || comboBox.SelectedItem is null ||
            comboBox.SelectedItem is not Models.CategoryModel categoryView ||
            DataContext is not SearchViewModel searchViewModel)
        {
            return;
        }

        searchViewModel.Search.Category = new(categoryView.Category);
        searchViewModel.Filter();
    }

    private void OnDropDownOpenedComboBoxCategory(object? sender, EventArgs e)
    {
        if (DataContext is not SearchViewModel searchViewModel)
        {
            return;
        }

        searchViewModel.Populate();
    }
}
}
