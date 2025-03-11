using AirportControlTower.Domain.Enums;
using AirportControlTower.Domain.Events;
using AirportControlTower.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AirportControlTower.Application.BackgroundJobs;

public class GroundCrewJob : BackgroundService
{
    private readonly ILogger<GroundCrewJob> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private bool _isJobRunning;
    private readonly object _lock = new();

    public GroundCrewJob(ILogger<GroundCrewJob> logger,
        IServiceScopeFactory serviceScopeFactory)
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
                using var scope = _serviceScopeFactory.CreateScope();
                var groundCrewReadRepository = scope.ServiceProvider.GetRequiredService<IGroundCrewRepository>();

                var response = await groundCrewReadRepository.GetAllLandedAircraftAsync(stoppingToken);

                if (response != null && response.Count > 0)
                {
                    _logger.LogInformation($"Ground crew has been notified of {response.Count} landed aircrafts.");

                    foreach (var flightRequest in response)
                    {
                        flightRequest.State = AircraftState.PARKED.ToString();
                        flightRequest.IsCompleteCycle = true;
                    }

                    await groundCrewReadRepository.UpdateFlightRequestStatusAsync([.. response], stoppingToken);

                    using var scope1 = _serviceScopeFactory.CreateScope();
                    var publisher = scope1.ServiceProvider.GetRequiredService<IPublisher>();
                    await Task.WhenAll(response.Select(o =>
                        publisher.Publish(new FlightLogEvent(
                            CallSign: o.CallSign,
                            State: AircraftState.PARKED.ToString(),
                            Reason: "Cleared the runway",
                            IsAccepted: true
                        ), stoppingToken)
                    ));


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

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}

