

using AirportControlTower.Domain.Entities;

namespace AirportControlTower.Application.Services;

public interface IFlightService
{
    Task<(bool, string)> RequestTakeOff(string callSign, CancellationToken cancellationToken);
    Task<(bool, FlightRequest, string)> RequestLanding(string callSign, CancellationToken cancellationToken);
    Task<Domain.Entities.Aircraft?> GetAirCraftByCallSign(string callSign, CancellationToken cancellationToken);
    Task<FlightRequest> AddFlightRequestAsync(FlightRequest request, CancellationToken cancellationToken);
    Task<int> UpdateFlightRequestAsync(FlightRequest request, CancellationToken cancellationToken);
    Task<(bool, FlightRequest, string)> ConfirmIntent(string callSign, string state, CancellationToken cancellationToken);
}
