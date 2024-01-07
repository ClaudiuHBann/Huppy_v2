using Huppy.API.Models;
using Huppy.API.Controllers;

using Shared.Models;
using Shared.Requests;

using Microsoft.EntityFrameworkCore;

using VirusTotalNet;
using VirusTotalNet.ResponseCodes;

namespace Huppy.API.Services
{
public class LinkService
(ILogger<LinkController> logger, HuppyContext context) : BaseService<LinkController>(logger, context)
{
    private const string _virusTotalAPIKey = "9b4a57c29539ad065b0ef6577ce9113ba89674b69e12e79b0336df087731fda4";

    public async Task < LinkEntity ? > Create(LinkRequest request)
    {
        ClearLastError();

        if (!await Validate(request))
        {
            return null;
        }

        var entity = await Create(new LinkEntity(request));
        if (entity == null)
        {
            SetLastError("The link could not be created.");
        }

        return entity;
    }

    public async Task < LinkEntity ? > Update(LinkRequest request)
    {
        ClearLastError();

        if (!await Validate(request))
        {
            return null;
        }

        var entity = await Update(new LinkEntity(request));
        if (entity == null)
        {
            SetLastError("The link could not be updated.");
        }

        return entity;
    }

    public async Task < LinkEntity ? > Load(LinkRequest request)
    {
        ClearLastError();

        var entity = await FindBy<LinkEntity>(request.Id);
        if (entity == null)
        {
            SetLastError("The link could not be loaded.");
        }

        return entity;
    }

    private async Task<bool> Validate(LinkRequest request)
    {
        ClearLastError();

        if (!await context.Apps.AnyAsync(app => app.Id == request.App))
        {
            SetLastError("The link's app is not valid!");
            return false;
        }

        if (!await CheckURL(request.Url))
        {
            SetLastError("The link is not a file!");
            return false;
        }

        if (!await ScanURL(request.Url))
        {
            SetLastError("The link has been flagged as a malware by VirusTotal!");
            return false;
        }

        return true;
    }

    private static async Task<bool> CheckURL(string url)
    {
        var uri = new Uri(url);
        var fileInfo = new FileInfo(uri.AbsolutePath);
        if (!string.IsNullOrWhiteSpace(fileInfo.Extension))
        {
            return false;
        }

        // TODO: check the file from url for extension too
        using var client = new HttpClient();
        try
        {
            var response = await client.SendAsync(new(HttpMethod.Head, url));
            if (response.IsSuccessStatusCode && response.Content.Headers.ContentType != null)
            {
                var contentType = response.Content.Headers.ContentType.MediaType ?? "";
                return contentType.StartsWith("application/octet-stream");
            }
        }
        finally
        {
        }

        return false;
    }

    private static async Task<bool> ScanURL(string url)
    {
        var virusTotal = new VirusTotal(_virusTotalAPIKey) { UseTLS = true };

        var report = await virusTotal.GetUrlReportAsync(url);
        if (report.ResponseCode == UrlReportResponseCode.Present)
        {
            return report.Positives > 0;
        }
        else
        {
            await virusTotal.ScanUrlAsync(url);
            return await ScanURL(url);
        }
    }
}
}
