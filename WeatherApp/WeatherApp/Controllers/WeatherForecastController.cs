using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Clients;

namespace WeatherApp.Controllers
{
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherClient _weatherApi;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherClient weatherApi)
        {
            _logger = logger;
            _weatherApi = weatherApi;
        }

        [EnableCors("AllowSpecificOrigin")]
        [HttpGet("/weatherforecast/{location}")]
        public async Task<WeatherForecast> GetWeatherLive(string location)
        {
            return await _weatherApi.GetLiveWeatherAsync(location);
        }
    }
}