namespace Restaurant.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Dtos.CatalogInfoDtos.CreateCatalogInfo;
using Restaurant.API.Dtos.CatalogInfoDtos.UpdateCatalogInfo;
using Restaurant.API.Entitites.CatalogEntities;
using Restaurant.API.Persistence;
using System.Net;

[ApiController]
[Route("[controller]")]
public class CatalogInfoController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public CatalogInfoController(RestaurantDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> CreateCatalogInfo([FromBody] CreateCatalogInfoCommand command)
    {
        CatalogInfo catalogInfo = new CatalogInfo { Size = command.Size, Price = command.Price, Description = command.Description };

        Catalog catalog = await _context.Catalogs.FindAsync(command.CatalogId);

        if (catalog != null)
        {
            catalog.CatalogInfos.Add(catalogInfo);
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{catalogId}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> UpdateCatalogInfo(int catalogId, [FromBody] UpdateCatalogInfoCommand command)
    {
        CatalogInfo entity = _context.CatalogInfos.Where(c => c.Catalog.Id == command.CatalogId && c.Size == command.Size).FirstOrDefault();

        if (entity != null)
        {
            entity.Price = command.Price;
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }
}
