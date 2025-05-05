using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.OdataControllers;

[Route("odata/StationImages")]
[Tags("StationImages")]
[ApiController]
[ODataAttributeRouting]
public class StationImagesOdataController(AppDbContext context)
{
    [HttpGet]
    [EnableQuery(PageSize = 100)]
    public IQueryable<StationImage> Get()
    {
        return context.StationImages.AsNoTracking();
    }
}
