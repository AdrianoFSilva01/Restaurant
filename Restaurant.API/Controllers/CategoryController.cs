namespace Restaurant.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.API.Dtos.CatalogInfoDtos.CreateCatalogInfo;
using Restaurant.API.Dtos.CatalogInfoDtos.UpdateCatalogInfo;
using Restaurant.API.Dtos.CategoryDtos.CreateCategory;
using Restaurant.API.Dtos.CategoryDtos.GetCategories;
using Restaurant.API.Dtos.CategoryDtos.GetCategory;
using Restaurant.API.Dtos.CategoryDtos.UpdateCategory;
using Restaurant.API.Entitites.CategoryEntities;
using Restaurant.API.Persistence;
using System.Net;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public CategoryController(RestaurantDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryDetail>>> GetCategories()
    {
        RestaurantInitializer.Initialize(_context);

        List<CategoryDetail> categories = await _context.Categories
            .Select(category => new CategoryDetail { Id = category.Id, Name = category.Name, ImageUrl = category.ImageUrl })
            .ToListAsync();

        return categories;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CatalogsInCategory>> GetCategory(int id)
    {
        CatalogsInCategory category = await _context.Categories
            .Include(c => c.Catalogs)
            .ThenInclude(c => c.CatalogInfos)
            .Include(c => c.Catalogs)
            .ThenInclude(c => c.Ingredients)
            .Where(c => c.Id == id)
            .Select(c => new CatalogsInCategory
            {
                Catalogs = c.Catalogs
                .Select(catalog => new CatalogDetail
                {
                    Id = catalog.Id,
                    Name = catalog.Name,
                    ImageUrl = catalog.ImageUrl,
                    HaveIngredients = catalog.Ingredients.Count > 0,
                    CatalogInfos = catalog.CatalogInfos
                        .Select(catalogInfo => new CatalogInfoDetail
                        {
                            Size = catalogInfo.Size,
                            Price = catalogInfo.Price,
                            Description = catalogInfo.Description
                        }).ToList()
                }).ToList()
            })
            .FirstOrDefaultAsync();

        return category;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        Category category = new Category { Name = command.Name, ImageUrl = command.ImageUrl };

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryCommand command)
    {
        Category entity = await _context.Categories.FindAsync(command.Id);

        if (entity != null)
        {
            entity.Name = command.Name;
            entity.ImageUrl = command.ImageUrl;
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        Category category = await _context.Categories.Where(sc => sc.Id == id).FirstOrDefaultAsync();
        if (category is not null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }
}
