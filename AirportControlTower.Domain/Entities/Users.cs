namespace AirportControlTower.Domain.Entities;

public class Users:ExtraEntity
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}
