using AirportControlTower.Domain.Entities;

namespace AirportControlTower.Domain.Interfaces;

public interface IWeatherWriteRepository
{
    Task AddWeatherAsync(Weather weather);
}
