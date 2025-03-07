using AirportControlTower.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace AirportControlTower.Infrastructure;

public class AirportControlTowerDbContext : DbContext
{
    public DbSet<Aircraft> Aircrafts { get; set; }
    public DbSet<FlightLogs> FlightLogs { get; set; }
    public DbSet<ParkingSpot> ParkingSpots { get; set; }
    public DbSet<Weather> WeatherRecords { get; set; }

    public AirportControlTowerDbContext(DbContextOptions<AirportControlTowerDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aircraft>().HasIndex(a => a.CallSign).IsUnique();
        modelBuilder.Entity<ParkingSpot>().HasIndex(p => p.Type);
    }
}
