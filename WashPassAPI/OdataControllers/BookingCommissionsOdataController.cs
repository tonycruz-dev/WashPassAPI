using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.OdataControllers;

[Route("odata/BookingCommissions")]
[Tags("BookingCommissions")]
[ApiController]
[ODataAttributeRouting]
public class BookingCommissionsOdataController(AppDbContext context)
{
    [HttpGet]
    [EnableQuery(PageSize = 100)]
    public IQueryable<BookingCommission> Get()
    {
        return context.BookingCommissions.AsNoTracking();
    }
}
