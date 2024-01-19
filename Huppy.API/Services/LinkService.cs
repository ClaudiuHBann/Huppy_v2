using System.Net;

using Huppy.API.Models;

using Shared.Models;
using Shared.Exceptions;

using Microsoft.EntityFrameworkCore;

using VirusTotalNet;
using VirusTotalNet.ResponseCodes;

namespace Huppy.API.Services
{
public class LinkService
(HuppyContext context) : BaseService<LinkEntity>(context)
{
    private const string _virusTotalAPIKey = "9b4a57c29539ad065b0ef6577ce9113ba89674b69e12e79b0336df087731fda4";

    protected override async Task CreateValidate(LinkEntity entity)
    {
        if (!await context.Apps.AnyAsync(app => app.Id == entity.App))
        {
            throw new DatabaseException(new(HttpStatusCode.BadRequest, "The link's app is not valid!"));
        }

        var entityWithSameName = await context.Links.FirstOrDefaultAsync(app => app.Url == entity.Url);
        if (entityWithSameName != null && entity.Id != entityWithSameName.Id)
        {
            throw new DatabaseException(new(HttpStatusCode.BadRequest, $"The link \"{entity.Url}\" already exists!"));
        }

        if (!await CheckURL(entity.Url))
        {
            throw new DatabaseException(new(HttpStatusCode.BadRequest, "The link is not a file!"));
        }

        if (await ScanURL(entity.Url))
        {
            throw new DatabaseException(
                new(HttpStatusCode.BadRequest, "The link has been flagged as a malware by VirusTotal!"));
        }
    }

    protected override async Task UpdateValidate(LinkEntity entity)
    {
        var entityLink = await ReadEx(entity.Id);
        if (entityLink == null)
        {
            throw new DatabaseException(new(HttpStatusCode.BadRequest, "The link is not a file!"));
        }

        var entityApp = await context.Apps.FindAsync(entityLink.App);
        if (entityApp != null && entityApp.Proposed == false)
        {
            throw new DatabaseException(new(HttpStatusCode.Unauthorized, "A trusted link can not be edited!"));
        }

        if (!await CheckURL(entity.Url))
        {
            throw new DatabaseException(new(HttpStatusCode.BadRequest, "The link is not a file!"));
        }

        if (await ScanURL(entity.Url))
        {
            throw new DatabaseException(
                new(HttpStatusCode.BadRequest, "The link has been flagged as a malware by VirusTotal!"));
        }
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
