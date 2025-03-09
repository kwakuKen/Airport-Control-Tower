using AirportControlTower.Domain.Entities;
using AirportControlTower.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirportControlTower.Infrastructure.Repositories;

public class AircraftRepository(AirportControlTowerDbContext _context)
    : IAircraftReadRepository,
    IAircraftWriteRepository
{
    public async Task AddFlightLogsAsync(FlightLogs flightLogs, CancellationToken cancellationToken)
    {
        await _context.FlightLogs.AddAsync(flightLogs, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<FlightLogs>> GetAllFlightLogsAsync(CancellationToken cancellationToken)
    {
        return await _context.FlightLogs
            .ToListAsync(cancellationToken);
    }

    public async Task<FlightLogs?> GetCurrentFlightLogsAsync(CancellationToken cancellationToken)
    {
        return await _context.FlightLogs
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<FlightLogs?> GetFlightLogsByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.FlightLogs
            .FindAsync(id, cancellationToken);
    }
}