using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    // POST: api/Bookings
    [HttpPost]
    public async Task<ActionResult<Booking>> Create(Booking booking)
    {
        booking.CreatedAt = DateTime.UtcNow;

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
    }

    // GET (used by CreatedAtAction, optional)
    [HttpGet("{id}")]
    public async Task<ActionResult<Booking>> GetById(int id)
    {
        var booking = await _context.Bookings
            .Include(b => b.User)
            .Include(b => b.Station)
            .Include(b => b.Vehicle)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null)
            return NotFound();

        return booking;
    }

    // PUT: api/Bookings/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Booking updated)
    {
        if (id != updated.Id)
            return BadRequest("Booking ID mismatch");

        var existing = await _context.Bookings.FindAsync(id);
        if (existing == null)
            return NotFound();

        existing.UserId = updated.UserId;
        existing.CarWashStationId = updated.CarWashStationId;
        existing.VehicleId = updated.VehicleId;
        existing.BookingDate = updated.BookingDate;
        existing.ArrivalTimeStart = updated.ArrivalTimeStart;
        existing.ArrivalTimeEnd = updated.ArrivalTimeEnd;
        existing.TotalPrice = updated.TotalPrice;
        existing.Note = updated.Note;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Bookings/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
            return NotFound();

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}