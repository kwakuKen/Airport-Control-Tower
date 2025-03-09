using AirportControlTower.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml;

namespace AirportControlTower.Infrastructure;

public class AirportControlTowerDbContext
    : DbContext
{
    private readonly DateTime _createdOn = DateTime.UtcNow;
    public AirportControlTowerDbContext(DbContextOptions<AirportControlTowerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Aircraft> Aircrafts { get; set; }
    public DbSet<FlightLogs> FlightLogs { get; set; }
    public DbSet<FlightRequest> FlightRequest { get; set; }
    public DbSet<Weather> WeatherRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aircraft>()
            .HasIndex(a => a.CallSign)
            .IsUnique();

        modelBuilder.Entity<Aircraft>()
        .Property(e => e.CreatedAt)
        .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<FlightLogs>()
            .HasOne(a => a.FlightRequest)
            .WithMany(fr => fr.FlightLogs)
            .HasForeignKey(a => a.FlightRequstId)
            .OnDelete(DeleteBehavior.Cascade);

        DataSeeder.SeedData(modelBuilder);
    }
}
