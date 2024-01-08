﻿namespace Huppy.Services.Database
{
public class DatabaseService : BaseService
{
    public PackageDatabaseService Packages { get; set; } = new();
    public CategoryDatabaseService Categories { get; set; } = new();
    public AppDatabaseService Apps { get; set; } = new();
}
}
