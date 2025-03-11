using AirportControlTower.Application.Services;
using AirportControlTower.Domain.Entities;
using AirportControlTower.Domain.Enums;
using AirportControlTower.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AirportControlTower.Application.Aircraft.Command.Intent;

public sealed class IntentCommandHandler : IRequestHandler<IntentCommand, int>
{
    private readonly ILogger<IntentCommandHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IFlightService _flightService;

    public IntentCommandHandler(
        ILogger<IntentCommandHandler> logger,
        IMediator mediator,
        IFlightService flightService)
    {
        _logger = logger;
        _mediator = mediator;
        _flightService = flightService;
    }

    public async Task<int> Handle(IntentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!ValidateRequest(request)) return -1;

            if (request.State == AircraftState.PARKED.ToString())
            {
                await _mediator.Publish(new FlightLogEvent(
                          CallSign: request.CallSign,
                          State: request.State,
                          Reason: $"Aircraft with callsign {request.CallSign} request to park. Aircraft cannot request to park",
                          IsAccepted: false
                      ), cancellationToken);

                return -2;
            }

            if (request.State == AircraftState.TAKEOFF.ToString())
            {
                var aircraftResult = await _flightService.GetAirCraftByCallSign(request.CallSign, cancellationToken);
                if (aircraftResult is null) return -1;

                var response = await _flightService.RequestTakeOff(request.CallSign, cancellationToken);

                if (response.Item1)
                {
                    var flighRequst = new FlightRequest()
                    {
                        AircraftId = aircraftResult.Id,
                        CreatedAt = DateTime.UtcNow,
                        CallSign = request.CallSign,
                        State = AircraftState.TAKEOFF.ToString(),
                        Type = aircraftResult.Type!
                    };

                    await _flightService.AddFlightRequestAsync(flighRequst, cancellationToken);
                }

                await _mediator.Publish(new FlightLogEvent(
                           CallSign: request.CallSign,
                           State: request.State,
                           Reason: response.Item2,
                           IsAccepted: response.Item1
                       ), cancellationToken);

                return 1;
            }
            else if (request.State == AircraftState.APPROACH.ToString())
            {
                var response = await _flightService.RequestLanding(request.CallSign, cancellationToken);

                if (response.Item1)
                {
                    response.Item2.State = AircraftState.APPROACH.ToString();
                    await _flightService.UpdateFlightRequestAsync(response.Item2, cancellationToken);
                }

                await _mediator.Publish(new FlightLogEvent(
                           CallSign: request.CallSign,
                           State: request.State,
                           Reason: response.Item3,
                           IsAccepted: response.Item1
                       ), cancellationToken);

                return 1;
            }
            else
            {
                var resultValue = -2;
                var response = await _flightService.ConfirmIntent(request.CallSign, request.State, cancellationToken);
                if (response.Item1)
                {
                    await _flightService.UpdateFlightRequestAsync(response.Item2, cancellationToken);
                    resultValue = 1;
                }

                    await _mediator.Publish(new FlightLogEvent(
                              CallSign: request.CallSign,
                              State: request.State,
                              Reason: response.Item3,
                              IsAccepted: response.Item1
                          ), cancellationToken);

                return resultValue;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error >>>> {ex.Message}");
        }
        return default!;
    }

    private static bool ValidateRequest(IntentCommand request)
    {
        if (request is null) return false;
        if (string.IsNullOrEmpty(request.CallSign)) return false;
        if (!Enum.TryParse(typeof(AircraftState), request.State, true, out _)) return false;
        return true;
    }
}
