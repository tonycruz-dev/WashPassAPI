using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.OdataControllers;



[Route("odata/AppUsers")]
[Tags("AppUsers")]
[ApiController]
[ODataAttributeRouting]
public class AppUsersOdataController(AppDbContext context)
{
    [HttpGet]
    [EnableQuery(PageSize = 100)]
    public IQueryable<AppUser> Get()
    {
        return context.AppUsers.AsNoTracking();
    }
}
