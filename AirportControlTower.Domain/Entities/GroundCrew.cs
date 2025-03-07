namespace AirportControlTower.Domain.Entities;

public class GroundCrew
{
    public int Id { get; set; }
    public string Status { get; set; } = default!;
    public DateTime LastCheck { get; set; }
}
