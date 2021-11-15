namespace Restaurant.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.API.Dtos.CatalogIngredientDtos.GetIngredients;
using Restaurant.API.Persistence;

[ApiController]
[Route("[controller]")]
public class IngredientsCategoryController : ControllerBase
{

    private readonly RestaurantDbContext _context;

    public IngredientsCategoryController(RestaurantDbContext context)
    {
        _context = context;
    }

    [HttpGet("{ids}")]
    public async Task<ActionResult<List<IngredientName>>> GetIngredients(string ids)
    {
        List<(bool Ok, int Value)> numIds = ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x)).ToList();

        if (!numIds.All(nid => nid.Ok))
        {
            return NotFound();
        }

        List<int> idsToSelect = numIds.Select(id => id.Value).ToList();

        List<IngredientName> ingredients = await _context.Ingredients
            .Where(i => idsToSelect.Contains(i.Id))
            .Select(i => new IngredientName { Id = i.Id, Name = i.Name })
            .ToListAsync();

        return ingredients;
    }
}
