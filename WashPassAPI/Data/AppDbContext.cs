using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Models;

namespace WashPassAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
{

    public DbSet<AdminUser> AdminUsers { get; set; } = null!;
    public DbSet<AppUser> AppUsers { get; set; } = null!;
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
    public DbSet<CarWashStation> CarWashStations { get; set; } = null!;
    public DbSet<StationImage> StationImages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Add any custom model configurations here
        modelBuilder.Entity<Vehicle>()
       .HasOne(v => v.AppUser)
       .WithMany(u => u.Vehicles)
       .HasForeignKey(v => v.AppUserId)
       .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CarWashStation>()
       .HasOne(s => s.AdminUser)
       .WithMany()
       .HasForeignKey(s => s.AdminUserId)
       .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<StationImage>()
       .HasOne(i => i.Station)
       .WithMany(s => s.Images)
       .HasForeignKey(i => i.StationId)
       .OnDelete(DeleteBehavior.Cascade);
    }
}
