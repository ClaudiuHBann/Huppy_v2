using Shared.Models;

using FluentValidation;

namespace Shared.Validators
{
public class LinkValidator : AbstractValidator<LinkEntity>
{
    public LinkValidator()
    {
        RuleFor(x => x.App).NotEmpty();
        RuleFor(x => x.Url).Length(0, 1024).MustAsync(ValidateURL);
    }

    private async Task<bool> ValidateURL(string url, CancellationToken token)
    {
        try
        {
            var uri = new Uri(url);
            var fileInfo = new FileInfo(uri.AbsolutePath);
            if (string.IsNullOrWhiteSpace(fileInfo.Extension))
            {
                return false;
            }

            using var client = new HttpClient();
            var response = await client.SendAsync(new(HttpMethod.Head, url), token);
            if (!response.IsSuccessStatusCode || response.Content.Headers.ContentType == null)
            {
                return false;
            }

            var contentType = response.Content.Headers.ContentType.MediaType ?? "";
            return contentType.StartsWith("application/");
        }
        catch
        {
            return false;
        }
    }
}
}
