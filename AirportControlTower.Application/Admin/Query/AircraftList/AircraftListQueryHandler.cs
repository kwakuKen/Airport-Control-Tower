using AirportControlTower.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AirportControlTower.Application.Admin.Query.AircraftList;

internal sealed class AircraftListQueryHandler(IAdminReadRepository adminReadRepository, ILogger<AircraftListQueryHandler> logger)
    : IRequestHandler<AircraftListQuery, IEnumerable<object>>
{
    public async Task<IEnumerable<object>> Handle(AircraftListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await adminReadRepository.GetAllAircraftWithLastFlightDataAsync(cancellationToken);
            if (result is null) return [];

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching aircraft list");
        }
        return [];
    }
}
