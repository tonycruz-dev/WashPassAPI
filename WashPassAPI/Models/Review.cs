namespace WashPassAPI.Models;

public class Review
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public int Rating { get; set; }  // 1 to 5

    public string Comment { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToLocalTime();

    public Booking Booking { get; set; } = null!;
    public List<ReviewPhoto> Photos { get; set; } = [];
}
