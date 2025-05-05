using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    // POST: api/Services
    [HttpPost]
    public async Task<ActionResult<Service>> Create(Service service)
    {
        service.CreatedAt = DateTime.UtcNow;
        _context.Services.Add(service);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = service.Id }, service);
    }

    // GET: api/Services/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Service>> GetById(int id)
    {
        var service = await _context.Services
            .Include(s => s.Station)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (service == null)
            return NotFound();

        return service;
    }

    // PUT: api/Services/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Service updatedService)
    {
        if (id != updatedService.Id)
            return BadRequest("ID mismatch");

        var existing = await _context.Services.FindAsync(id);
        if (existing == null)
            return NotFound();

        existing.CarWashStationId = updatedService.Id;
        existing.Name = updatedService.Name;
        existing.DurationMinutes = updatedService.DurationMinutes;
        existing.TokenValue = updatedService.TokenValue;
        existing.Price = updatedService.Price;
        existing.ServiceType = updatedService.ServiceType;
        existing.CommissionPercent = updatedService.CommissionPercent;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Services/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service == null)
            return NotFound();

        _context.Services.Remove(service);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
