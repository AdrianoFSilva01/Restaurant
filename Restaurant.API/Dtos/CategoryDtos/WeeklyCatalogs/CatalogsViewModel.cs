namespace Restaurant.API.Dtos.CategoryDtos.WeeklyCatalogs;
public class WeeklyCategoryDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public List<WeeklyCatalogDetail> Catalogs { get; set; }
}

public class WeeklyCatalogDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public List<WeeklyCatalogInfoDetail> CatalogInfos { get; set; }
}

public class WeeklyCatalogInfoDetail
{
    public string Size { get; set; }
    public float Price { get; set; }
    public string Description { get; set; }
}
