using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Clients;

public interface IWeatherClient
{
    Task<ActionResult<WeatherForecast>> GetLiveWeatherAsync(string location);
}
