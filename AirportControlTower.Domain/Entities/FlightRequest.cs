namespace AirportControlTower.Domain.Entities;

public class FlightRequest : ExtraEntity
{
    public int AircraftId { get; set; }
    public Aircraft? Aircraft { get; set; }
    public string State { get; set; } = default!;
    public string CallSign { get; set; } = default!;
    public bool IsCompleteCycle { get; set; }
}
