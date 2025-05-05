using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.Models;

namespace WashPassAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
{

    public DbSet<AdminUser> AdminUsers { get; set; } = null!;
    public DbSet<AppUser> AppUsers { get; set; } = null!;
    public DbSet<Vehicle> Vehicles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Add any custom model configurations here
        modelBuilder.Entity<Vehicle>()
       .HasOne(v => v.AppUser)
       .WithMany(u => u.Vehicles)
       .HasForeignKey(v => v.AppUserId)
       .OnDelete(DeleteBehavior.Cascade);
    }
}
