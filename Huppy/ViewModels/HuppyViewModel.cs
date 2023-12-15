using CommunityToolkit.Mvvm.ComponentModel;

using Huppy.Models;

namespace Huppy.ViewModels
{
public class HuppyViewModel
(HuppyContext context) : ObservableObject
{
    public CategoryViewModel CategoryViewModel { get; set; } = new(context);
}
}
