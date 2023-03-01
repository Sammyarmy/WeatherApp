using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;
using WeatherApp;

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
        return await _httpClient.GetFromJsonAsync<WeatherForecast>($"current.json?key={WeatherApiKey}&q={location}&aqi=no");
    }
}