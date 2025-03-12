using MediatR;

namespace AirportControlTower.Application.Admin.Query.LastTenFlightLogs;

public sealed record FlightLogsQuery()
    :IRequest<IEnumerable<object>>;
