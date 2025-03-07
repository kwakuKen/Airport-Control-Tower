namespace AirportControlTower.Domain.Entities;

public class ParkingSpot
{
    public int Id { get; set; }
    public string Type { get; set; } = default!;
    public bool IsOccupied { get; set; }
    public int? AircraftId { get; set; }
    public Aircraft? Aircraft { get; set; }
}
