using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    // POST: api/Reviews
    [HttpPost]
    public async Task<ActionResult<Review>> Create(Review review)
    {
        // Optional: Validate rating range
        if (review.Rating < 1 || review.Rating > 5)
            return BadRequest("Rating must be between 1 and 5.");

        review.CreatedAt = DateTime.UtcNow;
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = review.Id }, review);
    }

    // GET (used for CreatedAtAction, optional)
    [HttpGet("{id}")]
    public async Task<ActionResult<Review>> GetById(int id)
    {
        var review = await _context.Reviews
            .Include(r => r.Booking)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (review == null)
            return NotFound();

        return review;
    }

    // PUT: api/Reviews/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Review updated)
    {
        if (id != updated.Id)
            return BadRequest("ID mismatch");

        if (updated.Rating < 1 || updated.Rating > 5)
            return BadRequest("Rating must be between 1 and 5.");

        var existing = await _context.Reviews.FindAsync(id);
        if (existing == null)
            return NotFound();

        existing.Rating = updated.Rating;
        existing.Comment = updated.Comment;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Reviews/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
            return NotFound();

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
