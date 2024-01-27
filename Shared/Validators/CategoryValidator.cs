using Shared.Models;

using FluentValidation;

namespace Shared.Validators
{
public class CategoryValidator : AbstractValidator<CategoryEntity>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name).Length(0, 64);
        RuleFor(x => x.Description).Length(0, 256);
    }
}
}
