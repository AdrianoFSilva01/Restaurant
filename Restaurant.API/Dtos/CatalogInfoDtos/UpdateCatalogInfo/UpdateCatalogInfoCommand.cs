namespace Restaurant.API.Dtos.CatalogInfoDtos.UpdateCatalogInfo;
public class UpdateCatalogInfoCommand
{
    public int CatalogId { get; set; }
    public string Size { get; set; }
    public float Price { get; set; }
    public string Description { get; set; }
}
