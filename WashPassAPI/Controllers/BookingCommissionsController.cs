using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingCommissionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public BookingCommissionsController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/BookingCommissions
    [HttpPost]
    public async Task<ActionResult<BookingCommission>> Create(BookingCommission commission)
    {
        commission.CreatedAt = DateTime.UtcNow;

        _context.BookingCommissions.Add(commission);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = commission.Id }, commission);
    }

    // GET: api/BookingCommissions/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookingCommission>> GetById(int id)
    {
        var commission = await _context.BookingCommissions
            .Include(c => c.Booking)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (commission == null)
            return NotFound();

        return commission;
    }

    // PUT: api/BookingCommissions/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BookingCommission updated)
    {
        if (id != updated.Id)
            return BadRequest("Commission ID mismatch");

        var existing = await _context.BookingCommissions.FindAsync(id);
        if (existing == null)
            return NotFound();

        existing.BookingId = updated.BookingId;
        existing.CommissionPercent = updated.CommissionPercent;
        existing.CommissionAmount = updated.CommissionAmount;
        existing.PaidToAdmin = updated.PaidToAdmin;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/BookingCommissions/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var commission = await _context.BookingCommissions.FindAsync(id);
        if (commission == null)
            return NotFound();

        _context.BookingCommissions.Remove(commission);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
