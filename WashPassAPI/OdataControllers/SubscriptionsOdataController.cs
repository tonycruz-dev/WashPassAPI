using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.OdataControllers;

[Route("odata/Subscriptions")]
[Tags("Subscriptions")]
[ApiController]
[ODataAttributeRouting]
public class SubscriptionsOdataController(AppDbContext context)
{
    [HttpGet]
    [EnableQuery(PageSize = 100)]
    public IQueryable<Subscription> Get()
    {
        return context.Subscriptions.AsNoTracking();
    }
}