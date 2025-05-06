using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokensController : ControllerBase
{
    private readonly AppDbContext _context;

    public TokensController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/Tokens
    [HttpPost]
    public async Task<ActionResult<Token>> Create(Token token)
    {
        token.AcquiredAt = DateTime.UtcNow;

        _context.Tokens.Add(token);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = token.Id }, token);
    }

    // GET: api/Tokens/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Token>> GetById(int id)
    {
        var token = await _context.Tokens
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (token == null)
            return NotFound();

        return token;
    }

    // PUT: api/Tokens/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Token updated)
    {
        if (id != updated.Id)
            return BadRequest("Token ID mismatch");

        var existing = await _context.Tokens.FindAsync(id);
        if (existing == null)
            return NotFound();

        existing.AppUserId = updated.AppUserId;
        existing.Amount = updated.Amount;
        existing.Source = updated.Source;
        existing.AcquiredAt = updated.AcquiredAt;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Tokens/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var token = await _context.Tokens.FindAsync(id);
        if (token == null)
            return NotFound();

        _context.Tokens.Remove(token);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}