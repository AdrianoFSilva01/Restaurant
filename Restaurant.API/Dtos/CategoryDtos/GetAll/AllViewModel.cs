namespace Restaurant.API.Dtos.CategoryDtos.GetCategories;
public class AllDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string HeroImageUrl { get; set; }
    public List<AllCatalogDetail> Catalogs { get; set; }
}

public class AllCatalogDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string HeroImageUrl { get; set; }
    public bool HaveIngredients { get; set; }
    public List<AllCatalogInfoDetails> CatalogInfos { get; set; }
    public List<AllCatalogIngredient> Ingredients { get; set; }
}

public class AllCatalogInfoDetails
{
    public string Size { get; set; }
    public float Price { get; set; }
    public string Description { get; set; }
}

public class AllCatalogIngredient
{
    public int Id { get; set; }
    public string Name { get; set; }
}
