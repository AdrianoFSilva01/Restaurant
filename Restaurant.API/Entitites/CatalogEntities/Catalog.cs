namespace Restaurant.API.Entitites.CatalogEntities;
using Restaurant.API.Entitites.CategoryEntities;

public class Catalog
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string HeroImageUrl { get; set; }
    public Category Category { get; set; }
    public List<CatalogInfo> CatalogInfos { get; set; } = new();
    public List<Ingredient> Ingredients { get; set; } = new();
}
