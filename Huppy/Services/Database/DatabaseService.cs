namespace Huppy.Services.Database
{
public class DatabaseService
{
    public PackageDatabaseService Package { get; set; } = new();
    public CategoryDatabaseService Category { get; set; } = new();
}
}
