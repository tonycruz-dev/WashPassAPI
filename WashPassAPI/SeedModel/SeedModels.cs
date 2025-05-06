namespace WashPassAPI.SeedModel;

public class Rootobject
{
    public Washpass Washpass { get; set; }
}

public class Washpass
{
    public Adminaccount[] AdminAccounts { get; set; }
    public Carwashstation[] CarWashStations { get; set; }
    public Service[] Services { get; set; }
    public Stationimage[] StationImages { get; set; }
    public Appuser[] AppUsers { get; set; }
    public Subscription[] Subscriptions { get; set; }
    public Vehicle[] Vehicles { get; set; }
    public Token[] Tokens { get; set; }
    public Booking[] Bookings { get; set; }
    public Bookingservice[] BookingServices { get; set; }
    public Bookingcommission[] BookingCommissions { get; set; }
    public Review[] Reviews { get; set; }
    public Reviewphoto[] ReviewPhotos { get; set; }
    public Activitylog[] ActivityLogs { get; set; }
}

public class Adminaccount
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Location { get; set; }
    public string Role { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class Carwashstation
{
    public int Id { get; set; }
    public int AdminId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public string OpeningTime { get; set; }
    public string ClosingTime { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class Service
{
    public int Id { get; set; }
    public int CarWashStationId { get; set; }
    public string Name { get; set; }
    public int DurationMinutes { get; set; }
    public int TokenValue { get; set; }
    public decimal Price { get; set; }
    public string ServiceType { get; set; }
    public decimal CommissionPercent { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class Stationimage
{
    public int Id { get; set; }
    public int StationId { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class Appuser
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string? Location { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class Subscription
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public string PlanName { get; set; }
    public decimal MonthlyFee { get; set; }
    public string NextPaymentDate { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class Vehicle
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string LicensePlate { get; set; }
    public string VehicleType { get; set; }
    public string PhotoUrl { get; set; }
}

public class Token
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public int Amount { get; set; }
    public DateTime AcquiredAt { get; set; }
    public string Source { get; set; }
}

public class Booking
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CarWashStationId { get; set; }
    public int VehicleId { get; set; }
    public string BookingDate { get; set; }
    public string ArrivalTimeStart { get; set; }
    public string ArrivalTimeEnd { get; set; }
    public decimal TotalPrice { get; set; }
    public string Note { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class Bookingservice
{
    public int BookingId { get; set; }
    public int ServiceId { get; set; }
}

public class Bookingcommission
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public decimal CommissionPercent { get; set; }
    public decimal CommissionAmount { get; set; }
    public bool PaidToAdmin { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class Review
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class Reviewphoto
{
    public int Id { get; set; }
    public int ReviewId { get; set; }
    public string ImageUrl { get; set; }
}

public class Activitylog
{
    public int Id { get; set; }
    public int AdminId { get; set; }
    public string ActionType { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
}
