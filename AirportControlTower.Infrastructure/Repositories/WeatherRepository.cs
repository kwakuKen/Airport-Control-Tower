using AirportControlTower.Domain.Entities;
using AirportControlTower.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirportControlTower.Infrastructure.Repositories;

public class WeatherRepository(AirportControlTowerDbContext _context) 
    : IWeatherReadRepository, 
    IWeatherWriteRepository
{
    public async Task AddWeatherAsync(Weather weather)
    {
        await _context.WeatherRecords.AddAsync(weather);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Weather>> GetAllWeatherAsync()
    {
        return await _context.WeatherRecords.ToListAsync();
    }

    public async Task<Weather?> GetWeatherByIdAsync(int id)
    {
        return await _context.WeatherRecords.FindAsync(id);
    }
}
