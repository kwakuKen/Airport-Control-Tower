namespace AirportControlTower.Domain.Entities;

public class FlightLogs: ExtraEntity
{
    public int AircraftId { get; set; }
    public Aircraft? Aircraft { get; set; }
    public string State { get; set; } = default!;
    public bool Accepted { get; set; }
}
