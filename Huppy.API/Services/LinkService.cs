using Huppy.API.Models;

using Shared.Models;

using Microsoft.EntityFrameworkCore;

using VirusTotalNet;
using VirusTotalNet.ResponseCodes;

namespace Huppy.API.Services
{
public class LinkService
(HuppyContext context) : BaseService<LinkEntity>(context)
{
    private const string _virusTotalAPIKey = "9b4a57c29539ad065b0ef6577ce9113ba89674b69e12e79b0336df087731fda4";

    protected override async Task<bool> CreateValidate(LinkEntity? entity)
    {
        ClearLastError();

        if (entity == null)
        {
            return false;
        }

        if (!await context.Apps.AnyAsync(app => app.Id == entity.App))
        {
            SetLastError("The link's app is not valid!");
            return false;
        }

        var entityWithSameName = await context.Links.FirstOrDefaultAsync(app => app.Url == entity.Url);
        if (entityWithSameName != null && entity.Id != entityWithSameName.Id)
        {
            SetLastError($"The link \"{entity.Url}\" already exists!");
            return false;
        }

        if (!await CheckURL(entity.Url))
        {
            SetLastError("The link is not a file!");
            return false;
        }

        if (await ScanURL(entity.Url))
        {
            SetLastError("The link has been flagged as a malware by VirusTotal!");
            return false;
        }

        return true;
    }

    protected override async Task<bool> UpdateValidate(LinkEntity? entity)
    {
        ClearLastError();

        if (entity == null)
        {
            return false;
        }

        var entityLink = await ReadEx(entity.Id);
        if (entityLink == null)
        {
            SetLastError("The link could not be found!");
            return false;
        }

        var entityApp = await context.Apps.FindAsync(entityLink.App);
        if (entityApp != null && entityApp.Proposed == false)
        {
            SetLastError("A trusted link can not be edited!");
            return false;
        }

        if (!await CheckURL(entity.Url))
        {
            SetLastError("The link is not a file!");
            return false;
        }

        if (await ScanURL(entity.Url))
        {
            SetLastError("The link has been flagged as a malware by VirusTotal!");
            return false;
        }

        return true;
    }

    private static async Task<bool> CheckURL(string url)
    {
        // TODO: better check here !!!

        try
        {
            var uri = new Uri(url);
            var fileInfo = new FileInfo(uri.AbsolutePath);
            if (string.IsNullOrWhiteSpace(fileInfo.Extension))
            {
                return false;
            }

            using var client = new HttpClient();
            var response = await client.SendAsync(new(HttpMethod.Head, url));
            if (response.IsSuccessStatusCode && response.Content.Headers.ContentType != null)
            {
                var contentType = response.Content.Headers.ContentType.MediaType ?? "";
                return contentType.StartsWith("application/");
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
