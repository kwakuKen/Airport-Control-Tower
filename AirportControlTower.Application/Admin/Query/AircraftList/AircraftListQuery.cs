using MediatR;

namespace AirportControlTower.Application.Admin.Query.AircraftList;

public sealed record AircraftListQuery()
    : IRequest<IEnumerable<object>>;

