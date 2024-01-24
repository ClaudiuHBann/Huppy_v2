using Shared.Models;

using FluentValidation;

using SixLabors.ImageSharp;

namespace Shared.Validators
{
public class AppValidator : AbstractValidator<AppEntity>
{
    public AppValidator()
    {
        RuleFor(x => x.Category).NotEmpty();
        RuleFor(x => x.Name).Length(0, 128);
        RuleFor(x => x.Proposed).Equal(true);
        RuleFor(x => x.Image).MustAsync(ValidateImage);
    }

    private async Task<bool> ValidateImage(byte[] image, CancellationToken token)
    {
        try
        {
            await Image.LoadAsync(new MemoryStream(image), token);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
}
