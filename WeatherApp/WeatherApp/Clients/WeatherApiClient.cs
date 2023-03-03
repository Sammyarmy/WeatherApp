using Microsoft.AspNetCore.Mvc;
using System.Net;
using WeatherApp.Exceptions;
using WeatherApp.Models;

namespace WeatherApp.Clients;

public class WeatherApiClient : IWeatherClient
{
    private readonly HttpClient _httpClient;

    private const string WeatherApiKey = "0b06ab74843f4a11aa4151554222512";

    public WeatherApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ActionResult<WeatherForecast>> GetLiveWeatherAsync(string location)
    {
        var response = await _httpClient.GetAsync($"current.json?key={WeatherApiKey}&q={location}&aqi=no");
        if (response.IsSuccessStatusCode)
        {
            var forecast = await response.Content.ReadFromJsonAsync<WeatherForecast>();
            return forecast;
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new NotFoundException("Weather information not found for the specified location.");
        }
        else
        {
            throw new InternalServerErrorException("Something broke");
        }
    }
}