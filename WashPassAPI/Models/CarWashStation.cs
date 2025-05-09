﻿namespace WashPassAPI.Models;

public class CarWashStation
{
    public int Id { get; set; }

    public  int AdminId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string OpeningTime { get; set; } = string.Empty;

    public string ClosingTime { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow.ToLocalTime();

    public AdminAccount? AdminAccount { get; set; } 

    public List<StationImage> Images { get; set; } = [];
    public List<Service> Services { get; set; } = [];
    public List<Booking> Bookings { get; set; } = [];
}
