using AirportControlTower.Domain.Entities;
using AirportControlTower.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirportControlTower.Infrastructure.Repositories;

public class WeatherRepository(AirportControlTowerDbContext _context) 
    : IWeatherReadRepository, 
    IWeatherWriteRepository
{
    public async Task AddWeatherAsync(Weather weather, CancellationToken cancellationToken)
    {
        await _context.WeatherRecords.AddAsync(weather, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Weather>> GetAllWeatherAsync(CancellationToken cancellationToken)
    {
        return await _context.WeatherRecords.ToListAsync(cancellationToken);
    }

    public async Task<Weather?> GetWeatherByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.WeatherRecords.FindAsync(id, cancellationToken);
    }
}
