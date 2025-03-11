using AirportControlTower.Domain.Entities;
using AirportControlTower.Domain.Events;
using AirportControlTower.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AirportControlTower.Application.Aircraft.Command.Events;

public class FlightLogEventHandler(ILogger<FlightLogEventHandler> _logger, 
    IAircraftWriteRepository _aircraftWriteRepository) : INotificationHandler<FlightLogEvent>
{
    public async Task Handle(FlightLogEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var flightLog = new FlightLogs
            {
                CallSign = notification.CallSign,
                State = notification.State,
                IsAccepted = notification.IsAccepted,
                Reason = notification.Reason,
                CreatedAt = DateTime.UtcNow,
            };

            await _aircraftWriteRepository.AddFlightLogsAsync(flightLog, cancellationToken);

            _logger.LogInformation($"New FlightLog added for {notification.CallSign}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error inserting FlightLog: {ex.Message}");
        }
    }
}
