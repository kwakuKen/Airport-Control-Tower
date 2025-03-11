using AirportControlTower.Domain.Entities;

namespace AirportControlTower.Domain.Interfaces;

public interface IAircraftReadRepository
{

    Task<List<FlightLogs>> GetAllFlightLogsAsync(CancellationToken cancellationToken);
    Task<FlightLogs?> GetCurrentFlightLogsAsync(CancellationToken cancellationToken);
    Task<FlightLogs?> GetFlightLogsByIdAsync(int id, CancellationToken cancellationToken);
    Task<FlightRequest?> GetLastFlightRequestAsync(string callSign, CancellationToken cancellationToken);
    Task<List<FlightRequest>> GetAllFlightRequstAsync(CancellationToken cancellationToken);
    Task<int> GetParkedAircraftCountAsync(string type, CancellationToken cancellationToken);
    Task<Aircraft?> GetAircraftByCallSignAsync(string callSign, CancellationToken cancellationToken);
    Task<ParkingSpot?> GetParkingSpotByCallSignAsync(string callSign, CancellationToken cancellationToken);
}
