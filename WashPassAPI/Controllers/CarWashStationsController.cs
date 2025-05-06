using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarWashStationsController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    // POST: api/CarWashStations
    [HttpPost]
    public async Task<ActionResult<CarWashStation>> Create(CarWashStation station)
    {
        station.CreatedAt = DateTimeOffset.UtcNow;
        _context.CarWashStations.Add(station);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = station.Id }, station);
    }

    // GET: api/CarWashStations/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CarWashStation>> GetById(int id)
    {
        var station = await _context.CarWashStations
            .Include(s => s.Images)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (station == null)
            return NotFound();

        return station;
    }

    // PUT: api/CarWashStations/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CarWashStation updatedStation)
    {
        if (id != updatedStation.Id)
            return BadRequest("ID mismatch");

        var existing = await _context.CarWashStations.FindAsync(id);
        if (existing == null)
            return NotFound();

        // Update fields
        existing.Name = updatedStation.Name;
        existing.Address = updatedStation.Address;
        existing.Latitude = updatedStation.Latitude;
        existing.Longitude = updatedStation.Longitude;
        existing.OpeningTime = updatedStation.OpeningTime;
        existing.ClosingTime = updatedStation.ClosingTime;
        existing.Description = updatedStation.Description;
        existing.PhoneNumber = updatedStation.PhoneNumber;
        existing.AdminId = updatedStation.AdminId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/CarWashStations/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var station = await _context.CarWashStations.FindAsync(id);
        if (station == null)
            return NotFound();

        _context.CarWashStations.Remove(station);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}