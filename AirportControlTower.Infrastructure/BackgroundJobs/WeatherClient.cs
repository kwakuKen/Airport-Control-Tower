using Newtonsoft.Json;

namespace AirportControlTower.Infrastructure.BackgroundJobs;

internal class WeatherClient()
{
    private static readonly HttpClient client = new();

    public static async Task<WeatherData> GetWeatherAsync(string city)
    {
        string apiKey = "1a1f91e2241e9056cf2dd4f9cf66e8da";
        string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var weatherData = JsonConvert.DeserializeObject<WeatherData>(responseBody);
            return weatherData!;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
        }
        return default!;
    }
}
