namespace AirportControlTower.Domain.Entities;

public class FlightLogs
{
    public int Id { get; set; }
    public int AircraftId { get; set; }
    public Aircraft? Aircraft { get; set; }
    public string State { get; set; } = default!;
    public DateTime Timestamp { get; set; }
    public bool Accepted { get; set; }
}
