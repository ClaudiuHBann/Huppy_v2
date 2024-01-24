using Shared.Models;
using Shared.Exceptions;
using Shared.Validators;
using Shared.Services.Database;

namespace Test.API
{
[TestClass]
public class AppUnitTest
{
    // the image will be converted to WEBP 256x256 anyway...
    private const string _imageWEBP256x256 =
        "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";

    private readonly DatabaseService _database = new();

    private readonly AppValidator _appValidator = new();

    // clang-format off
    public static AppEntity AppEntityValid { get; } = new() {
        Proposed = true,
        Name = $"App's Name - {DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}",
        Category = CategoryEntity.CategoryOther,
        Image = Convert.FromBase64String(_imageWEBP256x256)
    };

    private const string AppEntityTestName = "Opera GX";

    public static IEnumerable<object[]> CreateInput { get; } = [
        [true, AppEntityValid], // trying to create a valid app
        [false, new AppEntity(AppEntityValid) { Proposed = false }], // trying to create a legit app
        [false, new AppEntity(AppEntityValid) { Name = AppEntityTestName }], // trying to create an app with an existing name
        [false, new AppEntity(AppEntityValid) { Id = Guid.NewGuid() }], // trying to create an app with a random id
        [false, new AppEntity(AppEntityValid) { Image = [4, 69, 20] }], // trying to create an app with a invalid image
        [false, new AppEntity(AppEntityValid) { Category = Guid.NewGuid() }], // trying to create an app with a invalid category
        [false, new AppEntity(AppEntityValid) { Name = new string('x', 256) }] // trying to create an app with exceeding large name
    ];
    // clang-format on

    [DataTestMethod]
    [DynamicData(nameof(CreateInput))]
    public async Task Create(bool valid, AppEntity entity)
    {
        try
        {
            var response = await _database.Apps.Create(new(entity));

            // we don't check the image because it will always change it to a 256x256 WEBP
            Assert.AreNotEqual(response.Id, entity.Id);
            Assert.AreEqual(response.Category, entity.Category);
            Assert.AreEqual(response.Name, entity.Name);
            Assert.IsTrue(response.Proposed);
            Assert.IsFalse(response.Updated);
            Assert.IsFalse(response.Deleted);
        }
        catch (DatabaseException exception)
        {
            Assert.IsFalse(valid, exception.Message);
        }
        catch (Exception exception)
        {
            Assert.Fail(exception.Message);
        }
    }

    [TestMethod]
    public async Task Read()
    {
    }

    [TestMethod]
    public async Task Update()
    {
    }

    [TestMethod]
    public async Task Delete()
    {
    }
}
}
