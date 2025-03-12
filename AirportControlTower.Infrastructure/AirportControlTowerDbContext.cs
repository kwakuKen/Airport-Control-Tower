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
    public DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aircraft>()
            .HasIndex(a => a.CallSign)
            .IsUnique();

        modelBuilder.Entity<Aircraft>()
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<Users>()
           .Property(e => e.CreatedAt)
           .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<FlightLogs>()
            .HasOne<Aircraft>()
            .WithMany(a => a.FlightLogs)
            .HasForeignKey(fl => fl.CallSign)
            .HasPrincipalKey(a => a.CallSign);

        DataSeeder.SeedData(modelBuilder);
    }

    public async Task SeedParkingSpotsAsync()
    {
        try
        {
            var privateCount = await ParkingSpots.CountAsync(p => p.Type == "PRIVATE");
            var airlinerCount = await ParkingSpots.CountAsync(p => p.Type == "AIRLINER");

            int airliner_spot = int.Parse(Environment.GetEnvironmentVariable("MAXIMUM_AIRLINER_SPOT")!);
            int private_spot = int.Parse(Environment.GetEnvironmentVariable("MAXIMUM_PRIVATE_SPOT")!);

            var newSpots = new List<ParkingSpot>();


            if (privateCount < private_spot)
            {
                for (int i = privateCount + 1; i <= private_spot; i++)
                {
                    newSpots.Add(new ParkingSpot
                    {
                        Type = "PRIVATE",
                        IsOccupied = false,
                    });
                }
            }
            else if (privateCount > private_spot)
            {
                var excessPrivateSpots = await ParkingSpots
                    .Where(p => p.Type == "PRIVATE" && !p.IsOccupied)
                    .OrderByDescending(p => p.Id)
                    .Take(privateCount - private_spot)
                    .ToListAsync();

                ParkingSpots.RemoveRange(excessPrivateSpots);
            }

            if (airlinerCount < airliner_spot)
            {
                for (int i = airlinerCount + 1; i <= airliner_spot; i++)
                {
                    newSpots.Add(new ParkingSpot
                    {
                        Type = "AIRLINER",
                        IsOccupied = false,
                    });
                }
            }

            else if (airlinerCount > airliner_spot)
            {
                var excessAirlinerSpots = await ParkingSpots
                    .Where(p => p.Type == "AIRLINER" && !p.IsOccupied)
                    .OrderByDescending(p => p.Id)
                    .Take(airlinerCount - airliner_spot)
                    .ToListAsync();

                ParkingSpots.RemoveRange(excessAirlinerSpots);
            }

            if (newSpots.Any())
            {
                await ParkingSpots.AddRangeAsync(newSpots);
            }

            await SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

}
