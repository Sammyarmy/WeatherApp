using WeatherApp.Models;

namespace WeatherApp.Clients;

public interface IWeatherClient
{
    Task<WeatherForecast> GetLiveWeatherAsync(string location);
}
