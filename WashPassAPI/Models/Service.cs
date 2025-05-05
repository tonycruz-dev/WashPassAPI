using System.ComponentModel.DataAnnotations.Schema;

namespace WashPassAPI.Models;

public class Service
{
    public int Id { get; set; }

    public int CarWashStationId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int DurationMinutes { get; set; }

    public int TokenValue { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    public string ServiceType { get; set; } = string.Empty;

    [Column(TypeName = "money")]
    public decimal CommissionPercent { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToLocalTime();

    public CarWashStation? Station { get; set; } = null!;
}