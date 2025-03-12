using AirportControlTower.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AirportControlTower.Application.Admin.Query.LastTenFlightLogs;

internal sealed class FlightLogsRequestHandler(
    IAdminReadRepository adminReadRepository,
    ILogger<FlightLogsRequestHandler> logger)
    : IRequestHandler<FlightLogsQuery, IEnumerable<object>>
{
    public async Task<IEnumerable<object>> Handle(FlightLogsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await adminReadRepository.GetAllFlightLogsAsync(cancellationToken);
            if (result == null) return [];
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in FlightLogsRequestHandler");
        }
        return [];
    }
}
