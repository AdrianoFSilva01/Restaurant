namespace Restaurant.API.Entitites.CategoryEntities;
using global::Restaurant.API.Entitites.CatalogEntities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string HeroImageUrl { get; set; }
    public List<Catalog> Catalogs { get; set; } = new();
}
