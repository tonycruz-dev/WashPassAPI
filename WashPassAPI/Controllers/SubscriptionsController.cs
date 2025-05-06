using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SubscriptionsController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/Subscriptions
    [HttpPost]
    public async Task<ActionResult<Subscription>> Create(Subscription subscription)
    {
        subscription.CreatedAt = DateTime.UtcNow;
        _context.Subscriptions.Add(subscription);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = subscription.Id }, subscription);
    }

    // GET (optional, for CreatedAtAction)
    [HttpGet("{id}")]
    public async Task<ActionResult<Subscription>> GetById(int id)
    {
        var sub = await _context.Subscriptions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sub == null)
            return NotFound();

        return sub;
    }

    // PUT: api/Subscriptions/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Subscription updated)
    {
        if (id != updated.Id)
            return BadRequest("Subscription ID mismatch");

        var existing = await _context.Subscriptions.FindAsync(id);
        if (existing == null)
            return NotFound();

        existing.PlanName = updated.PlanName;
        existing.MonthlyFee = updated.MonthlyFee;
        existing.NextPaymentDate = updated.NextPaymentDate;
        existing.AppUserId = updated.AppUserId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Subscriptions/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var sub = await _context.Subscriptions.FindAsync(id);
        if (sub == null)
            return NotFound();

        _context.Subscriptions.Remove(sub);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
