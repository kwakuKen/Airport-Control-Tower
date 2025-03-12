using AirportControlTower.Application.Admin.Query.AircraftList;
using AirportControlTower.Application.Admin.Query.AircraftLogs;
using AirportControlTower.Application.Admin.Query.LastTenFlightLogs;
using AirportControlTower.Application.Admin.Query.ParkingSpot;
using AirportControlTower.Application.Admin.Query.Weather;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace AirportControlTower.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController(ISender sender)
    : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("list-aircraft")]
    public async Task<IActionResult> ListAircraft()
    {
        var result = await _sender.Send(new AircraftListQuery());
        return Ok(result);
    }

    [HttpGet("aircraft-logs")]
    public async Task<IActionResult> AircraftLogs()
    {
        var result = await _sender.Send(new AircraftLogsQuery());
        return Ok(result);
    }

    [HttpGet("weather")]
    public async Task<IActionResult> Weather()
    {
        var result = await _sender.Send(new WeatherQuery());
        return Ok(result);
    }

    [HttpGet("flight-logs")]
    public async Task<IActionResult> FlightLogs()
    {
        var result = await _sender.Send(new FlightLogsQuery());
        return Ok(result);
    }

    [HttpGet("parking-spot")]
    public async Task<IActionResult> ParkingSpot()
    {
        var result = await _sender.Send(new ParkingSpotQuery());
        return Ok(result);
    }
}
