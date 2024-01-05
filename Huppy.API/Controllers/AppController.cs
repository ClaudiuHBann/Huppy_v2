using Huppy.API.Services;

using Microsoft.AspNetCore.Mvc;

using Shared.Models;
using Shared.Requests;
using Shared.Responses;
using Shared.Utilities;

namespace Huppy.API.Controllers
{
[ApiController]
[Route("[controller]")]
public class AppController : BaseController<AppController>
{
    private readonly AppService _appService;

    public AppController(ILogger<AppController> logger, AppService appService) : base(logger)
    {
        _appService = appService;
    }

    [HttpPost(nameof(Create))]
    public async Task<ActionResult> Create([FromBody] AppRequest appRequest)
    {
        var appEntityWithSameName = await _appService.FirstOrDefault(package => package.Name == appRequest.Name);
        if (appEntityWithSameName != null)
        {
            return MakeAndLogBadRequest($"The app with the name \"{appRequest.Name}\" already exists!");
        }

        var id = await _appService.Count() + 1;

        AppEntity appEntity =
            new() { Id = id, Category = appRequest.Category, Name = appRequest.Name, ImageRaw = appRequest.ImageRaw };

        LinkEntity linkEntity = new() { App = id, Url = appRequest.Url };

        if (await _appService.Add(appEntity, linkEntity))
        {
            var appResponse = new AppResponse(appEntity);
            return Ok(appResponse.ToJSON());
        }
        else
        {
            return MakeAndLogBadRequest("The app could not be created.");
        }
    }
}
}
