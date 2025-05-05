using Microsoft.AspNetCore.Mvc;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    // POST: api/Vehicles
    [HttpPost]
    public async Task<ActionResult<Vehicle>> CreateVehicle(Vehicle vehicle)
    {
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.Id }, vehicle);
    }

    // GET (helper for CreatedAtAction)
    [HttpGet("{id}")]
    public async Task<ActionResult<Vehicle>> GetVehicle(int id)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);

        if (vehicle == null)
            return NotFound();

        return vehicle;
    }

    // PUT: api/Vehicles/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVehicle(int id, Vehicle updatedVehicle)
    {
        if (id != updatedVehicle.Id)
            return BadRequest("Vehicle ID mismatch.");

        var existingVehicle = await _context.Vehicles.FindAsync(id);
        if (existingVehicle == null)
            return NotFound();

        // Update fields
        existingVehicle.Make = updatedVehicle.Make;
        existingVehicle.Model = updatedVehicle.Model;
        existingVehicle.LicensePlate = updatedVehicle.LicensePlate;
        existingVehicle.VehicleType = updatedVehicle.VehicleType;
        existingVehicle.PhotoUrl = updatedVehicle.PhotoUrl;
        existingVehicle.AppUserId = updatedVehicle.AppUserId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Vehicles/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);
        if (vehicle == null)
            return NotFound();

        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}