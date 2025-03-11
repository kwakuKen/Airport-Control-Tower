using MediatR;

namespace AirportControlTower.Domain.Events;

public sealed record FlightLogEvent(
    string CallSign,
    string State,
    string Reason,
    bool IsAccepted = false) : INotification;