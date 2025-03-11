using AirportControlTower.Domain.Entities;
using AirportControlTower.Domain.Enums;
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
            .FindAsync([id, cancellationToken], cancellationToken: cancellationToken);
    }

    public async Task<FlightRequest?> GetLastFlightRequestAsync(string callSign, CancellationToken cancellationToken)
    {
        return await _context.FlightRequest
            .Where(FlightRequest => FlightRequest.CallSign == callSign)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> UpdateFlightRequestAsync(FlightRequest flightRequest, CancellationToken cancellationToken)
    {
        _context.FlightRequest.Update(flightRequest);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<FlightRequest>> GetAllFlightRequstAsync(CancellationToken cancellationToken)
    {
        //completed cycle because i need all request that is not i a park state
        ///when isCompletedCyle is true it means that the plan is in a park state
        return await _context.FlightRequest
            .Where(FlightRequest => FlightRequest.IsCompleteCycle == false) 
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetParkedAircraftCountAsync(string type, CancellationToken cancellationToken)
    {
        return await _context.FlightRequest
            .CountAsync(FlightRequest => FlightRequest.Type == type &&
            FlightRequest.State == AircraftState.PARKED.ToString() &&
            FlightRequest.IsCompleteCycle == false, cancellationToken);
    }

    public async Task<Aircraft?> GetAircraftByCallSignAsync(string callSign, CancellationToken cancellationToken)
    {
        return await _context.Aircrafts
            .Where(o => o.CallSign == callSign)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<FlightRequest> AddFlightRequestAsync(FlightRequest flightRequest, CancellationToken cancellationToken)
    {
        await _context.FlightRequest.AddAsync(flightRequest, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return flightRequest;
    }
   
    public async Task<ParkingSpot?> GetParkingSpotByCallSignAsync(string callSign, CancellationToken cancellationToken)
    {
        return await _context.ParkingSpots
            .Where(o => o.CallSign == callSign)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ParkingSpot> AddParkingSportAsync(ParkingSpot parkingSpot, CancellationToken cancellationToken)
    {
        await _context.ParkingSpots.AddAsync(parkingSpot, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return parkingSpot;
    }

    public async Task<ParkingSpot> UpdateParkingSportAsync(ParkingSpot parkingSpot, CancellationToken cancellationToken)
    {
         _context.ParkingSpots.Update(parkingSpot);
        await _context.SaveChangesAsync(cancellationToken);
        return parkingSpot;
    }
}