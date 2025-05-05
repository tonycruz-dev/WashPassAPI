namespace WashPassAPI.Models;

public class Vehicle
{
    public int Id { get; set; }

    public int AppUserId { get; set; }

    public string Make { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public string LicensePlate { get; set; } = string.Empty;

    public string VehicleType { get; set; } = string.Empty;

    public string PhotoUrl { get; set; } = string.Empty;

    // Optional: Navigation property if using Entity Framework
     public AppUser? AppUser { get; set; }
    public List<Booking> Bookings { get; set; } = [];
}
