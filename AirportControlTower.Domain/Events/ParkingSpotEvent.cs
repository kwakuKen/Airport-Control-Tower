using MediatR;

namespace AirportControlTower.Domain.Events;

public sealed record ParkingSpotEvent(
    string Type,
    string CallSign,
    bool IsOccupied = true,
    bool IsTakeOff = true)
    : INotification;