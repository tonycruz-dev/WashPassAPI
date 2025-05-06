using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Models;

namespace WashPassAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
{

    public DbSet<AdminAccount> AdminAccounts { get; set; } = null!;
    public DbSet<AppUser> AppUsers { get; set; } = null!;
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
    public DbSet<CarWashStation> CarWashStations { get; set; } = null!;
    public DbSet<StationImage> StationImages { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;
    public DbSet<BookingService> BookingServices { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<ReviewPhoto> ReviewPhotos { get; set; } = null!;
    public DbSet<Subscription> Subscriptions { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Add any custom model configurations here
        // Vehicle - AppUser (One-to-Many)
        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.AppUser)
            .WithMany(u => u.Vehicles)
            .HasForeignKey(v => v.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        // CarWashStation - AdminUser (One-to-Many or One-to-One depending on design)
        modelBuilder.Entity<CarWashStation>()
            .HasOne(s => s.AdminAccount)
            .WithMany(a => a.CarWashStations) 
            .HasForeignKey(s => s.AdminId)
            .OnDelete(DeleteBehavior.Cascade);

        // StationImage - CarWashStation (One-to-Many)
        modelBuilder.Entity<StationImage>()
            .HasOne(i => i.Station)
            .WithMany(s => s.Images)
            .HasForeignKey(i => i.StationId)
            .OnDelete(DeleteBehavior.Cascade);

        // Service - CarWashStation (One-to-Many)
        modelBuilder.Entity<Service>()
            .HasOne(s => s.Station)
            .WithMany(st => st.Services)
            .HasForeignKey(s => s.CarWashStationId)
            .OnDelete(DeleteBehavior.Cascade);

        // Booking - User
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Booking - Station
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Station)
            .WithMany(s => s.Bookings)
            .HasForeignKey(b => b.CarWashStationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Booking>()
        .HasOne(b => b.Review)
        .WithOne(r => r.Booking)
        .HasForeignKey<Review>(r => r.BookingId)
        .OnDelete(DeleteBehavior.Cascade);

        // Booking - Vehicle
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Vehicle)
            .WithMany(v => v.Bookings)
            .HasForeignKey(b => b.VehicleId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete if vehicle is deleted

        modelBuilder.Entity<BookingService>(x => x.HasKey(bs => new { bs.BookingId, bs.ServiceId }));
        
        //// BookingService (Many-to-Many)
        modelBuilder.Entity<BookingService>()
            .HasOne(bs => bs.Booking)
            .WithMany(b => b.BookingServices)
            .HasForeignKey(bs => bs.BookingId)
            .OnDelete(DeleteBehavior.NoAction);


        modelBuilder.Entity<BookingService>()
            .HasOne(bs => bs.Service)
            .WithMany(s => s.BookingServices)
            .HasForeignKey(bs => bs.ServiceId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ReviewPhoto>()
            .HasOne(p => p.Review)
            .WithMany(r => r.Photos)
            .HasForeignKey(p => p.ReviewId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.User)
            .WithOne(u => u.Subscription)
            .HasForeignKey<Subscription>(s => s.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
