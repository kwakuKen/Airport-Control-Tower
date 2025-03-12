using AirportControlTower.Domain.Entities;

namespace AirportControlTower.Domain.Interfaces;

public interface IAdminReadRepository
{
    Task<IEnumerable<object>> GetAllAircraftWithLastFlightDataAsync(CancellationToken cancellationToken);
    Task<IEnumerable<object>> GetAllAircraftWithLogs(CancellationToken cancellationToken);
    Task<IEnumerable<FlightLogs>> GetAllFlightLogsAsync(CancellationToken cancellationToken);
    Task<Weather?> GetCurrentWeatherAsync(CancellationToken cancellationToken);
    Task<IEnumerable<ParkingSpot>> GetParkingSpotAsync(CancellationToken cancellationToken);
}
