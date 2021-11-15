namespace Restaurant.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.API.Dtos.CategoryDtos.GetCategories;
using Restaurant.API.Dtos.IngredientCategoryDtos.CreateIngredientCategory;
using Restaurant.API.Dtos.IngredientCategoryDtos.UpdateIngredientCategory;
using Restaurant.API.Entitites.IngredientCategoryEntities;
using Restaurant.API.Persistence;
using System.Net;

[ApiController]
[Route("[controller]")]
public class IngredientCategoryController : ControllerBase
{

    private readonly RestaurantDbContext _context;

    public IngredientCategoryController(RestaurantDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryDetail>>> GetAll()
    {
        List<CategoryDetail> categories = await _context.IngredientCategories
            .Include(s => s.IngredientSubCategories)
            .ThenInclude(s => s.Ingredients)
            .Select(x => new CategoryDetail { Id = x.Id, Name = x.Name, ImageUrl = x.ImageUrl })
            .ToListAsync();

        return categories;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> CreateIngredientCategory([FromBody] CreateIngredientCategoryCommand command)
    {
        IngredientCategory category = new IngredientCategory { Name = command.Name, ImageUrl = command.ImageUrl };

        _context.IngredientCategories.Add(category);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> UpdateIngredientCategory(int id, [FromBody] UpdateIngredientCategoryCommand command)
    {
        IngredientCategory ingredientCategory = await _context.IngredientCategories.FindAsync(id);

        if (ingredientCategory is not null)
        {
            ingredientCategory.Name = command.Name;
            ingredientCategory.ImageUrl = command.ImageUrl;
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeleteIngredientCategory(int id)
    {
        IngredientCategory ingredientCategory = await _context.IngredientCategories.FindAsync(id);

        if (ingredientCategory is not null)
        {
            _context.IngredientCategories.Remove(ingredientCategory);
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }
}
