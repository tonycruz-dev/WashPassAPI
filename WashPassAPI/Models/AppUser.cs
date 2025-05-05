namespace WashPassAPI.Models;

public class AppUser
{
    public int Id { get; set; } 
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public string? Location { get; }
    public required Guid UserId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
