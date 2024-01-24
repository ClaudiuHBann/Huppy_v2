using System.Net;

using Huppy.API.Models;

using Microsoft.EntityFrameworkCore;

using Shared.Models;
using Shared.Exceptions;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Huppy.API.Services
{
public class AppService : BaseService<AppEntity>
{
    private const int _appIconSize = 256; // 256x256 WEBP

    private readonly HuppyContext _context;

    public AppService(HuppyContext context) : base(context)
    {
        _context = context;
    }

    protected override async Task CreateValidate(AppEntity entity) => await Validate(entity);
    // Read validates at the same time
    protected override Task ReadValidate(AppEntity entity) => Task.CompletedTask;
    protected override async Task UpdateValidate(AppEntity entity) => await Validate(entity);
    // Delete validates at the same time
    protected override Task DeleteValidate(AppEntity entity) => Task.CompletedTask;

    private async Task Validate(AppEntity entity)
    {
        if (!await _context.Categories.AnyAsync(category => category.Id == entity.Category))
        {
            throw new DatabaseException(new(HttpStatusCode.BadRequest, "The app's category is not valid!"));
        }

        // only for Update but works for Create too...
        var entityReal = await ReadEx(entity.Id);
        if (entityReal != null && entityReal.Proposed == false)
        {
            throw new DatabaseException(new(HttpStatusCode.Unauthorized, "A trusted app can not be edited!"));
        }

        var entityRealWithSameName = await _context.Apps.FirstOrDefaultAsync(app => app.Name == entity.Name);
        if (entityRealWithSameName != null && entity.Id != entityRealWithSameName.Id)
        {
            throw new DatabaseException(new(HttpStatusCode.BadRequest, $"The app '{entity.Name}' already exists!"));
        }
    }

    public override async Task<AppEntity> Create(AppEntity entity)
    {
        MemoryStream stream = new(entity.Image);
        Image image = await Image.LoadAsync(stream);

        image.Mutate(config => config.Resize(_appIconSize, _appIconSize));
        stream.Position = 0; // reset for writing
        await image.SaveAsWebpAsync(stream);

        entity.Proposed = true;
        return await CreateEx(entity);
    }

    public override async Task<AppEntity> Read(AppEntity entity) => await FindByIdOrName(entity.Id, entity.Name);

    public override async Task<AppEntity> Delete(AppEntity entity) => await DeleteEx(await Read(entity));

    private async Task<AppEntity> FindByIdOrName(Guid id, string name)
    {
        var entity = await _context.Apps.FirstOrDefaultAsync(app => app.Name == name);
        if (entity != null)
        {
            return entity;
        }

        return await ReadEx(id);
    }
}
}
