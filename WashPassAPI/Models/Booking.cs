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
    public Review? Review { get; set; }
    public ICollection<BookingService> BookingServices { get; set; } = [];
    public BookingCommission? Commission { get; set; }
}

public class BookingCommission
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    [Column(TypeName = "money")]
    public decimal CommissionPercent { get; set; }

    [Column(TypeName = "money")]
    public decimal CommissionAmount { get; set; }

    public bool PaidToAdmin { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Booking? Booking { get; set; } = null!;
}