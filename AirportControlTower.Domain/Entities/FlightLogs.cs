namespace AirportControlTower.Domain.Entities;

public class FlightLogs : ExtraEntity
{
    public string CallSign { get; set; } = default!;
    public string State { get; set; } = default!;
    public bool IsAccepted { get; set; }
    public string Reason { get; set; } = default!;
}
