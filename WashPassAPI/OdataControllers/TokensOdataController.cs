using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.OdataControllers;

[Route("odata/Tokens")]
[Tags("Tokens")]
[ApiController]
[ODataAttributeRouting]
public class TokensOdataController(AppDbContext context)
{
    [HttpGet]
    [EnableQuery(PageSize = 100)]
    public IQueryable<Token> Get()
    {
        return context.Tokens.AsNoTracking();
    }
}
