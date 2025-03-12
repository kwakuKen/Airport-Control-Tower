using AirportControlTower.Domain.Interfaces;

namespace AirportControlTower.API.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyHeaderName = "X-Api-Key";
    private const string ApiCallSignHeaderName = "X-CallSign";
    

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Bypass authentication for public endpoints
        if (context.Request.Path.StartsWithSegments("/api/public") ||
            context.Request.Path.StartsWithSegments("/api/admin"))
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var ApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }


        if (!context.Request.Headers.TryGetValue(ApiCallSignHeaderName, out var CallSign))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(CallSign))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        var aircraftRepository = context.RequestServices.GetRequiredService<IAircraftReadRepository>();
        var aircraftDetails = await aircraftRepository.GetAircraftByCallSignAsync(CallSign!, default!);

        if (aircraftDetails is null || !aircraftDetails.PublicKey!.Equals(ApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await _next(context);
    }
}
