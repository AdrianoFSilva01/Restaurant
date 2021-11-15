namespace Restaurant.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.API.Dtos.CatalogIngredientDtos.GetIngredients;
using Restaurant.API.Dtos.IngredientDtos.CreateIngredient;
using Restaurant.API.Dtos.IngredientDtos.UpdateIngredient;
using Restaurant.API.Entitites;
using Restaurant.API.Persistence;
using System.Net;

[ApiController]
[Route("[controller]")]
public class IngredientController : ControllerBase
{

    private readonly RestaurantDbContext _context;

    public IngredientController(RestaurantDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredientCommand command)
    {
        Ingredient ingredient = new Ingredient { Name = command.Name, ImageUrl = command.ImageUrl, IngredientSubCategoryId = command.SubCategoryId };
        _context.Ingredients.Add(ingredient);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> UpdateIngredient(int id, [FromBody] UpdateIngredientCommand command)
    {
        Ingredient entity = await _context.Ingredients.FindAsync(id);

        if (entity is not null)
        {
            entity.Name = command.Name;
            entity.ImageUrl = command.ImageUrl;
            entity.IngredientSubCategoryId = command.SubCategoryId;
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeleteIngredient(int id)
    {
        Ingredient ingredient = await _context.Ingredients.FindAsync(id);

        if (ingredient is not null)
        {
            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }
}
