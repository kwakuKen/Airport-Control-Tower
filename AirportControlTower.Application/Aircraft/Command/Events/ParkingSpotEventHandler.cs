using AirportControlTower.Domain.Entities;
using AirportControlTower.Domain.Events;
using AirportControlTower.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AirportControlTower.Application.Aircraft.Command.Events;

public sealed class ParkingSpotEventHandler(
    IAircraftReadRepository aircraftReadRepository,
    IAircraftWriteRepository aircraftWriteRepository,
    ILogger<ParkingSpotEventHandler> logger)
    : INotificationHandler<ParkingSpotEvent>
{
    public async Task Handle(ParkingSpotEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var aircraftDetail = await aircraftReadRepository.GetAircraftByCallSignAsync(notification.CallSign, cancellationToken);
            if (aircraftDetail is null) return;

            var parkingDetails = await aircraftReadRepository.GetParkingSpotByCallSignAsync(notification.CallSign, cancellationToken);
            if (parkingDetails is null)
            {
                var newParkingSpot = new ParkingSpot
                {
                    Type = notification.Type,
                    CallSign = notification.CallSign,
                    IsOccupied = notification.IsOccupied
                };
                await aircraftWriteRepository.AddParkingSportAsync(newParkingSpot, cancellationToken);
            }
            else
            {
                parkingDetails.IsOccupied = notification.IsOccupied;
                await aircraftWriteRepository.UpdateParkingSportAsync(parkingDetails, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in ParkingSpotEventHandler");
        }
    }
}
