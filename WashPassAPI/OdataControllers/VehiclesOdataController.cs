using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.OdataControllers;

[Route("odata/Vehicles")]
[Tags("Vehicles")]
[ApiController]
[ODataAttributeRouting]
public class VehiclesOdataController(AppDbContext context)
{
    [HttpGet]
    [EnableQuery(PageSize = 100)]
    public IQueryable<Vehicle> Get()
    {
        return context.Vehicles.AsNoTracking();
    }
}

