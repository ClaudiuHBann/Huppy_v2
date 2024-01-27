using Shared.Models;

using FluentValidation;

namespace Shared.Validators
{
public class PackageValidator : AbstractValidator<PackageEntity>
{
    public PackageValidator()
    {
        RuleFor(x => x.Name).Length(0, 36);
    }
}
}
