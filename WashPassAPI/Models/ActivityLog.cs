namespace WashPassAPI.Models;

public class ActivityLog
{
    public int Id { get; set; }

    public int AdminId { get; set; }

    public string ActionType { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public AdminAccount? AdminAccount { get; set; }
}