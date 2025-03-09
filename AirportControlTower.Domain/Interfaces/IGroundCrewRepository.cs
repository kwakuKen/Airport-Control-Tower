using AirportControlTower.Domain.Entities;

namespace AirportControlTower.Domain.Interfaces;

public interface IGroundCrewRepository
{
    Task<List<FlightRequest>> GetAllLandedAircraftAsync(CancellationToken cancellationToken);
    Task UpdateFlightRequestStatusAsync(FlightRequest[] flightRequest, CancellationToken cancellationToken);
}
