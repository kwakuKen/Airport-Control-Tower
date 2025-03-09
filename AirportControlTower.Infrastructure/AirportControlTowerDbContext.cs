using AirportControlTower.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace AirportControlTower.Infrastructure;

public class AirportControlTowerDbContext(DbContextOptions<AirportControlTowerDbContext> options) : DbContext(options)
{
    public DbSet<Aircraft> Aircrafts { get; set; }
    public DbSet<FlightLogs> FlightLogs { get; set; }
    public DbSet<ParkingSpot> ParkingSpots { get; set; }
    public DbSet<Weather> WeatherRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aircraft>().HasIndex(a => a.CallSign).IsUnique();
        modelBuilder.Entity<ParkingSpot>().HasIndex(p => p.Type);
    }
}
