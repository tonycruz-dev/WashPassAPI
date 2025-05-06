namespace WashPassAPI.Models;

public class AdminAccount
{
    public int Id { get; set; }

    public required string FullName { get; set; }

    public required string Email { get; set; }

    public string? Location { get; }

    public string? Role { get; set; }

    public required Guid UserId { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow.ToLocalTime();

    public List<CarWashStation> CarWashStations { get; set; } = [];

}