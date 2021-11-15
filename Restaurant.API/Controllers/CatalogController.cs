namespace Restaurant.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.API.Dtos.CatalogDtos.CreateCatalog;
using Restaurant.API.Dtos.CatalogDtos.UpdateCatalog;
using Restaurant.API.Dtos.Queries.GetIngredientsNames;
using Restaurant.API.Entitites.CatalogEntities;
using Restaurant.API.Entitites.CategoryEntities;
using Restaurant.API.Persistence;
using System.Net;

[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public CatalogController(RestaurantDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> CreateCatalog([FromBody] CreateCatalogCommand command)
    {
        Catalog catalog = new Catalog { Name = command.Name, ImageUrl = command.ImageUrl };

        Category category = await _context.Categories.FindAsync(command.CategoryId);

        if (category != null)
        {
            category.Catalogs.Add(catalog);
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> UpdateCatalog(int id, [FromBody] UpdateCatalogCommand command)
    {
        Catalog entity = await _context.Catalogs.FindAsync(command.Id);

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
    public async Task<IActionResult> DeleteCatalog(int id)
    {
        Catalog catalog = await _context.Catalogs.Where(sc => sc.Id == id).FirstOrDefaultAsync();
        if (catalog is not null)
        {
            _context.Catalogs.Remove(catalog);
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<GetCatalogIngredient>>> GetCatalogIngredients(int id)
    {
        List<GetCatalogIngredient> ingredients = await _context.Catalogs
            .Include(c => c.Ingredients)
            .Where(c => c.Id == id)
            .SelectMany(c => c.Ingredients.Select(x => new GetCatalogIngredient { Id = x.Id, Name = x.Name }).ToList())
            .ToListAsync();

        return ingredients;
    }
}
