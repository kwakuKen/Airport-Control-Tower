namespace AirportControlTower.Domain.Entities;

public class Aircraft: ExtraEntity
{
    public string CallSign { get; set; } = default!;
    public string? Type { get; set; }
    public string? PublicKey { get; set; }
    public ICollection<FlightLogs>? FlightLogs { get; set; }
}
