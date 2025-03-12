using MediatR;

namespace AirportControlTower.Application.Admin.Query.AircraftLogs;

public sealed record AircraftLogsQuery()
    :IRequest<IEnumerable<object>>;