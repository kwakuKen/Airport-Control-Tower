using AirportControlTower.Application.Aircraft.Command.Location;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirportControlTower.API.Controllers
{
    [Route("api/{callSign}/{action}")]
    [ApiController]
    public class PrivateController(ISender sender)
        : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpPut]
        public async Task<IActionResult> Location([FromRoute] string callSign, [FromBody] LocationCommandDto request)
        {
            var command = new LocationCommand(
                request.Type,
                request.Latitude,
                request.Longitude,
                request.Altitude,
                request.Heading,
                callSign);

            var result = await _sender.Send(command);
            return result == -1 ? BadRequest() : NoContent();
        }

    }
}
