namespace Restaurant.API.Entitites;

using Restaurant.API.Entitites.CatalogEntities;
using Restaurant.API.Entitites.IngredientCategoryEntities;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public int IngredientSubCategoryId { get; set; }
    public IngredientSubCategory IngredientSubCategory { get; set; }
    public List<Catalog> Catalogs { get; set; } = new();
}
