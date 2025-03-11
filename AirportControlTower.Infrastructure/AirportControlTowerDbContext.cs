using AirportControlTower.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirportControlTower.Infrastructure;

public class AirportControlTowerDbContext(DbContextOptions<AirportControlTowerDbContext> options)
        : DbContext(options)
{
    public DbSet<Aircraft> Aircrafts { get; set; }
    public DbSet<FlightLogs> FlightLogs { get; set; }
    public DbSet<FlightRequest> FlightRequest { get; set; }
    public DbSet<Weather> WeatherRecords { get; set; }
    public DbSet<ParkingSpot> ParkingSpots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aircraft>()
            .HasIndex(a => a.CallSign)
            .IsUnique();

        modelBuilder.Entity<Aircraft>()
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<FlightLogs>()
            .HasOne<Aircraft>()
            .WithMany(a => a.FlightLogs)
            .HasForeignKey(fl => fl.CallSign)
            .HasPrincipalKey(a => a.CallSign);

        modelBuilder.Entity<ParkingSpot>()
            .HasOne<Aircraft>()
            .WithOne()
            .HasForeignKey<ParkingSpot>(ps => ps.CallSign)
            .HasPrincipalKey<Aircraft>(a => a.CallSign);

        DataSeeder.SeedData(modelBuilder);
    }
}
