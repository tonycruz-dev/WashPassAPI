namespace WashPassAPI.Models;

public class Token
{
    public int Id { get; set; }

    public int AppUserId { get; set; }

    public int Amount { get; set; }

    public DateTime AcquiredAt { get; set; } = DateTime.UtcNow;

    public string Source { get; set; } = string.Empty;

    public AppUser User { get; set; } = null!;
}