namespace Restaurant.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Dtos.CatalogIngredientDtos.CreateCatalogIngredient;
using Restaurant.API.Entitites;
using Restaurant.API.Entitites.CatalogEntities;
using Restaurant.API.Persistence;
using System.Net;

[ApiController]
[Route("[controller]")]
public class CatalogIngredientController : ControllerBase
{

    private readonly RestaurantDbContext _context;

    public CatalogIngredientController(RestaurantDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> CreateCatalogIngredient([FromBody] CreateCatalogIngredientCommand command)
    {
        Ingredient ingredient = await _context.Ingredients.FindAsync(command.IngredientId);

        Catalog catalog = await _context.Catalogs.FindAsync(command.CatalogId);

        if (catalog != null)
        {
            catalog.Ingredients.Add(ingredient);
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{catalogId}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeleteCatalogIngredient(int catalogId, int ingredientId)
    {
        Ingredient ingredient = await _context.Ingredients.FindAsync(ingredientId);

        Catalog catalog = await _context.Catalogs.FindAsync(catalogId);

        if (ingredient is not null && catalog is not null)
        {
            catalog.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }
}
