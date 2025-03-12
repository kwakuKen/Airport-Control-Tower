using MediatR;

namespace AirportControlTower.Application.Admin.Query.ParkingSpot;

public sealed record ParkingSpotQuery()
    :IRequest<IEnumerable<object>>;
