using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.OdataControllers;

[Route("odata/CarWashStations")]
[Tags("CarWashStations")]
[ApiController]
[ODataAttributeRouting]
public class CarWashStationsOdataController(AppDbContext context)
{
    [HttpGet]
    [EnableQuery(PageSize = 100)]
    public IQueryable<CarWashStation> Get()
    {
        return context.CarWashStations.AsNoTracking();
    }
}

