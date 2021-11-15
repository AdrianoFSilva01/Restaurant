namespace Restaurant.API.Entitites.IngredientCategoryEntities;

using Restaurant.API.Entitites;
using Restaurant.API.Entitites.CategoryEntities;
using System.Collections.Generic;

public class IngredientSubCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public IngredientCategory IngredientCategory { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new();
}
