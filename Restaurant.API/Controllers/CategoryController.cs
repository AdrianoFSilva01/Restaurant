namespace Restaurant.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.API.Dtos.CatalogInfoDtos.CreateCatalogInfo;
using Restaurant.API.Dtos.CatalogInfoDtos.UpdateCatalogInfo;
using Restaurant.API.Dtos.CategoryDtos.CreateCategory;
using Restaurant.API.Dtos.CategoryDtos.GetCategories;
using Restaurant.API.Dtos.CategoryDtos.GetCategory;
using Restaurant.API.Dtos.CategoryDtos.UpdateCategory;
using Restaurant.API.Dtos.CategoryDtos.WeeklyCatalogs;
using Restaurant.API.Entitites.CatalogEntities;
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

    [HttpGet("initialize")]
    public IActionResult Initialize()
    {
        RestaurantInitializer.Initialize(_context);
        return Ok();
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<AllDetail>>> GetAll()
    {
        List<AllDetail> categories = await _context.Categories
            .Select(category => new AllDetail
            {
                Id = category.Id,
                Name = category.Name,
                ImageUrl = category.ImageUrl,
                HeroImageUrl = category.HeroImageUrl,
                Catalogs = category.Catalogs
                    .Select(catalog => new AllCatalogDetail
                    {
                        Id = catalog.Id,
                        Name = catalog.Name,
                        HeroImageUrl = catalog.HeroImageUrl,
                        HaveIngredients = catalog.Ingredients.Count > 0,
                        CatalogInfos = catalog.CatalogInfos
                            .Select(catalogInfo => new AllCatalogInfoDetails
                            {
                                Size = catalogInfo.Size,
                                Price = catalogInfo.Price,
                                Description = catalogInfo.Description
                            }).OrderBy(cI => cI.Price).ToList(),
                        Ingredients = catalog.Ingredients
                            .Select(ingredient => new AllCatalogIngredient
                            {
                                Id = ingredient.Id,
                                Name = ingredient.Name
                            }).ToList()
                    }).ToList()
            })
            .ToListAsync();

        return categories;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryDetail>>> GetCategories()
    {
        List<CategoryDetail> categories = await _context.Categories
            .Select(category => new CategoryDetail { Id = category.Id, Name = category.Name, ImageUrl = category.ImageUrl })
            .ToListAsync();

        return categories;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDetail>> GetCategory(int id)
    {
        var category = await _context.Categories
            .Select(category => new CategoryDetail
            {
                Id = category.Id,
                Name = category.Name,
                ImageUrl = category.ImageUrl
            })
            .FirstOrDefaultAsync(category => category.Id == id);

        return category;
    }

    [HttpGet("weekly")]
    public async Task<ActionResult<List<WeeklyCategoryDetail>>> GetWeeklyCatalogs()
    {
        int entradaId = 1;
        int carneId = 2;
        int peixeId = 3;
        var weeklyCatalogsIds = new List<int> {
            entradaId,
            carneId,
            peixeId
        };

        var category = await _context.Categories
            .Include(c => c.Catalogs)
            .ThenInclude(c => c.CatalogInfos)
            .Where(c => weeklyCatalogsIds.Contains(c.Id))
            .Select(c => new WeeklyCategoryDetail
            {
                Id = c.Id,
                Name = c.Name,
                ImageUrl = c.ImageUrl,
                Catalogs = c.Catalogs
                    .Select(catalog => new WeeklyCatalogDetail
                    {
                        Id = catalog.Id,
                        Name = catalog.Name,
                        ImageUrl = catalog.HeroImageUrl,
                        CatalogInfos = catalog.CatalogInfos
                            .Select(catalogInfo => new WeeklyCatalogInfoDetail
                            {
                                Size = catalogInfo.Size,
                                Price = catalogInfo.Price,
                                Description = catalogInfo.Description
                            }).OrderBy(cI => cI.Price).ToList()
                    })
                    .ToList()
            })
            .ToListAsync();

   
        foreach(var c in category)
        {
            if (c.Catalogs.Count > 3)
            {
                c.Catalogs.RemoveRange(3, c.Catalogs.Count - 3);
            }
        }

        return category;
    }

    [HttpGet("{id}/detail")]
    public async Task<ActionResult<List<CatalogDetail>>> GetCategoryDetail(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Catalogs)
            .ThenInclude(c => c.CatalogInfos)
            .Include(c => c.Catalogs)
            .ThenInclude(c => c.Ingredients)
            .Where(c => c.Id == id)
            .SelectMany(c => c.Catalogs
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
                        }).OrderBy(cI => cI.Price).ToList()
                }).ToList()
            )
            .ToListAsync();

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
