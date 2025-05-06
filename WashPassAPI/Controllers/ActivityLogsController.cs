using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Data;
using WashPassAPI.Models;

namespace WashPassAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivityLogsController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    // POST: api/ActivityLogs
    [HttpPost]
    public async Task<ActionResult<ActivityLog>> Create(ActivityLog log)
    {
        log.CreatedAt = DateTime.UtcNow;

        _context.ActivityLogs.Add(log);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = log.Id }, log);
    }

    // GET: api/ActivityLogs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityLog>> GetById(int id)
    {
        var log = await _context.ActivityLogs
            .Include(l => l.AdminAccount)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (log == null)
            return NotFound();

        return log;
    }

    // GET: api/ActivityLogs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActivityLog>>> GetAll()
    {
        var logs = await _context.ActivityLogs
            .Include(l => l.AdminAccount)
            .OrderByDescending(l => l.CreatedAt)
            .ToListAsync();

        return logs;
    }

    // DELETE: api/ActivityLogs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var log = await _context.ActivityLogs.FindAsync(id);
        if (log == null)
            return NotFound();

        _context.ActivityLogs.Remove(log);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
