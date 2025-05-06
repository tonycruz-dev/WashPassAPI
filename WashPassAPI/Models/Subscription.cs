using System.ComponentModel.DataAnnotations.Schema;

namespace WashPassAPI.Models;

public class Subscription
{
    public int Id { get; set; }

    public int AppUserId { get; set; }

    public string PlanName { get; set; } = string.Empty;

    [Column(TypeName = "money")]
    public decimal MonthlyFee { get; set; }

    public DateTime NextPaymentDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public AppUser User { get; set; } = null!;
}