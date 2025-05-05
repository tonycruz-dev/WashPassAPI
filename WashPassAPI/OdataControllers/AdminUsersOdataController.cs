using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.OdataControllers;

[Route("odata/AdminUsers")]
[Tags("AdminUsers")]
[ApiController]
[ODataAttributeRouting]
public class AdminUsersOdataController(AppDbContext context)
{
    [HttpGet]
    [EnableQuery(PageSize = 100)]
    public IQueryable<AdminUser> Get()
    {
        return context.AdminUsers.AsNoTracking();
    }
}
