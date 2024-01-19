﻿using Huppy.API.Models;

using Microsoft.EntityFrameworkCore;

using Shared.Models;
using Shared.Utilities;

namespace Huppy.API.Services
{
public class CategoryService : BaseService<CategoryEntity>
{
    private readonly HuppyContext _context;

    public CategoryService(HuppyContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<CAL>> CategoriesToAppsWithLinks()
    {
        List<CAL> categoryToApps = [];

        var groupsUnordered = await _context.Apps.GroupBy(app => app.CategoryNavigation).ToListAsync();
        var groupsOrdered = groupsUnordered.OrderBy(group => group.Key.Name);
        foreach (var group in groupsOrdered)
        {
            var als = group.Select(app => new AL(app, [.._context.Links.Where(link => link.App == app.Id)])).ToList();
            categoryToApps.Add(new(group.Key, als));
        }

        return categoryToApps;
    }
}
}
