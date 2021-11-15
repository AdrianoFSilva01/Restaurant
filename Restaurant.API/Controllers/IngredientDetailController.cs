namespace Restaurant.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Dtos.Queries.GetIngredientDetail;
using Restaurant.API.Entitites;
using Restaurant.API.Persistence;

[ApiController]
[Route("[controller]")]
public class IngredientDetailController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public IngredientDetailController(RestaurantDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IngredientDetailViewModel>> GetIngredientDetail(int id)
    {
        Ingredient ingredient = await _context.Ingredients.FindAsync(id);

        if (ingredient is not null)
        {
            return new IngredientDetailViewModel { Id = ingredient.Id, Name = ingredient.Name, ImageUrl = ingredient.ImageUrl };
        }

        return NotFound();
    }
}
