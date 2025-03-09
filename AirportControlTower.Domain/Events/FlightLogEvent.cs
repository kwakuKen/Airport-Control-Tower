using MediatR;

namespace AirportControlTower.Domain.Events;

public sealed record FlightLogEvent(
    int AircraftId,
    int FlightRequstId,
    string CallSign,
    string State,
    string Reason,
    bool IsAccepted = false) : INotification;