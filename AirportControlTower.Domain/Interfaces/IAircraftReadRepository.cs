﻿using AirportControlTower.Domain.Entities;

namespace AirportControlTower.Domain.Interfaces;

public interface IAircraftReadRepository
{

    Task<List<FlightLogs>> GetAllFlightLogsAsync(CancellationToken cancellationToken);
    Task<FlightLogs?> GetCurrentFlightLogsAsync(CancellationToken cancellationToken);
    Task<FlightLogs?> GetFlightLogsByIdAsync(int id, CancellationToken cancellationToken);
    Task<FlightRequest?> GetLastFlightLogsAsync(string callSign, CancellationToken cancellationToken);

}
