using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StationImagesController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    // POST: api/StationImages
    [HttpPost]
    public async Task<ActionResult<StationImage>> Create(StationImage image)
    {
        image.CreatedAt = DateTimeOffset.UtcNow.ToLocalTime();
        _context.StationImages.Add(image);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = image.Id }, image);
    }

    // GET (for CreatedAtAction)
    [HttpGet("{id}")]
    public async Task<ActionResult<StationImage>> GetById(int id)
    {
        var image = await _context.StationImages
            .Include(i => i.Station)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (image == null)
            return NotFound();

        return image;
    }

    // PUT: api/StationImages/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, StationImage updatedImage)
    {
        if (id != updatedImage.Id)
            return BadRequest("ID mismatch");

        var existing = await _context.StationImages.FindAsync(id);
        if (existing == null)
            return NotFound();

        existing.ImageUrl = updatedImage.ImageUrl;
        existing.StationId = updatedImage.StationId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/StationImages/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var image = await _context.StationImages.FindAsync(id);
        if (image == null)
            return NotFound();

        _context.StationImages.Remove(image);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}