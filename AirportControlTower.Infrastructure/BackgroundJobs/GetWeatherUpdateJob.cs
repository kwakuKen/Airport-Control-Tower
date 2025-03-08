using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AirportControlTower.Infrastructure.BackgroundJobs;

public class GetWeatherUpdateJob(ILogger<GetWeatherUpdateJob> _logger,
    bool _isJobRunning = false)
    : BackgroundService
{
    private readonly object _lock = new object();
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (!_isJobRunning)
            {
                try
                {
                    lock (_lock)
                    {
                        _isJobRunning = true;
                    }
                    //fetch weather details
                    _logger.LogInformation("Getting weather details");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occured while executin the job >>>>> {ex.Message}");
                }
                finally
                {
                    lock (_lock)
                    {
                        _isJobRunning = false;
                    }
                }
            }
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}
