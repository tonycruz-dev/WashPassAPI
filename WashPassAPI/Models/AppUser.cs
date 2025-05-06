namespace WashPassAPI.Models;

public class AppUser
{
    public int Id { get; set; } 
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public string? Location { get; set; }
    public required Guid UserId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public Subscription? Subscription { get; set; }
    public List<Vehicle> Vehicles { get; set; } = [];
    public List<Booking> Bookings { get; set; } = [];
    public List<Token> Tokens { get; set; } = [];
}
