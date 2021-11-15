namespace Restaurant.API.Entitites.IngredientCategoryEntities;
using System.Collections.Generic;

public class IngredientCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public List<IngredientSubCategory> IngredientSubCategories { get; set; } = new();
}
