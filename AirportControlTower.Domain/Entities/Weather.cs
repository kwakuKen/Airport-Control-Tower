namespace AirportControlTower.Domain.Entities;

public class Weather
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public double Temperature { get; set; }
    public int Visibility { get; set; }
    public double WindSpeed { get; set; }
    public int WindDirection { get; set; }
    public DateTime LastUpdate { get; set; }
}
