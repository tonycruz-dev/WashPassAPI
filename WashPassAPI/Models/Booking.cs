using System.ComponentModel.DataAnnotations.Schema;

namespace WashPassAPI.Models;

public class Booking
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public int CarWashStationId { get; set; }
    public int VehicleId { get; set; }

    public DateTime BookingDate { get; set; }

    public TimeSpan ArrivalTimeStart { get; set; }
    public TimeSpan ArrivalTimeEnd { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalPrice { get; set; }

    public string Note { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow.ToLocalTime();

    public AppUser? User { get; set; } 
    public CarWashStation? Station { get; set; }
    public Vehicle? Vehicle { get; set; }
    public List<BookingService> BookingServices { get; set; } = [];
}