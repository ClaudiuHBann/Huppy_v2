using Microsoft.AspNetCore.Mvc;

using Huppy.API.Services;

using Shared.Utilities;

namespace Huppy.API.Controllers
{
[ApiController]
[Route("[controller]")]
public class CategoryController : BaseController<CategoryController>
{
    private readonly CategoryService _categoryService;

    public CategoryController(ILogger<CategoryController> logger, CategoryService categoryService) : base(logger)
    {
        _categoryService = categoryService;
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetCategoryToApps()
    {
        var categoryToApps = await _categoryService.GetCategoryToApps();
        return Ok(categoryToApps.ToJSON());
    }
}
}
