namespace AirportControlTower.Application.Weather.Query;

public sealed record WeatherQueryResult(
    string Description,
    double Temperature,
    int Visibility,
    WindInfo Wind,
    DateTime LastUpdate);

public sealed record WindInfo(
    double Speed,
    int Deg
);