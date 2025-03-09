namespace AirportControlTower.Domain.Entities;

public class ParkingSpot : ExtraEntity
{
    public string Type { get; set; } = default!;
    public bool IsOccupied { get; set; }
    public int? AircraftId { get; set; }
    public Aircraft? Aircraft { get; set; }
}
