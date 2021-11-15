namespace Restaurant.API.Dtos.CatalogInfoDtos.CreateCatalogInfo;
public class CreateCatalogInfoCommand
{
    public string Size { get; set; }
    public float Price { get; set; }
    public string Description { get; set; }
    public int CatalogId { get; set; }

}
