using AirportControlTower.Domain.Entities;
using AirportControlTower.Domain.Enums;
using AirportControlTower.Domain.Interfaces;

namespace AirportControlTower.Application.Services;

public class FlightService(
    IAircraftReadRepository aircraftReadRepository,
    IAircraftWriteRepository aircraftWriteRepository
    ) : IFlightService
{
    public async Task<(bool, string)> RequestTakeOff(string callSign, CancellationToken cancellationToken)
    {
        var aircraftRequests = await aircraftReadRepository.GetAllFlightRequstAsync(cancellationToken);

        if (aircraftRequests == null || aircraftRequests.Count == 0)
        {
            return (true, $"Aircraft with call sign {callSign} is allowed to take off");
        }

        if (aircraftRequests.Any(f => f.State == AircraftState.TAKEOFF.ToString() || f.State == AircraftState.LANDED.ToString()))
        {
            return (false, "Runway is occupied");
        }

        var lastAircraftRequest = aircraftRequests
            .Where(f => f.CallSign == callSign)
            .OrderByDescending(f => f.Id)
            .FirstOrDefault();

        if (lastAircraftRequest == null || lastAircraftRequest.State == "PARKED")
        {
            return (true, $"Aircraft with call sign {callSign} is allowed to take off");
        }

        return (false, $"Aircraft with call sign {callSign} is not parked");
    }

    public async Task<(bool, FlightRequest, string)> RequestLanding(string callSign, CancellationToken cancellationToken)
    {
        var aircraftRequests = await aircraftReadRepository.GetAllFlightRequstAsync(cancellationToken);

        if (aircraftRequests == null || aircraftRequests.Count == 0)
        {
            return (false, default!, $"Aircraft with call sign {callSign} cannot land. because it might be in a parked state");
        }

        if (aircraftRequests.Any(f => f.State == AircraftState.APPROACH.ToString() ||
                                      f.State == AircraftState.TAKEOFF.ToString() ||
                                      f.State == AircraftState.LANDED.ToString()))
        {
            return (false, default!, "Runway is occupied");
        }

        var lastAircraftRequest = aircraftRequests
            .Where(f => f.CallSign == callSign)
            .OrderByDescending(f => f.Id)
            .FirstOrDefault();

        if (lastAircraftRequest == null || lastAircraftRequest.State != AircraftState.AIRBORNE.ToString())
        {
            return (false, default!, $"Aircraft with call sign {callSign} is not airborne");
        }

        int availableSpots = await aircraftReadRepository.GetAvailableParkingSpotAsync(lastAircraftRequest.Type!, cancellationToken);

        if (availableSpots < 1)
        {
            return (false, default!, "No available parking");
        }

        return (true, lastAircraftRequest, $"Aircraft with call sign {callSign} is allowed to land");
    }

    public async Task<(bool, FlightRequest, string)> ConfirmIntent(string callSign, string state, CancellationToken cancellationToken)
    {
        var result = await aircraftReadRepository.GetLastFlightRequestAsync(callSign, cancellationToken);
        if (result is null)
        {
            return (false, default!, $"Aircraft can not request {state.ToUpper()} at the moment");
        }
        if (state == AircraftState.AIRBORNE.ToString())
        {
            if (result.State == AircraftState.TAKEOFF.ToString())
            {
                result.State = AircraftState.AIRBORNE.ToString();
                return (true, result, $"state changed from {AircraftState.TAKEOFF} to {AircraftState.AIRBORNE}");
            }
            else
            {
                return (false, result, $"state changed rejected because aircraft is not in {AircraftState.TAKEOFF} state");
            }
        }
        else if (state == AircraftState.LANDED.ToString())
        {
            if (result.State == AircraftState.APPROACH.ToString())
            {
                result.State = AircraftState.LANDED.ToString();
                return (true, result, $"state changed from {AircraftState.APPROACH} to {AircraftState.LANDED}");
            }
            else
            {
                return (false, result, $"state changed rejected because aircraft is not in {AircraftState.APPROACH} state");
            }
        }
        else
            return (false, result, $"Invalid Intent State. {state}");



    }
    public async Task<Domain.Entities.Aircraft?> GetAirCraftByCallSign(string callSign, CancellationToken cancellationToken)
    {
        return await aircraftReadRepository.GetAircraftByCallSignAsync(callSign, cancellationToken);
    }

    public async Task<FlightRequest> AddFlightRequestAsync(FlightRequest request, CancellationToken cancellationToken)
    {
        return await aircraftWriteRepository.AddFlightRequestAsync(request, cancellationToken);
    }

    public async Task<int> UpdateFlightRequestAsync(FlightRequest request, CancellationToken cancellationToken)
    {
        return await aircraftWriteRepository.UpdateFlightRequestAsync(request, cancellationToken);
    }
}
