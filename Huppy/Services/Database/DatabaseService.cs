namespace Huppy.Services.Database
{
public class DatabaseService : BaseService
{
    public PackageDatabaseService Package { get; set; } = new();
    public CategoryDatabaseService Category { get; set; } = new();
}
}
