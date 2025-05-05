namespace WashPassAPI.Models;

public class BookingService
{
    public int BookingServiceId { get; set; }

    public int BookingId { get; set; }

    public int ServiceId { get; set; }

    public Booking Booking { get; set; } = null!;

    public Service Service { get; set; } = null!;
}