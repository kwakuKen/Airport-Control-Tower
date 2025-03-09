using MediatR;

namespace AirportControlTower.Application.Weather.Query;

public sealed record WeatherQuery() : IRequest<WeatherQueryResult>;

