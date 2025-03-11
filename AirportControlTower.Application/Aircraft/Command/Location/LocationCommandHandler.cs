using AirportControlTower.Domain.Enums;
using AirportControlTower.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AirportControlTower.Application.Aircraft.Command.Location;

public sealed class LocationCommandHandler(
            IAircraftReadRepository aircraftReadRepository,
            IAircraftWriteRepository aircraftWriteRepository,
            ILogger<LocationCommandHandler> logger)
            : IRequestHandler<LocationCommand, int>
{
    public async Task<int> Handle(LocationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!ValidateRequest(request)) return -1;

            var flightRequest = await aircraftReadRepository.GetLastFlightRequestAsync(request.CallSign, cancellationToken)
                           ?? throw new ArgumentNullException("Flight request not found");
            if (flightRequest is null) return -2;

            flightRequest.Latitude = request.Latitude;
            flightRequest.Longitude = request.Longitude;
            flightRequest.Altitude = request.Altitude;
            flightRequest.Heading = request.Heading;
            flightRequest.Type = request.Type;

            return await aircraftWriteRepository.UpdateFlightRequestAsync(flightRequest, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error >>>> {ex.Message}");
        }
        return default!;
    }

    private static bool ValidateRequest(LocationCommand request)
    {
        if (request is null) return false;
        if (!int.TryParse(request.Altitude.ToString(), out _)) return false;
        if (!int.TryParse(request.Heading.ToString(), out _)) return false;
        if (!decimal.TryParse(request.Latitude.ToString(), out _)) return false;
        if (!decimal.TryParse(request.Longitude.ToString(), out _)) return false;
        if (string.IsNullOrEmpty(request.CallSign)) return false;
        if (!Enum.TryParse(typeof(AircraftType), request.Type, true, out _)) return false;
        return true;
    }
}
