using AirportControlTower.Domain.Entities;
using AirportControlTower.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AirportControlTower.Infrastructure.BackgroundJobs;

public class GetWeatherUpdateJob : BackgroundService
{
    private readonly ILogger<GetWeatherUpdateJob> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private bool _isJobRunning;
    private readonly object _lock = new();

    public GetWeatherUpdateJob(ILogger<GetWeatherUpdateJob> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            lock (_lock)
            {
                if (_isJobRunning) return; // Prevent multiple executions
                _isJobRunning = true;
            }

            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var weatherWriteRepository = scope.ServiceProvider.GetRequiredService<IWeatherWriteRepository>();

                    var response = await WeatherClient.GetWeatherAsync("Accra");
                    if (response is not null)
                    {
                        await weatherWriteRepository.AddWeatherAsync(
                            new Weather
                            {
                                CreatedAt = DateTime.UtcNow,
                                Description = response.weather[0].description,
                                Visibility = response.visibility,
                                Temperature = response.main.temp,
                                WindSpeed = response.wind.speed,
                                WindDirection = response.wind.deg
                            },
                            stoppingToken
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while executing the job: {ex.Message}");
            }
            finally
            {
                lock (_lock)
                {
                    _isJobRunning = false;
                }
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}
