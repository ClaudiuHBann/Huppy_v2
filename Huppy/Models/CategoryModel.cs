using System;

using CommunityToolkit.Mvvm.ComponentModel;

using Shared.Models;

namespace Huppy.Models
{
public partial class CategoryModel
(CategoryEntity category) : ObservableObject
{
    public static readonly Guid CategoryOtherIndex = Guid.Parse("48d3177b-d18d-4828-9760-1c8c04c43d7a");

    public CategoryEntity Category { get; set; } = category;

    [ObservableProperty]
    public bool isVisible = true;
}
}
