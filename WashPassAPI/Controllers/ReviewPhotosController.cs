using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewPhotosController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    // POST: api/ReviewPhotos
    [HttpPost]
    public async Task<ActionResult<ReviewPhoto>> Create(ReviewPhoto photo)
    {
        _context.ReviewPhotos.Add(photo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = photo.Id }, photo);
    }

    // GET: api/ReviewPhotos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewPhoto>> GetById(int id)
    {
        var photo = await _context.ReviewPhotos
            .Include(p => p.Review)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (photo == null)
            return NotFound();

        return photo;
    }

    // PUT: api/ReviewPhotos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ReviewPhoto updated)
    {
        if (id != updated.Id)
            return BadRequest("Photo ID mismatch");

        var existing = await _context.ReviewPhotos.FindAsync(id);
        if (existing == null)
            return NotFound();

        existing.ImageUrl = updated.ImageUrl;
        existing.ReviewId = updated.ReviewId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/ReviewPhotos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var photo = await _context.ReviewPhotos.FindAsync(id);
        if (photo == null)
            return NotFound();

        _context.ReviewPhotos.Remove(photo);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
