using System.Security.Cryptography;
using System.Text;

namespace AirportControlTower.Domain.Entities;

public class Users:ExtraEntity
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;

    public void SetPassword(string password)
    {
        using var sha256 = SHA256.Create();
        Password = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }

    public bool VerifyPassword(string password)
    {
        string hashedInput = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
        return Password == hashedInput;
    }
}
