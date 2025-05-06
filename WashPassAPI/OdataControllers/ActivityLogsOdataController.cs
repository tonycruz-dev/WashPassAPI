using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.OdataControllers;

[Route("odata/ActivityLogs")]
[Tags("ActivityLogs")]
[ApiController]
[ODataAttributeRouting]
public class ActivityLogsOdataController(AppDbContext context)
{
    [HttpGet]
    [EnableQuery(PageSize = 100)]
    public IQueryable<ActivityLog> Get()
    {
        return context.ActivityLogs.AsNoTracking();
    }
}
