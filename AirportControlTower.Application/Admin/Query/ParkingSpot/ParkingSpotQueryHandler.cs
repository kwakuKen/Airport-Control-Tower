using AirportControlTower.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AirportControlTower.Application.Admin.Query.ParkingSpot
{
    internal sealed class ParkingSpotQueryHandler(
        IAdminReadRepository adminReadRepository,
        ILogger<ParkingSpotQueryHandler> logger)
        : IRequestHandler<ParkingSpotQuery, IEnumerable<object>>
    {
        public async Task<IEnumerable<object>> Handle(ParkingSpotQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await adminReadRepository.GetParkingSpotAsync(cancellationToken);
                if (result is null) return [];
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in ParkingSpotQueryHandler");
            }
            return [];
        }
    }
}
