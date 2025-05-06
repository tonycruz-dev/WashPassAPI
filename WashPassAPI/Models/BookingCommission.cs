using System.ComponentModel.DataAnnotations.Schema;

namespace WashPassAPI.Models;

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