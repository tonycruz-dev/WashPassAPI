namespace WashPassAPI.Models;

public class StationImage
{
    public int Id { get; set; }

    public int StationId { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow.ToLocalTime();

    public CarWashStation? Station { get; set; } = null!;
}