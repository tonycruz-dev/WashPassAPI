using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.OdataControllers;

[Route("odata/Reviews")]
[Tags("Reviews")]
[ApiController]
[ODataAttributeRouting]
public class ReviewsOdataController(AppDbContext context)
{
    [HttpGet]
    [EnableQuery(PageSize = 100)]
    public IQueryable<Review> Get()
    {
        return context.Reviews.AsNoTracking();
    }
}