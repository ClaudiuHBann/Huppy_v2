using Avalonia;
using Avalonia.Controls;

using System.Threading.Tasks;

using FluentAvalonia.UI.Controls;
using Avalonia.Layout;

namespace Huppy.Views.Dialogs
{
public class AboutDialog : Dialog
{
    private const string _header = "About Huppy";

    public AboutDialog(Visual? root) : base(root, _header)
    {
        Buttons.Add(TaskDialogButton.OKButton);
    }

    protected override Control CreateContent()
    {
        var stackPanelDeveloper = new StackPanel() { Spacing = 5, Orientation = Orientation.Horizontal };
        stackPanelDeveloper.Children.Add(new TextBlock() { Text = "Developer:" });
        stackPanelDeveloper.Children.Add(new TextBlock() { Text = "Hermann Claudiu-Andrei" });

        var stackPanelDeveloperPseudonym = new StackPanel() { Spacing = 5, Orientation = Orientation.Horizontal };
        stackPanelDeveloperPseudonym.Children.Add(new TextBlock() { Text = "Developer Pseudonym:" });
        stackPanelDeveloperPseudonym.Children.Add(new TextBlock() { Text = "Claudiu HBann" });

        var stackPanel = new StackPanel() { Spacing = 5 };

        stackPanel.Children.Add(stackPanelDeveloper);
        stackPanel.Children.Add(stackPanelDeveloperPseudonym);

        return stackPanel;
    }

    public new async Task Show() => await base.Show();
}
}
