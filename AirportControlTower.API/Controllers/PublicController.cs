using AirportControlTower.Application.Weather.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirportControlTower.API.Controllers;

[Route("api/public/{callSign}/weather")]
[ApiController]
public class PublicController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetWeather()
    {
        var result = await _sender.Send(new WeatherQuery());
        if (result is null) return NoContent();
        return Ok(result);
    }
}
