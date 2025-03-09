using AirportControlTower.Domain.Entities;
using AirportControlTower.Domain.Enums;
using AirportControlTower.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirportControlTower.Infrastructure.Repositories;

class GroundCrewRepository(AirportControlTowerDbContext _context)
    : IGroundCrewRepository
{

    public async Task<List<FlightRequest>> GetAllLandedAircraftAsync(CancellationToken cancellationToken)
    {
        return await _context.FlightRequest
            .Where(x => x.State == AircraftState.LANDED.ToString())
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateFlightRequestStatusAsync(FlightRequest[] flightRequest, CancellationToken cancellationToken)
    {
        _context.FlightRequest.UpdateRange(flightRequest);
        await _context.SaveChangesAsync(cancellationToken);
    }

}
