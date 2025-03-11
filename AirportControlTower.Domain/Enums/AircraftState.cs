using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace AirportControlTower.Domain.Enums;

public enum AircraftState
{
    PARKED,

    [EnumMember(Value = "TAKE-OFF")]
    [JsonPropertyName("TAKE-OFF")]
    TAKEOFF,
    AIRBORNE,
    APPROACH,
    LANDED
}
