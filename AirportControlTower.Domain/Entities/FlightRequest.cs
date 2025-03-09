namespace AirportControlTower.Domain.Entities;

public class FlightRequest : ExtraEntity
{
    public int AircraftId { get; set; }
    public Aircraft? Aircraft { get; set; }
    public string State { get; set; } = default!;
    public string CallSign { get; set; } = default!;
    public string? Type { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public long? Altitude { get; set; }
    public long? Heading { get; set; }
    public bool IsCompleteCycle { get; set; }
    public ICollection<FlightLogs>? FlightLogs { get; set; }
}

"type": "AIRLINER|PRIVATE", 
"latitude": "44.82128505247063", 
"longitude": "20.455516172478386", 
"altitude": 3500, 
"heading": 220 
