using AirportControlTower.Domain.Enums;

namespace AirportControlTower.Application.Aircraft.Command.Location;

public record LocationCommandDto(
    string Type,
    string Latitude,
    string Longitude,
    long Altitude,
    long Heading);

