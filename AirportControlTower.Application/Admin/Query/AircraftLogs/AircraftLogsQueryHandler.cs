using AirportControlTower.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AirportControlTower.Application.Admin.Query.AircraftLogs;

internal sealed class AircraftLogsQueryHandler(
    IAdminReadRepository adminReadRepository,
    ILogger<AircraftLogsQueryHandler> logger)
    : IRequestHandler<AircraftLogsQuery, IEnumerable<object>>

{
    public async Task<IEnumerable<object>> Handle(AircraftLogsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await adminReadRepository.GetAllAircraftWithLogs(cancellationToken);
            if (result == null || !result.Any()) return [];
            return result;

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occured in AircraftLogsQueryHandler");
        }
        return [];
    }
}
