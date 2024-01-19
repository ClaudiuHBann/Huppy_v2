using System.Net;

using Huppy.API.Models;
using Huppy.API.Controllers;

using Microsoft.EntityFrameworkCore;

using Shared.Models;
using Shared.Exceptions;

namespace Huppy.API.Services
{
public class PackageService : BaseService<PackageEntity>
{
    private readonly HuppyContext _context;
    private readonly ILogger<PackageController> _logger;

    public PackageService(ILogger<PackageController> logger, HuppyContext context) : base(context)
    {
        _logger = logger;
        _context = context;
    }

    protected override async Task CreateValidate(PackageEntity entity) => await ValideApps(entity.Apps);

    // Read validates at the same time
    protected override Task ReadValidate(PackageEntity entity) => Task.CompletedTask;

    protected override async Task UpdateValidate(PackageEntity entity)
    {
        await ValideApps(entity.Apps);
        await ReadEx(entity.Id); // checks for it's existance

        var entityWithSameName = await _context.Packages.FirstOrDefaultAsync(package => package.Name == entity.Name);
        if (entityWithSameName != null && entity.Id != entityWithSameName.Id)
        {
            throw new DatabaseException(
                new(HttpStatusCode.BadRequest, $"The package \"{entity.Name}\" already exists!"));
        }
    }

    // Delete validates at the same time
    protected override Task DeleteValidate(PackageEntity entity) => Task.CompletedTask;

    public override async Task<PackageEntity> Create(PackageEntity entity)
    {
        await CreateValidate(entity);

        // this method will never fail on name not unqiue so:
        entity.Name = await FindUniquePackageName(entity.Name);

        return await Create(entity);
    }

    public override async Task<PackageEntity> Read(PackageEntity entity) => await FindByIdOrName(entity.Id,
                                                                                                 entity.Name);

    public override async Task<PackageEntity> Delete(PackageEntity entity) => await DeleteEx(await Read(entity));

    private async Task ValideApps(Guid[] apps)
    {
        // TODO: a better way of not doing 69 million requests?
        foreach (var app in apps)
        {
            if (await _context.Apps.FindAsync(app) == null)
            {
                throw new DatabaseException(new(HttpStatusCode.BadRequest, "The package's apps contain invalid apps!"));
            }
        }
    }

    private async Task<PackageEntity> FindByIdOrName(Guid id, string name)
    {
        var entity = await _context.Packages.FirstOrDefaultAsync(package => package.Name == name);
        if (entity != null)
        {
            return entity;
        }

        return await ReadEx(id);
    }

    private async Task<string> FindUniquePackageName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || await _context.Packages.AnyAsync(package => package.Name == name))
        {
            name = Guid.NewGuid().ToString();
            _logger.LogInformation($"The package's name was not unique so we generated one: {name}");
        }

        return name;
    }
}
}
