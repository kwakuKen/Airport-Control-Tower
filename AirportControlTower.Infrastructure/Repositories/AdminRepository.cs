using AirportControlTower.Domain.Entities;
using AirportControlTower.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirportControlTower.Infrastructure.Repositories;

public class AdminRepository(AirportControlTowerDbContext context) : IAdminReadRepository
{
    private readonly AirportControlTowerDbContext _context = context;

    public async Task<IEnumerable<object>> GetAllAircraftWithLastFlightDataAsync(CancellationToken cancellationToken)
    {
        var query = from aircraft in _context.Aircrafts
                    join latestFlight in _context.FlightRequest
                        .Where(fr => fr.CreatedAt == _context.FlightRequest
                            .Where(x => x.CallSign == fr.CallSign)
                            .Max(x => x.CreatedAt))
                    on aircraft.CallSign equals latestFlight.CallSign
                    select new
                    {
                        aircraft.CallSign,
                        aircraft.Type,
                        latestFlight.State,
                        latestFlight.Latitude,
                        latestFlight.Longitude,
                        latestFlight.Altitude,
                        latestFlight.Heading,
                        latestFlight.CreatedAt
                    };


        return await query.ToListAsync(cancellationToken);



    }

    public async Task<IEnumerable<object>> GetAllAircraftWithLogs(CancellationToken cancellationToken)
    {
        var groupedLogs = await _context.FlightLogs
        .OrderByDescending(fl => fl.CreatedAt)
        .GroupBy(fl => fl.CallSign)
        .Select(group => new
        {
            CallSign = group.Key,
            Logs = group.Select(fl => new
            {
                fl.State,
                Outcome = fl.IsAccepted ? "ACCEPTED" : "REJECTED",
                fl.Reason,
                fl.CreatedAt
            }).ToList()
        })
        .ToListAsync(cancellationToken);

        return groupedLogs;
    }
    public async Task<Weather?> GetCurrentWeatherAsync(CancellationToken cancellationToken)
    {
        return await _context.WeatherRecords
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<IEnumerable<FlightLogs>> GetAllFlightLogsAsync(CancellationToken cancellationToken)
    {
        return await _context.FlightLogs
            .OrderByDescending (fl => fl.CreatedAt)
            .Take(10)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ParkingSpot>> GetParkingSpotAsync(CancellationToken cancellationToken)
    {
        return await _context.ParkingSpots
            .ToListAsync(cancellationToken);
    }
}
