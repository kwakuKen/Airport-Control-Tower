using AirportControlTower.Domain.Entities;
using AirportControlTower.Domain.Events;
using AirportControlTower.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net.Security;

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

            var parkingDetails = new ParkingSpot();
            //get by type and isoccupied is false first or default
            //then update the record with the callsign
            if (notification.IsTakeOff)
                parkingDetails = await aircraftReadRepository.GetParkingSpotByCallSignAsync(notification.CallSign, cancellationToken);
            else
                parkingDetails = await aircraftReadRepository.GetParkingSpotByTypeAsyc(notification.Type, cancellationToken);
            if (parkingDetails is not null)
            {
                if (notification.IsTakeOff)
                    parkingDetails!.CallSign = null;
                else
                    parkingDetails!.CallSign = notification.CallSign;

                parkingDetails!.IsOccupied = notification.IsOccupied;
                await aircraftWriteRepository.UpdateParkingSportAsync(parkingDetails, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in ParkingSpotEventHandler");
        }
    }
}
