using AirportControlTower.Domain.Enums;
using MediatR;

namespace AirportControlTower.Application.Aircraft.Command.Location;

public sealed record LocationCommand(
    string Type,
    string Latitude,
    string Longitude,
    long Altitude,
    long Heading,
    string CallSign
    ) : IRequest<int>;
