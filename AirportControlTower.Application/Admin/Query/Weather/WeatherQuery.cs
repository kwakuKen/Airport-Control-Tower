using MediatR;

namespace AirportControlTower.Application.Admin.Query.Weather
{
    public sealed record WeatherQuery()
        :IRequest<object>;
}
