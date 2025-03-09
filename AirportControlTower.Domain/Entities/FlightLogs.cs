namespace AirportControlTower.Domain.Entities;

public class FlightLogs : ExtraEntity
{
    public int AircraftId { get; set; }
    public Aircraft? Aircraft { get; set; }
    public int FlightRequstId { get; set; }
    public FlightRequest? FlightRequest { get; set; }
    public string CallSign { get; set; } = default!;
    public string State { get; set; } = default!;
    public bool IsAccepted { get; set; }
    public string Reason { get; set; } = default!;
}
