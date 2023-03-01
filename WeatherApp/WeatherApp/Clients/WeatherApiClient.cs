using System.Net;

namespace WeatherApp.Clients;

public interface IWeatherClient
{
    Task<WeatherForecast> GetWeatherLiveFromApi(string location);
}

public class WeatherApiClient : IWeatherClient
{
    private readonly HttpClient _httpClient;

    private const string WeatherApiKey = "0b06ab74843f4a11aa4151554222512";

    public WeatherApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherForecast?> GetWeatherLiveFromApi(string location)
    {
        var response = await _httpClient.GetAsync($"current.json?key={WeatherApiKey}&q={location}&aqi=no");
        if (response.IsSuccessStatusCode)
        {
            var forecast = await response.Content.ReadFromJsonAsync<WeatherForecast>();
            forecast.Message = $"Latest weather from {location}.";
            return forecast;
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.NotFound)
        {
            return new WeatherForecast { Message = "Location not found" };
        }
        else
        {
            return new WeatherForecast { Message = "An unexpected error occurred" };
        }
    }
}