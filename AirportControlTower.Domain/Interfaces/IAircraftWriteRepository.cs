using AirportControlTower.Domain.Entities;

namespace AirportControlTower.Domain.Interfaces;

public interface IAircraftWriteRepository
{
    Task AddFlightLogsAsync(FlightLogs flightLogs, CancellationToken cancellationToken);
    Task<int> UpdateFlightRequestAsync(FlightRequest flightRequest, CancellationToken cancellationToken);
}
