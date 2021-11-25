namespace Restaurant.API.Dtos.CategoryDtos.GetCategory;

public class CatalogDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public bool HaveIngredients { get; set; }
    public List<CatalogInfoDetail> CatalogInfos { get; set; }
}

public class CatalogInfoDetail
{
    public string Size { get; set; }
    public float Price { get; set; }
    public string Description { get; set; }
}
